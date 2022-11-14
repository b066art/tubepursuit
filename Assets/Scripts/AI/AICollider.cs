using UnityEngine;

public class AICollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionPS;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GetComponentInParent<AIMovement>().SetTargetSpeedToZero();
            explosionPS.Play();
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
