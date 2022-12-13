using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScreenText : MonoBehaviour
{
    private TMP_Text textLabel;
    private Transform textTransform;
    private Sequence mySequence;

    private void Start() {
        EventManager.CriminalCaughtEvent.AddListener(ShowText);
        EventManager.LevelStartEvent.AddListener(WaitAndShowStartText);

        textLabel = GetComponent<TMP_Text>();
        textTransform = GetComponent<Transform>();
    }

    private void ShowText() {
        switch (CriminalsCounter.Instance.GetCount()) {
            case 1:
                textLabel.text = "FIRST BLOOD";
                AnimateText();
                break;
            case 2:
                textLabel.text = "ONLY ONE\nLEFT";
                AnimateText();
                break;
            case 3:
                textLabel.text = "GOOD JOB";
                AnimateText();
                break;
        }
    }

    private void AnimateText() {
        mySequence = DOTween.Sequence();    

        mySequence.Append(textTransform.DOScale(Vector3.one, .75f).SetEase(Ease.InOutQuad));
        mySequence.Append(textTransform.DOScale(Vector3.zero, .75f).SetEase(Ease.InOutQuad));
    }

    private void WaitAndShowStartText() { Invoke("ShowStartText", 2f); }

    private void ShowStartText() {
        textLabel.text = "CATCH THE\nCRIMINALS";
        AnimateText();
    }

    private void OnDisable() { mySequence.Kill(); }

    private void OnDestroy() { mySequence.Kill(); }
}
