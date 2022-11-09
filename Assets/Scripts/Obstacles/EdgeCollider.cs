using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { EventManager.HitEvent.Invoke(); }
}
