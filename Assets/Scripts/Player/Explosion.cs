using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
        EventManager.DeadEvent.AddListener(StartAnimation);
    }

    private void StartAnimation() { ps.Play(); }
}
