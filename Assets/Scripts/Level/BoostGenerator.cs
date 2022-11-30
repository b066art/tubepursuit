using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] RoadSegments;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool[] SegmentNumbers;
    [SerializeField] private int maxRoadLength = 6;
    [SerializeField] private int delay = 2;
    [SerializeField] private float distanceBetweenSegments = 10f;
    [SerializeField] private float maxPositionZ = 10f;
    [SerializeField] private Vector3 waitingZone = new Vector3(0, 0, -40);

    private List<GameObject> ReadyRoad = new List<GameObject>();

    private int currentRoadLength = 0;
    private bool firstObstacle = true;
    private bool generatingIsOn = false;

    private int currentSegmentNumber = -1;
    private int lastSegmentNumber = -1;

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

            if (ReadyRoad.Count != 0) { RemoveRoad(); }
        }
    }

    private void RemoveRoad() {
        if (playerTransform.position.z - ReadyRoad[0].transform.position.z > maxPositionZ) {
            int i = ReadyRoad[0].GetComponent<Boost>().number;
            SegmentNumbers[i] = false;
            ReadyRoad[0].transform.localPosition = waitingZone;
            ReadyRoad.RemoveAt(0);
            currentRoadLength--;
        }
    }

    private void RoadCreation() {
        if (ReadyRoad.Count > 0) {
            if (firstObstacle) {
                RoadSegments[currentSegmentNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + Vector3.forward * distanceBetweenSegments * (Mathf.RoundToInt(playerTransform.position.z / maxPositionZ) + delay) + Vector3.back * 12f;
                RoadSegments[currentSegmentNumber].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                firstObstacle = false;
            } else {
            RoadSegments[currentSegmentNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + Vector3.forward * distanceBetweenSegments + Vector3.back * 12f;
            RoadSegments[currentSegmentNumber].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            }
        } else if (ReadyRoad.Count == 0) {
            RoadSegments[currentSegmentNumber].transform.localPosition = Vector3.zero;
        }

        RoadSegments[currentSegmentNumber].GetComponent<Boost>().number = currentSegmentNumber;
        SegmentNumbers[currentSegmentNumber] = true;
        lastSegmentNumber = currentSegmentNumber;
        ReadyRoad.Add(RoadSegments[currentSegmentNumber]);
        currentRoadLength++;
    }

    private void StartGenerating() { generatingIsOn = true; }

    private void StopGenerating() { generatingIsOn = false; }
}
