using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform playerModel;
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform bikeModel;
    [SerializeField] private Transform levelPath;

    [SerializeField] private float smoothSpeed = 35f;
    [SerializeField] private float swerveSpeed = .35f;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 3f, 30f);

    private List<Path> paths = new List<Path>();

    private Path currentPath;
    private SwerveInputSystem swerveInputSystem;

    private bool isEnabled = false;
    private bool controls = false;

    [SerializeField] private float speed;

    private float p = 0;
    private int s = 0;
    private float t = 0;

    private void Start() {
        swerveInputSystem = GetComponent<SwerveInputSystem>();

        EventManager.LevelStartEvent.AddListener(ControlsOn);
        EventManager.DeadEvent.AddListener(ControlsOff);

        Invoke("GetPaths", 0.5f);
        Invoke("EnableMovement", 0.5f);
    }

    private void Update() {
        if (isEnabled) {
            p += speed * Time.deltaTime;
            s = Mathf.RoundToInt(Mathf.Floor(p));

            if (s != 0) { t = p % s; }
            else { t = p; }

            transform.position = Bezier.GetPoint(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t);
            transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t));
        }

        if (controls) {
            playerModel.Rotate(0f, 0f, swerveInputSystem.MoveFactorX * swerveSpeed);
        }
        RotateModel();
    }

    private void RotateModel() {
        float rotationFactor;
        if (controls) { rotationFactor = Mathf.Clamp(swerveInputSystem.MoveFactorX, -1, 1); }
        else { rotationFactor = 0; }
        bikeModel.localRotation = Quaternion.RotateTowards(bikeModel.localRotation, Quaternion.Euler(targetRotation * rotationFactor), smoothSpeed * Time.deltaTime);
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void EnableMovement() { isEnabled = true; }

    private void ControlsOn() { controls = true; }

    private void ControlsOff() { controls = false; }
}
