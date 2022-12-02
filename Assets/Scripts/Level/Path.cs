using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform p3;

    [SerializeField] private int distanceBetweenPoints;
    [SerializeField] private float maxOffset;

    private void Awake() { GenerateCurve(); }

    public void GenerateCurve() {
        p1.localPosition = Vector3.right * Random.Range(-maxOffset, maxOffset) + Vector3.up * Random.Range(-maxOffset, maxOffset) + Vector3.forward * distanceBetweenPoints * 1;
        p2.localPosition = Vector3.right * Random.Range(-maxOffset, maxOffset) + Vector3.up * Random.Range(-maxOffset, maxOffset) + Vector3.forward * distanceBetweenPoints * 2;
        p3.localPosition = Vector3.forward * distanceBetweenPoints * 3;
    }

    public int GetLength() { return distanceBetweenPoints * 3; }

    private void OnDrawGizmos() {
        int segmentsNumber = 36;
        Vector3 previousPoint = p0.position;

        for (int i = 0; i <= segmentsNumber; i++) {
            float value = (float)i / segmentsNumber;
            Vector3 point = Bezier.GetPoint(p0.position, p1.position, p2.position, p3.position, value);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
