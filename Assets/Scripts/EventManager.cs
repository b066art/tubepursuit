using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent DeadEvent = new UnityEvent();
    public static UnityEvent HitEvent = new UnityEvent();
}
