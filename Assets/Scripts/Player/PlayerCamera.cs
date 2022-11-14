using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera cam;
    private Vector3 defaultPosition;

    Sequence jumpSequence;
    Sequence reduceSequence;

    private void Start() {
        cam = Camera.main;
        defaultPosition = cam.transform.localPosition;
        EventManager.DeadEvent.AddListener(FOVReduce);
        EventManager.HitEvent.AddListener(FOVJump);
    }

    private void FOVJump() {
        jumpSequence = DOTween.Sequence();

        jumpSequence.Append(cam.DOFieldOfView(75f, 1f).SetEase(Ease.InOutSine));
        jumpSequence.Join(cam.DOShakePosition(.5f, .5f, 10, 0));
        jumpSequence.Append(cam.DOFieldOfView(90f, 1f).SetEase(Ease.InOutSine));

        StartCoroutine(ResetPosition());
    }

    private void FOVReduce() {
        reduceSequence = DOTween.Sequence();

        reduceSequence.Append(cam.DOFieldOfView(75f, 1f).SetEase(Ease.InOutSine));
        reduceSequence.Join(cam.DOShakePosition(.5f, .5f, 10, 0));

        StartCoroutine(ResetPosition());
    }

    private IEnumerator ResetPosition() {
        yield return new WaitForSeconds(.5f);
        cam.transform.localPosition = defaultPosition;
    }

    private void OnDestroy() {
        jumpSequence.Kill();
        reduceSequence.Kill();
    }
}
