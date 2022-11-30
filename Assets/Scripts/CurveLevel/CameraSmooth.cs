using UnityEngine;

public class CameraSmooth : MonoBehaviour {

    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Quaternion defaultRotation;
    [SerializeField] private Transform target;
    [SerializeField] private Transform lookPosition;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void Update() { GameplayMode(); }

    private void GameplayMode() {
        Vector3 finalPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(lookPosition);
    }
}
