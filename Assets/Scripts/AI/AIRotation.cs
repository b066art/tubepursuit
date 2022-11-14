using System.Collections;
using UnityEngine;

public class AIRotation : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 30f;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 3f, 6f);

    private Transform bikeModel;
    private Transform closestObstacle;

    private float rotationAngle = 0;
    private float lastEulerAngleZ = 0;
    private float lastDistance = 9999;

    private bool levelStarted = false;
    private bool obstacleIsFound = false;

    private void Start() {
        bikeModel = transform.Find("Model");
        EventManager.LevelStartEvent.AddListener(StartRotation);
    }

    private void Update() {
        if (levelStarted && !obstacleIsFound) { FindClosestObstacle(); }

        if (levelStarted && obstacleIsFound) {
            if (closestObstacle.transform.position.z - transform.position.z <= 0) { obstacleIsFound = false; }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, closestObstacle.rotation, rotationAngle * Time.deltaTime);
            //RotateModel();
        }
    }

    private float CalculateAngle() {
        float enemyAngle = 360 - transform.rotation.eulerAngles.z;
        float obstacleAngle = 360 - closestObstacle.rotation.eulerAngles.z;

        if (Mathf.Abs(obstacleAngle - enemyAngle) > Mathf.Abs(enemyAngle - obstacleAngle)) { return Mathf.Abs(obstacleAngle - enemyAngle); }
        else { return Mathf.Abs(enemyAngle - obstacleAngle); }
    }

    private void FindClosestObstacle() {
        closestObstacle = null;
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles) {
            float distance = Vector3.Distance(transform.position, obstacle.transform.position);

            if (distance < lastDistance && obstacle.transform.position.z - transform.position.z > 0) {
                closestObstacle = obstacle.transform;
                lastDistance = distance;
            }
        }

        if (closestObstacle != null) {
            obstacleIsFound = true;
            rotationAngle = CalculateAngle();
        }
    }

    private void RotateModel() {
        float rotationFactor = Mathf.Clamp((lastEulerAngleZ - transform.rotation.eulerAngles.z), -1, 1);
        bikeModel.localRotation = Quaternion.RotateTowards(bikeModel.localRotation, Quaternion.Euler(targetRotation * rotationFactor), smoothSpeed * Time.deltaTime);
        lastEulerAngleZ = bikeModel.rotation.eulerAngles.z;
    }

    private void StartRotation() { StartCoroutine(WaitAndStartRotation()); }

    private IEnumerator WaitAndStartRotation() {
        yield return new WaitForSeconds(1.5f);
        levelStarted = true;
    }
}
