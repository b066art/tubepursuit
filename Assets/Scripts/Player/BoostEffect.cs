using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
        EventManager.BoostEvent.AddListener(StartAnimation);
    }

    private void StartAnimation() { ps.Play(); }
}
