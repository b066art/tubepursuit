using System.Collections;
using UnityEngine;

public class GlassCollider : MonoBehaviour
{
    [SerializeField] private GameObject fragmentsPrefab;

    private MeshRenderer meshRenderer;

    private void Start() { meshRenderer = GetComponentInParent<MeshRenderer>(); }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            EventManager.HitEvent.Invoke();
            GameObject fragments = Instantiate(fragmentsPrefab, transform.position, Quaternion.identity);
            HideGlass();
        }
    }

    private void HideGlass() {
        meshRenderer.enabled = false;
        Invoke("ShowGlass", 1f);
    }

    private void ShowGlass() { meshRenderer.enabled = true; }
}
