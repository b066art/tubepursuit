using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPath;
    [SerializeField] private Transform level;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private int distance;
    [SerializeField] private int maxPositionZ;
    [SerializeField] private float step;

    private List<Path> paths = new List<Path>();

    private Transform currentSegment = null;

    private float p = 0;
    private int s = 0;
    private float t = 0;

    private bool isEnabled = false;

    private void Start() {
        Invoke("GetPaths", 3f);
        Invoke("GenerateLevel", 4f);
    }

    private void FixedUpdate() { if (isEnabled) { if (playerTransform.position.z - level.GetChild(0).position.z > maxPositionZ) { MoveSegment(); }}}

    private void GenerateLevel() {
        for(; p < distance; p += step) {
            s = Mathf.RoundToInt(Mathf.Floor(p));

            if (s != 0) { t = p % s; }
            else { t = p; }

            GameObject ring = Instantiate(ringPrefab, level);

            ring.transform.position = Bezier.GetPoint(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t);
            ring.transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t));
        }

        isEnabled = true;
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void MoveSegment() {
        currentSegment = level.GetChild(0);

        s = Mathf.RoundToInt(Mathf.Floor(p));
        t = p % s;

        currentSegment.position = Bezier.GetPoint(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t);
        currentSegment.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t));
        
        currentSegment.SetAsLastSibling();
        p += step;
    }
}
