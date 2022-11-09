using DG.Tweening;
using UnityEngine;

public class SpikesR : MonoBehaviour
{
    private void Start() {
        Sequence mySequence = DOTween.Sequence();

        mySequence.AppendInterval(.5f);
        mySequence.Append(transform.DOLocalMove(new Vector3(-0.7f, -1.55f, 0), 1f).SetEase(Ease.InOutSine));
        mySequence.AppendInterval(.5f);
        mySequence.Append(transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InOutSine));
        mySequence.SetLoops(-1);
    }

}
