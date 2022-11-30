using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform levelPath;

    private List<Path> paths = new List<Path>();

    private Path currentPath;

    private bool isEnabled = false;

    [SerializeField] private float speed;

    private float p = 0;
    private int s = 0;
    private float t = 0;

    private void Start() {
        Invoke("GetPaths", 5f);
        Invoke("EnableMovement", 5f);
    }

    private void Update() {
        if (isEnabled) {
            p += speed * Time.deltaTime;
            s = Mathf.RoundToInt(Mathf.Floor(p));

            if (s != 0) { t = p % s; }
            else { t = p; }

            transform.position = Bezier.GetPoint(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t);
            transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[s].p0.position, paths[s].p1.position, paths[s].p2.position, paths[s].p3.position, t));
        }
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void EnableMovement() { isEnabled = true; }
}
