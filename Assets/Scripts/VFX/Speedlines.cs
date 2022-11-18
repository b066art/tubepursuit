using UnityEngine;

public class Speedlines : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();

        EventManager.DeadEvent.AddListener(StopAnimation);
        EventManager.HitEvent.AddListener(TemporarilyStopAnimation);

        StartAnimation();
    }

    private void FixedUpdate() { transform.localPosition = Vector3.forward * playerTransform.position.z; }

    private void StartAnimation() { ps.Play(); }

    private void StopAnimation() { ps.Stop(); }

    private void TemporarilyStopAnimation() {
        StopAnimation();
        Invoke("StartAnimation", 1f);
    }

}
