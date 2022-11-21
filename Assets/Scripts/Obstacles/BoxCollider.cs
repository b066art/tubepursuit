using System.Collections;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{
    [SerializeField] private GameObject splintersPrefab;

    private MeshRenderer[] meshes = new MeshRenderer[3];

    private void Start() { meshes = GetComponentsInChildren<MeshRenderer>(); }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            EventManager.HitEvent.Invoke();
            GameObject fragments = Instantiate(splintersPrefab, transform.position, Quaternion.identity);
            HideBox();
        }
    }

    private void HideBox() {
        for (int i = 0; i < meshes.Length; i++) { meshes[i].enabled = false; }
        Invoke("ShowBox", 1f);
    }

    private void ShowBox() { for (int i = 0; i < meshes.Length; i++) { meshes[i].enabled = true; }}
}
