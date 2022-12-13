using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerModel;
    [SerializeField] Transform bikeModel;
    [SerializeField] private Transform levelPath;

    [SerializeField] private float smoothSpeed = 35f;
    [SerializeField] private float swerveSpeed = .35f;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 3f, 30f);

    [SerializeField] private ParticleSystem explosion;

    private List<Path> paths = new List<Path>();

    private Path currentPath;
    private SwerveInputSystem swerveInputSystem;

    private bool isEnabled = false;
    private bool controls = false;

    [SerializeField] private float defaultSpeed;
    private float speed;

    private float p = 0;
    private int s = 0;
    private float t = 0;

    private void Awake() {
        EventManager.BoostEvent.AddListener(TemporarilyIncreaseSpeed);
        EventManager.HitEvent.AddListener(TemporarilyReduceSpeed);
        EventManager.DeadEvent.AddListener(ControlsOff);
        EventManager.PathReadyEvent.AddListener(EnableMovement);
        EventManager.LevelStartEvent.AddListener(EnableControls);

        speed = defaultSpeed;
    }

    private void Start() { swerveInputSystem = GetComponent<SwerveInputSystem>(); }

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

    private void EnableMovement() {
        GetPaths();
        isEnabled = true;
    }

    private void EnableControls() { Invoke("ControlsOn", 2f); }

    private void ControlsOn() { controls = true; }

    private void ControlsOff() {
        isEnabled = false;
        controls = false;
        explosion.Play();
        bikeModel.gameObject.SetActive(false);
    }

    private void IncreaseSpeed() { speed *= 1.3f; }

    private void ReduceSpeed() { speed /= 1.3f; }

    private void ResetSpeed() { speed = defaultSpeed; }

    private void TemporarilyIncreaseSpeed() {
        IncreaseSpeed();
        Invoke("ResetSpeed", 3f);
    }

    private void TemporarilyReduceSpeed() {
        ReduceSpeed();
        Invoke("ResetSpeed", 3f);
    }


}
