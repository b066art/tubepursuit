using UnityEngine;

public class AICollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionPS;

    private Collider boxCollider;

    private void Start() { boxCollider = GetComponent<Collider>(); }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            boxCollider.enabled = false;
            EventManager.CriminalCaughtEvent.Invoke();
            GetComponentInParent<AIMovement>().SetTargetSpeedToZero();
            explosionPS.Play();
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
