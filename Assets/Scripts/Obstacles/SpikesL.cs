using DG.Tweening;
using UnityEngine;

public class SpikesL : MonoBehaviour
{
    Sequence mySequence; 

    private void Start() {
        mySequence = DOTween.Sequence();

        mySequence.AppendInterval(.5f);
        mySequence.Append(transform.DOLocalMove(new Vector3(-1f, -1.4f, 0), 1f).SetEase(Ease.InOutSine));
        mySequence.AppendInterval(.5f);
        mySequence.Append(transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InOutSine));
        mySequence.SetLoops(-1);
    }

    private void OnDestroy() { mySequence.Kill(); }
}
