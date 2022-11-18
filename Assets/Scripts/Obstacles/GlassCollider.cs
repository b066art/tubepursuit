using System.Collections;
using UnityEngine;

public class GlassCollider : MonoBehaviour
{
    [SerializeField] private GameObject fragmentsPrefab;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            EventManager.HitEvent.Invoke();
            GameObject fragments = Instantiate(fragmentsPrefab, transform.position, Quaternion.identity);
            StartCoroutine(HideGlass());
        }
    }

    private IEnumerator HideGlass() {
        GetComponentInParent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponentInParent<MeshRenderer>().enabled = true;
    }
}
