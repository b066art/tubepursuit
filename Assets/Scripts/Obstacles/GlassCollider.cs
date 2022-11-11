using System.Collections;
using UnityEngine;

public class GlassCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            EventManager.GlassBreakEvent.Invoke(transform.position, transform.rotation);
            EventManager.HitEvent.Invoke();
            StartCoroutine(HideGlass());
        }
    }

    private IEnumerator HideGlass() {
        GetComponentInParent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponentInParent<MeshRenderer>().enabled = true;
    }
}
