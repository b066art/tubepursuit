using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] LevelProgressBar levelProgressBar;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform enemy1Transform;
    [SerializeField] Transform enemy2Transform;
    [SerializeField] Transform enemy3Transform;

    [SerializeField] private int levelDistance = 1800;

    private float startPositionZ;

    private bool levelFinished = false;

    private void Start() { EventManager.LevelStartEvent.AddListener(StartLevel); }

    private void Update() {
        levelProgressBar.UpdateBar((playerTransform.position.z - startPositionZ) / levelDistance, (enemy1Transform.position.z - startPositionZ) / levelDistance, (enemy2Transform.position.z - startPositionZ) / levelDistance, (enemy3Transform.position.z - startPositionZ) / levelDistance);
        if (playerTransform.position.z - startPositionZ >= levelDistance && !levelFinished) {
            levelFinished = true;
            EventManager.LevelFinishEvent.Invoke();
        }
    }

    private void StartLevel() { startPositionZ = playerTransform.position.z; }

}
