using DG.Tweening;
using UnityEngine;

public class Fingerprint : MonoBehaviour
{
    RectTransform rT;
    Sequence mySequence;

    private void Start() {
        rT = GetComponent<RectTransform>();
        EventManager.LevelStartEvent.AddListener(StartTutorial);
    }

    private void StartTutorial() {
        mySequence = DOTween.Sequence();

        mySequence.AppendInterval(.5f);
        mySequence.Append(rT.DOAnchorPosX(-320f, 1f).SetEase(Ease.InOutQuad));
        mySequence.Append(rT.DOAnchorPosX(320f, 2f).SetEase(Ease.InOutQuad));
        mySequence.Append(rT.DOAnchorPosX(0, 1f).SetEase(Ease.InOutQuad));
        mySequence.SetLoops(-1);
    }

    private void OnDestroy() { mySequence.Kill(); }
}
