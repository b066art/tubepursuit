using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { EventManager.DeadEvent.Invoke(); }
}
