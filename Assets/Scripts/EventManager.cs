using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent DeadEvent = new UnityEvent();
    public static UnityEvent<Vector3, Quaternion> GlassBreakEvent = new UnityEvent<Vector3, Quaternion>();
    public static UnityEvent HitEvent = new UnityEvent();
    public static UnityEvent JumpEvent = new UnityEvent();
    public static UnityEvent LevelStartEvent = new UnityEvent();
    public static UnityEvent LevelFinishEvent = new UnityEvent();
}
