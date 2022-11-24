using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate() { transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime); }
}
