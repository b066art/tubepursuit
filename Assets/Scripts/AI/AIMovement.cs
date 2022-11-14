using System.Collections;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedFactor;

    private float currentSpeed;
    private float targetSpeed;

    private void Start() {
        currentSpeed = startSpeed;
        targetSpeed = startSpeed;
        EventManager.LevelStartEvent.AddListener(StartSpeed);
    }

    private void Update() {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void StartSpeed() { StartCoroutine(IncreaseSpeed()); }

    private IEnumerator IncreaseSpeed() {
        currentSpeed = defaultSpeed;
        targetSpeed = defaultSpeed * speedFactor;
        yield return new WaitForSeconds(3f);
        DefaultSpeed();
    }

    private void DefaultSpeed() { targetSpeed = defaultSpeed; }

    public void SetTargetSpeedToZero() { targetSpeed = 0; }
}
