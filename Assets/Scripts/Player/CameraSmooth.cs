using UnityEngine;

public class CameraSmooth : MonoBehaviour {

    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Quaternion defaultRotation;
    [SerializeField] private Transform model;
    [SerializeField] private Transform target;
    [SerializeField] private Transform lookPosition;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void Update() { GameplayMode(); }

    private void GameplayMode() {
        float smoothedPositionX = Mathf.Lerp(transform.position.x, target.position.x, smoothSpeed);
        float smoothedPositionY = Mathf.Lerp(transform.position.y, target.position.y, smoothSpeed);
        Vector3 smoothedPosition = new Vector3(smoothedPositionX, smoothedPositionY, target.position.z);
        transform.position = smoothedPosition;
        transform.LookAt(lookPosition);
    }
}
