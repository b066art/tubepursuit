using System.Collections;
using UnityEngine;

public class BikeMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float reduceFactor;

    private float currentSpeed;
    private float targetSpeed;

    private void Start() {
        currentSpeed = defaultSpeed;
        targetSpeed = defaultSpeed;
        EventManager.DeadEvent.AddListener(DecreaseSpeedToZero);
        EventManager.HitEvent.AddListener(StartTemporaryReduction);
    }

    private void FixedUpdate() {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);
    }

    private void IncreaseSpeed() { targetSpeed = defaultSpeed; }

    private void DecreaseSpeed() { targetSpeed = defaultSpeed * reduceFactor; }

    private void DecreaseSpeedToZero() { targetSpeed = 0; }

    private void StartTemporaryReduction() { StartCoroutine(TemporaryReduction()); }

    private IEnumerator TemporaryReduction() {
        DecreaseSpeed();
        yield return new WaitForSeconds(1f);
        IncreaseSpeed();
    }
}
