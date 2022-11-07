using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    private float lastPointerPositionX;
    private float moveFactorX;

    public float MoveFactorX => moveFactorX;

    private void Update() {
        #if UNITY_ANDROID
        if (Input.touchCount == 1) {
            if (Input.GetMouseButtonDown(0)) {
                lastPointerPositionX = Input.mousePosition.x;
            } else if (Input.GetMouseButton(0)) {
                moveFactorX = Input.mousePosition.x - lastPointerPositionX;
                lastPointerPositionX = Input.mousePosition.x;
            } else if (Input.GetMouseButtonUp(0)) {
                moveFactorX = 0f;
            }
        }
        #endif

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            lastPointerPositionX = Input.mousePosition.x;
        } else if (Input.GetMouseButton(0)) {
            moveFactorX = Input.mousePosition.x - lastPointerPositionX;
            lastPointerPositionX = Input.mousePosition.x;
        } else if (Input.GetMouseButtonUp(0)) {
            moveFactorX = 0f;
        }
        #endif
    }
}
