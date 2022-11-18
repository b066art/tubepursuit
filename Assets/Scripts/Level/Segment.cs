using DG.Tweening;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] Material stripes;

    public int number;
    
    private Sequence mySequence;

    private void Start() {
        stripes.color = new Color(254/255f, 168/255f, 2/255f);
        mySequence = DOTween.Sequence();

        mySequence.Append(stripes.DOColor(new Color(254/255f, 168/255f, 2/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(254/255f, 116/255f, 52/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(234/255f, 81/255f, 95/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(217/255f, 74/255f, 140/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(180/255f, 65/255f, 142/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(217/255f, 74/255f, 140/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(234/255f, 81/255f, 95/255f), 1).SetEase(Ease.InOutSine));
        mySequence.Append(stripes.DOColor(new Color(254/255f, 116/255f, 52/255f), 1).SetEase(Ease.InOutSine));

        mySequence.SetLoops(-1);
    }

    private void OnDestroy() { mySequence.Kill(); }
}
