using DG.Tweening;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] Material ringMat;
    
    private Sequence mySequence;

    private void Start() {
        mySequence = DOTween.Sequence();

        mySequence.Append(ringMat.DOColor(new Color(254/255f, 168/255f, 2/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(254/255f, 116/255f, 52/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(234/255f, 81/255f, 95/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(217/255f, 74/255f, 140/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(180/255f, 65/255f, 142/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(217/255f, 74/255f, 140/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(234/255f, 81/255f, 95/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(ringMat.DOColor(new Color(254/255f, 116/255f, 52/255f), 1).SetEase(Ease.InOutSine));

        mySequence.SetLoops(-1);
    }

    private void OnDestroy() {
        mySequence.Kill();
        ringMat.color = new Color(254/255f, 168/255f, 2/255f);
    }
}
