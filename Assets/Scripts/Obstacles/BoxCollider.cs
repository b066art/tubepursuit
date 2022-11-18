using System.Collections;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{
    [SerializeField] private GameObject splintersPrefab;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            EventManager.HitEvent.Invoke();
            GameObject fragments = Instantiate(splintersPrefab, transform.position, Quaternion.identity);
            StartCoroutine(HideBox());
        }
    }

    private IEnumerator HideBox() {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes) { mesh.enabled = false; }
        yield return new WaitForSeconds(1f);
        foreach (MeshRenderer mesh in meshes) { mesh.enabled = true; }
    }
}
