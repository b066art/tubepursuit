using UnityEngine;

public class BoostCollider : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Start() { meshRenderer = GetComponentInParent<MeshRenderer>(); }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            EventManager.BoostEvent.Invoke();
            HideModel();
        }
    }

    private void HideModel() {
        meshRenderer.enabled = false;
        Invoke("ShowModel", 1f);
    }

    private void ShowModel() { meshRenderer.enabled = true; }
}