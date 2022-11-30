using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera cam;
    private Vector3 defaultPosition;

    Sequence boostSequence;
    Sequence jumpSequence;
    Sequence reduceSequence;

    private void Start() {
        cam = Camera.main;
        defaultPosition = cam.transform.localPosition;

        EventManager.BoostEvent.AddListener(FOVIncrease);
        EventManager.DeadEvent.AddListener(FOVReduce);
        EventManager.HitEvent.AddListener(FOVJump);
    }

    private void FOVJump() {
        jumpSequence = DOTween.Sequence();

        jumpSequence.Append(cam.DOFieldOfView(70f, 1f).SetEase(Ease.InOutSine));
        jumpSequence.Join(cam.DOShakePosition(.5f, .5f, 10, 0));
        jumpSequence.Append(cam.DOFieldOfView(90f, 1f).SetEase(Ease.InOutSine));

        Invoke("ResetPosition", .5f);
    }

    private void FOVIncrease() {
        cam.DOFieldOfView(110f, .5f).SetEase(Ease.InOutSine);
        Invoke("FOVDefault", 2.5f);
    }

    private void FOVDefault() {
        boostSequence.Append(cam.DOFieldOfView(90f, .5f).SetEase(Ease.InOutSine));
    }

    private void FOVReduce() {
        reduceSequence = DOTween.Sequence();

        reduceSequence.Append(cam.DOFieldOfView(70f, 1f).SetEase(Ease.InOutSine));
        reduceSequence.Join(cam.DOShakePosition(.5f, .5f, 10, 0));

        Invoke("ResetPosition", .5f);
    }

    private void ResetPosition() { cam.transform.localPosition = defaultPosition; }

    private void OnDestroy() {
        transform.DOKill();
        boostSequence.Kill();
        jumpSequence.Kill();
        reduceSequence.Kill();
    }
}
