using Random = UnityEngine.Random;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPath;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform obstaclesObject;

    [SerializeField] private GameObject[] obstaclePrefab;

    [SerializeField] private int distance;
    [SerializeField] private int maxPositionZ;

    [SerializeField] private float obstacleStep;

    private List<Path> paths = new List<Path>();

    private Transform currentObstacle = null;

    private int currentSegmentNumber = -1;
    private int lastSegmentNumber = -1;

    private int[] obstacles = new int[6];

    private int obstacleMinLevel;
    private int obstacleMaxLevel;

    private int currentLevel = 1;

    private float obstacleP = 0;
    private int obstacleS = 0;
    private float obstacleT = 0;

    private bool isEnabled = false;

    private void Start() {
        Invoke("GetPaths", 0.5f);
        Invoke("GenerateObstacles", 0.5f);
    }

    private void FixedUpdate() {
        if (isEnabled) { if (playerTransform.position.z - obstaclesObject.GetChild(0).position.z > maxPositionZ) { MoveObstacle(); }}
    }

    private void GenerateObstacles() {
        for(; obstacleP < distance; obstacleP += obstacleStep) {
            obstacleS = Mathf.RoundToInt(Mathf.Floor(obstacleP));

            if (obstacleS != 0) { obstacleT = obstacleP % obstacleS; }
            else { obstacleT = obstacleP; }

            GameObject obstacle = Instantiate(obstaclePrefab[0], obstaclesObject);

            obstacle.transform.position = Bezier.GetPoint(paths[obstacleS].p0.position, paths[obstacleS].p1.position, paths[obstacleS].p2.position, paths[obstacleS].p3.position, obstacleT);
            obstacle.transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[obstacleS].p0.position, paths[obstacleS].p1.position, paths[obstacleS].p2.position, paths[obstacleS].p3.position, obstacleT)) * Quaternion.Euler(0, 0, Random.Range(0, 360));
        }

        isEnabled = true;
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void MoveObstacle() {
        currentObstacle = obstaclesObject.GetChild(0);

        obstacleS = Mathf.RoundToInt(Mathf.Floor(obstacleP));
        obstacleT = obstacleP % obstacleS;

        currentObstacle.position = Bezier.GetPoint(paths[obstacleS].p0.position, paths[obstacleS].p1.position, paths[obstacleS].p2.position, paths[obstacleS].p3.position, obstacleT);
        currentObstacle.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[obstacleS].p0.position, paths[obstacleS].p1.position, paths[obstacleS].p2.position, paths[obstacleS].p3.position, obstacleT)) * Quaternion.Euler(0, 0, Random.Range(0, 360));
        
        currentObstacle.SetAsLastSibling();
        obstacleP += obstacleStep;
    }

    /*
    private void FindMinLevel() {
        for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = obstacleMinLevel + i; }
        if (currentLevel + obstacles.Length - 1 > (obstacles[0] + obstacles[obstacles.Length - 1]) * obstacles.Length / 2) {
            obstacleMinLevel++;
            FindMinLevel();
        }
    }

    private void GenerateList() {
        if (obstacleMinLevel == 1) {
            for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = 1; }

            for (int i = 1; i < obstacles.Length; i++) {
                for (int n = 0; n < obstacles.Length - i; n++) {
                    if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
                    obstacles[n] = i + 1;
                }
            }

            Array.Sort(obstacles);
        }

        else if (obstacleMinLevel + obstacles.Length > obstacleMaxLevel) {

            for (int i = 0; i < obstacles.Length; i++) {
                int n = obstacleMinLevel + i - 1;
                if (n > obstacleMaxLevel) { n = obstacleMaxLevel; }
                obstacles[i] = n;
            }

            while (obstacles.Sum() != obstacleMaxLevel * obstacles.Length) {
                for (int i = 0; i < obstacles.Length; i++) {
                    obstacles[i]++;
                    if (obstacles[i] > obstacleMaxLevel) { obstacles[i] = obstacleMaxLevel; }
                    if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
                }

                if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
            }
        }

        else {
            for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = obstacleMinLevel + i - 1; }

            for (int i = 0; i < obstacles.Length; i++) {
                obstacles[0] = i + obstacleMinLevel;
                if (obstacles.Sum() == currentLevel + obstacles.Length - 1) { break; }
            }

            Array.Sort(obstacles);

            if (obstacles[obstacles.Length - 1] >= obstacleMaxLevel) {
                obstacles[0]++;
                obstacles[obstacles.Length - 1]--;
            }
        }
    }

    private void StartGenerating() {
        currentLevel = CurrentLevel.Instance.GetLevel();
        obstacleMinLevel = 1;
        obstacleMaxLevel = obstaclePrefabs.Length;

        FindMinLevel();
        GenerateArray();

        for (int i = 0; i < obstacles.Length; i++) {
            for (int n = 0; n < obstaclePrefabs.Length; n++) {
                if (obstaclePrefabs[n].GetComponent<Obstacle>().difficulty == obstacles[i]) {
                    GameObject obstacle = Instantiate(obstaclePrefabs[n], this.transform);
                    obstacle.transform.position = waitingZone;
                    obstacle.name = obstaclePrefabs[n].name;
                    RoadSegments[i] = obstacle;
                }
            }
        }

        generatingIsOn = true;
    }

    private void StopGenerating() { generatingIsOn = false; }
    */
}
