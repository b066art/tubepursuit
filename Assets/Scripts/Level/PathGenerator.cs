using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator Instance;

    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private Transform levelPath;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxPositionZ;

    public int pathLength;

    private Path previousPath = null;
    private Path currentPath = null;

    private bool isEnabled = true;

    private void Awake() { Instance = this; } 

    private void Start() { GeneratePath(); } 

    private void FixedUpdate() { if (isEnabled) { if (playerTransform.position.z - levelPath.GetChild(0).position.z > maxPositionZ) { MoveSegment(); }}}

    private void GeneratePath() {
        for (int i = 0; i < pathLength; i++) {
            GameObject path = Instantiate(pathPrefab, levelPath);

            if (previousPath != null) {
                path.transform.position = previousPath.p3.position;
                path.GetComponent<Path>().p1.localPosition = previousPath.p3.localPosition - previousPath.p2.localPosition;
            }

            previousPath = path.GetComponent<Path>();
        }
        
        EventManager.PathReadyEvent.Invoke();
    }

    private void MoveSegment() {
        levelPath.GetChild(0).position = previousPath.p3.position;
        currentPath = levelPath.GetChild(0).GetComponent<Path>();
        currentPath.GenerateCurve();
        currentPath.p1.localPosition = previousPath.p3.localPosition - previousPath.p2.localPosition;
        previousPath = currentPath;
        levelPath.GetChild(0).SetAsLastSibling();
    }




}
