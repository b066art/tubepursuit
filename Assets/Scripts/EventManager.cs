using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent BoostEvent = new UnityEvent();
    public static UnityEvent CriminalCaughtEvent = new UnityEvent();
    public static UnityEvent DeadEvent = new UnityEvent();
    public static UnityEvent<Vector3, Quaternion> GlassBreakEvent = new UnityEvent<Vector3, Quaternion>();
    public static UnityEvent HitEvent = new UnityEvent();
    public static UnityEvent JumpEvent = new UnityEvent();
    public static UnityEvent LevelFinishEvent = new UnityEvent();
    public static UnityEvent LevelStartEvent = new UnityEvent();
    public static UnityEvent NewLevelEvent = new UnityEvent();
    public static UnityEvent PathReadyEvent = new UnityEvent();
    public static UnityEvent PausedEvent = new UnityEvent();
    public static UnityEvent UnpausedEvent = new UnityEvent();
}
