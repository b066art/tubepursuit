using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
        EventManager.BoostEvent.AddListener(StartAnimation);
        EventManager.DeadEvent.AddListener(StopAnimation);
        EventManager.HitEvent.AddListener(StopAnimation);
    }

    private void StartAnimation() { ps.Play(); }

    private void StopAnimation() { ps.Stop(); }
}
