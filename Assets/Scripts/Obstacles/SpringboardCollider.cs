using UnityEngine;

public class SpringboardCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { EventManager.JumpEvent.Invoke(); }
}
