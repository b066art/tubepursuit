using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPath;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform segments;
    [SerializeField] private Transform rings;

    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private int distance;
    [SerializeField] private int maxPositionZ;

    [SerializeField] private float segmentStep;
    [SerializeField] private float ringStep;

    private List<Path> paths = new List<Path>();

    private Transform currentSegment = null;
    private Transform currentRing = null;

    private float segmentP = 0;
    private int segmentS = 0;
    private float segmentT = 0;

    private float ringP = 0;
    private int ringS = 0;
    private float ringT = 0;

    private bool isEnabled = false;

    private void Start() {
        Invoke("GetPaths", 0.5f);
        Invoke("GenerateLevel", 0.5f);
    }

    private void FixedUpdate() {
        if (isEnabled) {
            if (playerTransform.position.z - segments.GetChild(0).position.z > maxPositionZ) { MoveSegment(); }
            if (playerTransform.position.z - rings.GetChild(0).position.z > maxPositionZ) { MoveRing(); }
        }
    }

    private void GenerateLevel() {
        for(; segmentP < distance; segmentP += segmentStep) {
            segmentS = Mathf.RoundToInt(Mathf.Floor(segmentP));

            if (segmentS != 0) { segmentT = segmentP % segmentS; }
            else { segmentT = segmentP; }

            GameObject segment = Instantiate(segmentPrefab, segments);

            segment.transform.position = Bezier.GetPoint(paths[segmentS].p0.position, paths[segmentS].p1.position, paths[segmentS].p2.position, paths[segmentS].p3.position, segmentT);
            segment.transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[segmentS].p0.position, paths[segmentS].p1.position, paths[segmentS].p2.position, paths[segmentS].p3.position, segmentT));
        }

        for(; ringP < distance; ringP += ringStep) {
            ringS = Mathf.RoundToInt(Mathf.Floor(ringP));

            if (ringS != 0) { ringT = ringP % ringS; }
            else { ringT = ringP; }

            GameObject ring = Instantiate(ringPrefab, rings);

            ring.transform.position = Bezier.GetPoint(paths[ringS].p0.position, paths[ringS].p1.position, paths[ringS].p2.position, paths[ringS].p3.position, ringT);
            ring.transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[ringS].p0.position, paths[ringS].p1.position, paths[ringS].p2.position, paths[ringS].p3.position, ringT));
        }

        isEnabled = true;
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void MoveSegment() {
        currentSegment = segments.GetChild(0);

        segmentS = Mathf.RoundToInt(Mathf.Floor(segmentP));
        segmentT = segmentP % segmentS;

        currentSegment.position = Bezier.GetPoint(paths[segmentS].p0.position, paths[segmentS].p1.position, paths[segmentS].p2.position, paths[segmentS].p3.position, segmentT);
        currentSegment.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[segmentS].p0.position, paths[segmentS].p1.position, paths[segmentS].p2.position, paths[segmentS].p3.position, segmentT));
        
        currentSegment.SetAsLastSibling();
        segmentP += segmentStep;
    }

    private void MoveRing() {
        currentRing = rings.GetChild(0);

        ringS = Mathf.RoundToInt(Mathf.Floor(ringP));
        ringT = ringP % ringS;

        currentRing.position = Bezier.GetPoint(paths[ringS].p0.position, paths[ringS].p1.position, paths[ringS].p2.position, paths[ringS].p3.position, ringT);
        currentRing.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[ringS].p0.position, paths[ringS].p1.position, paths[ringS].p2.position, paths[ringS].p3.position, ringT));
        
        currentRing.SetAsLastSibling();
        ringP += ringStep;
    }
}
