using Random = UnityEngine.Random;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool[] SegmentNumbers;
    [SerializeField] private int maxRoadLength = 6;
    [SerializeField] private int delay = 2;
    [SerializeField] private float distanceBetweenSegments = 10f;
    [SerializeField] private float maxPositionZ = 10f;
    [SerializeField] private Vector3 waitingZone = new Vector3(0, 0, -48);

    private GameObject[] RoadSegments = new GameObject[6];
    private List<GameObject> ReadyRoad = new List<GameObject>();

    private int currentRoadLength = 0;
    private bool firstObstacle = true;
    private bool generatingIsOn = false;

    private int currentSegmentNumber = -1;
    private int lastSegmentNumber = -1;

    private int[] obstacles = new int[6];

    private int obstacleMinLevel;
    private int obstacleMaxLevel;

    private int currentLevel = 1;

    private void Start() {
        EventManager.LevelStartEvent.AddListener(StartGenerating);
        EventManager.LevelFinishEvent.AddListener(StopGenerating);
    }

    private void FixedUpdate() {
        if (generatingIsOn) {
            if (currentRoadLength != maxRoadLength) {
                currentSegmentNumber = Random.Range(0, RoadSegments.Length);

                if (currentSegmentNumber != lastSegmentNumber) {
                    if (currentSegmentNumber < RoadSegments.Length / 2) {
                        if (SegmentNumbers[currentSegmentNumber] != true) {
                            if (lastSegmentNumber != (RoadSegments.Length / 2) + currentSegmentNumber) { RoadCreation(); }
                            else if (lastSegmentNumber == (RoadSegments.Length / 2) + currentSegmentNumber && currentRoadLength == RoadSegments.Length - 1) { RoadCreation(); }
                        }
                    } else if (currentSegmentNumber >= RoadSegments.Length / 2) {
                        if (SegmentNumbers[currentSegmentNumber] != true) {
                            if (lastSegmentNumber != currentSegmentNumber - (RoadSegments.Length / 2)) { RoadCreation(); }
                            else if (lastSegmentNumber == currentSegmentNumber - (RoadSegments.Length / 2) && currentRoadLength == RoadSegments.Length - 1) { RoadCreation(); }
                        }
                    }
                }
            }

            if (!firstObstacle && ReadyRoad.Count != 0) { RemoveRoad(); }
        }
    }

    private void RemoveRoad() {
        if (playerTransform.position.z - ReadyRoad[0].transform.position.z > maxPositionZ) {
            int i = ReadyRoad[0].GetComponent<Obstacle>().number;
            SegmentNumbers[i] = false;
            ReadyRoad[0].transform.localPosition = waitingZone;
            ReadyRoad.RemoveAt(0);
            currentRoadLength--;
        }
    }

    private void RoadCreation() {
        if (ReadyRoad.Count > 0) {
            if (firstObstacle) {
                RoadSegments[currentSegmentNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + Vector3.forward * distanceBetweenSegments * (Mathf.RoundToInt(playerTransform.position.z / maxPositionZ) + delay);
                if (RoadSegments[currentSegmentNumber].GetComponent<Obstacle>().rotatable == true) { RoadSegments[currentSegmentNumber].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360)); }
                firstObstacle = false;
            } else {
            RoadSegments[currentSegmentNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + Vector3.forward * distanceBetweenSegments;
            if (RoadSegments[currentSegmentNumber].GetComponent<Obstacle>().rotatable == true) { RoadSegments[currentSegmentNumber].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360)); }
            }
        } else if (ReadyRoad.Count == 0) {
            RoadSegments[currentSegmentNumber].transform.localPosition = Vector3.zero;
        }

        RoadSegments[currentSegmentNumber].GetComponent<Obstacle>().number = currentSegmentNumber;
        SegmentNumbers[currentSegmentNumber] = true;
        lastSegmentNumber = currentSegmentNumber;
        ReadyRoad.Add(RoadSegments[currentSegmentNumber]);
        currentRoadLength++;
    }

    private void FindMinLevel() {
        for (int i = 0; i < obstacles.Length; i++) { obstacles[i] = obstacleMinLevel + i; }
        if (currentLevel + obstacles.Length - 1 > (obstacles[0] + obstacles[obstacles.Length - 1]) * obstacles.Length / 2) {
            obstacleMinLevel++;
            FindMinLevel();
        }
    }

    private void GenerateArray() {
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
}
