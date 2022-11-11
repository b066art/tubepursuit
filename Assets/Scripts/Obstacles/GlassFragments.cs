using UnityEngine;

public class GlassFragments : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
        EventManager.GlassBreakEvent.AddListener(PlayAnimation);
    }

    private void PlayAnimation(Vector3 pos, Quaternion rot) {
        transform.position = pos;
        transform.rotation = rot;
        ps.Play();
    }
}
