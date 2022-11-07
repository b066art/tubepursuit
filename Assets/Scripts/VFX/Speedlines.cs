using UnityEngine;

public class Speedlines : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float offsetZ;

    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
        EventManager.DeadEvent.AddListener(StopAnimation);
        StartAnimation();
    }

    private void FixedUpdate() { transform.position = new Vector3(0, 0, playerTransform.position.z + offsetZ); }

    private void StartAnimation() { ps.Play(); }

    private void StopAnimation() { ps.Stop(); }
}
