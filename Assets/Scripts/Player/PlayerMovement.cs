using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof (SwerveInputSystem))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform bikeModel;

    [SerializeField] private float acceleration;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float reduceFactor;

    [SerializeField] private float smoothSpeed = 35f;
    [SerializeField] private float swerveSpeed = .35f;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 3f, 30f);

    private Sequence mySequence;
    private SwerveInputSystem swerveInputSystem;

    private float currentSpeed;
    private float targetSpeed;

    private bool controls = false;

    private void Start() {
        Time.timeScale = 1f;
        Application.targetFrameRate = 120;
        DOTween.SetTweensCapacity(250, 125);
        
        swerveInputSystem = GetComponent<SwerveInputSystem>();

        EventManager.DeadEvent.AddListener(ControlsOff);
        EventManager.DeadEvent.AddListener(DecreaseSpeedToZero);
        EventManager.HitEvent.AddListener(TemporaryReduction);
        EventManager.JumpEvent.AddListener(Jump);
        EventManager.LevelStartEvent.AddListener(ControlsOn);

        currentSpeed = defaultSpeed;
        targetSpeed = defaultSpeed;
    }

    private void Update() {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (controls) { transform.RotateAround(transform.position, Vector3.forward, swerveInputSystem.MoveFactorX * swerveSpeed); }
        RotateModel();
    }

    private void ControlsOn() { controls = true; }

    private void ControlsOff() { controls = false; }

    private void DecreaseSpeed() { targetSpeed = defaultSpeed * reduceFactor; }

    private void DecreaseSpeedToZero() { targetSpeed = 0; }

    private void IncreaseSpeed() { targetSpeed = defaultSpeed; }

    private void RotateModel() {
        float rotationFactor;
        if (controls) { rotationFactor = Mathf.Clamp(swerveInputSystem.MoveFactorX, -1, 1); }
        else { rotationFactor = 0; }
        bikeModel.localRotation = Quaternion.RotateTowards(bikeModel.localRotation, Quaternion.Euler(targetRotation * rotationFactor), smoothSpeed * Time.deltaTime);
    }

    private void Jump() {
        mySequence = DOTween.Sequence();
        mySequence.Append(bikeModel.DOLocalJump(bikeModel.localPosition, 2f, 1, .5f).SetEase(Ease.OutSine));
        mySequence.Join(bikeModel.DOLocalRotate(Vector3.left * 20f, .1f).SetEase(Ease.OutSine));

        ControlsOff();
        Invoke("ControlsOn", .5f);
    }

    private void TemporaryReduction() {
        DecreaseSpeed();
        Invoke("IncreaseSpeed", 1f);
    }

    private void OnDestroy() { mySequence.Kill(); }
}
