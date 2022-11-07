using UnityEngine;

public class Speedlines : MonoBehaviour
{
    [SerializeField] private Transform carTransform;
    [SerializeField] private float offsetZ;

    private ParticleSystem speedlinesPS;

    private void FixedUpdate() {
        transform.position = new Vector3(0, 0, carTransform.position.z + offsetZ);
    }
}
