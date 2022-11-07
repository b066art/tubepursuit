using System.Collections;
using UnityEngine;

[RequireComponent (typeof (SwerveInputSystem))]
public class SwerveRotation : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 35f;
    [SerializeField] private float swerveSpeed = .35f;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 2.5f, 5f);

    private Transform carModel;
    private SwerveInputSystem swerveInputSystem;

    private void Awake() {
        carModel = transform.Find("Model");
        swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    private void Update() {
        transform.RotateAround(transform.position, Vector3.forward, swerveInputSystem.MoveFactorX * swerveSpeed);
        RotateModel();
    }

    private void RotateModel() {
        float rotationFactor = Mathf.Clamp(swerveInputSystem.MoveFactorX, -1, 1);
        carModel.localRotation = Quaternion.RotateTowards(carModel.localRotation, Quaternion.Euler(targetRotation * rotationFactor), smoothSpeed * Time.deltaTime);
    }
}
