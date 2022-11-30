using UnityEngine;

public class BoostCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { EventManager.BoostEvent.Invoke(); }
}