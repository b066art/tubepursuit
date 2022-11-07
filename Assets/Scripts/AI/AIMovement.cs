using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float defaultSpeed;

    private float currentSpeed;
    private float targetSpeed;

    private void Start() {
        currentSpeed = defaultSpeed;
        targetSpeed = defaultSpeed;
    }

    private void FixedUpdate() {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);
    }
}
