using System.Collections;
using UnityEngine;

public class AIRotation : MonoBehaviour
{
    [SerializeField] private Transform obstacles;

    private Transform enemyModel;
    private Transform closestObstacle;

    private float rotationAngle = 0;
    private float lastDistance = 9999;

    private Quaternion nextRotation;

    private bool isEnabled = false;
    private bool obstacleIsFound = false;

    private void Start() {
        EventManager.LevelStartEvent.AddListener(StartRotation);
        enemyModel = transform.Find("EnemyModel");
    }

    private void Update() {
        if (isEnabled && !obstacleIsFound) { FindClosestObstacle(); }

        if (isEnabled && obstacleIsFound) {
            if (closestObstacle.transform.position.z - transform.position.z <= 0) { obstacleIsFound = false; }

            nextRotation = Quaternion.Euler(Vector3.forward * rotationAngle);
            enemyModel.localRotation = Quaternion.RotateTowards(enemyModel.localRotation, nextRotation, 1f);
        }
    }

    private float CalculateAngle() {
        float enemyAngle = transform.localRotation.eulerAngles.z;
        float obstacleAngle = closestObstacle.localRotation.eulerAngles.z;

        if (Mathf.Abs(obstacleAngle - enemyAngle) > Mathf.Abs(enemyAngle - obstacleAngle)) { return Mathf.Abs(obstacleAngle - enemyAngle); }
        else { return Mathf.Abs(enemyAngle - obstacleAngle); }
    }

    private void FindClosestObstacle() {
        closestObstacle = null;

        for (int i = 0; i < obstacles.childCount; i++) {
            float distance = Vector3.Distance(transform.position, obstacles.GetChild(i).position);

            if (distance < lastDistance && obstacles.GetChild(i).position.z - transform.position.z > 0) {
                closestObstacle = obstacles.GetChild(i);
                lastDistance = distance;
            }
        }

        if (closestObstacle != null) {
            obstacleIsFound = true;
            rotationAngle = CalculateAngle();
        }
    }

    private void StartRotation() { Invoke("EnableMovement", 2f); }

    private void EnableMovement() { isEnabled = true; }
}
