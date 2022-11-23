using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] LevelProgressBar levelProgressBar;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform enemyTransform;

    [SerializeField] private int levelDistance = 1800;

    private float startPositionZ;

    private void Start() { EventManager.LevelStartEvent.AddListener(StartLevel); }

    private void Update() {
        levelProgressBar.UpdateBar((playerTransform.position.z - startPositionZ) / levelDistance, (enemyTransform.position.z - startPositionZ) / levelDistance);
        if (playerTransform.position.z - startPositionZ >= levelDistance) { EventManager.DeadEvent.Invoke(); }
    }

    private void StartLevel() {
        startPositionZ = playerTransform.position.z;
    }

}
