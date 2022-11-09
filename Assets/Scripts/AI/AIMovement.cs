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

    private void Update() {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
