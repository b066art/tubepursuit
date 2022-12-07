using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate() { transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); }
}
