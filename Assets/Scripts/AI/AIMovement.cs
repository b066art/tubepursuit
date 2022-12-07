using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedFactor;
    [SerializeField] private float increasedSpeedTime;

    [SerializeField] private Transform levelPath;

    private List<Path> paths = new List<Path>();

    private float currentSpeed;
    private float targetSpeed;

    private bool isEnabled = false;

    private float p = 0;
    private int s = 0;
    private float t = 0;

    private void Start() {
        currentSpeed = startSpeed;
        targetSpeed = startSpeed;
        EventManager.LevelStartEvent.AddListener(EnableMovement);
        EventManager.LevelStartEvent.AddListener(StartSpeed);
    }

    private void Update() {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        if (isEnabled) {
            p += currentSpeed * Time.deltaTime;
            s = Mathf.RoundToInt(Mathf.Floor(p));

            if (s != 0) { t = p % s; }
            else { t = p; }

            transform.position = Bezier.GetPoint(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t);
            transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t));
        }
    }

    private void StartSpeed() { IncreaseSpeed(); }

    private void IncreaseSpeed() {
        currentSpeed = defaultSpeed;
        targetSpeed = defaultSpeed * speedFactor;
        Invoke("DefaultSpeed", increasedSpeedTime);
    }

    private void DefaultSpeed() { targetSpeed = defaultSpeed; }

    public void SetTargetSpeedToZero() { targetSpeed = 0; }

    private void EnableMovement() {
        GetPaths();
        isEnabled = true;
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }
}
