using DG.Tweening;
using TMPro;
using UnityEngine;

public class CriminalsCounter : MonoBehaviour
{
    public static CriminalsCounter Instance;

    [SerializeField] private GameObject content;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private RectTransform icon;

    private Sequence mySequence;

    private int counter = 0;

    private void Awake() {
        Instance = this;

        EventManager.LevelStartEvent.AddListener(ShowCounter);
        EventManager.LevelFinishEvent.AddListener(HideCounter);
        EventManager.CriminalCaughtEvent.AddListener(AddCriminal);
    }

    private void Start() { counterText.text = counter.ToString(); }

    public int GetCount() { return counter; }

    private void HideCounter() {
        content.SetActive(false);
    }

    private void ShowCounter() {
        content.SetActive(true);
    }

    private void AddCriminal() {
        ShakeIcon();
        counterText.text = (++counter).ToString();
    }

    private void ShakeIcon() {
        mySequence = DOTween.Sequence();

        mySequence.Append(icon.DOPunchScale(Vector3.one * .2f, .5f, 2, 1f));
        mySequence.Join(icon.DOPunchAnchorPos(Vector2.one * 25f, 1f, 20, 1f));
    }

    private void OnDisable() { mySequence.Kill(); }

    private void OnDestroy() { mySequence.Kill(); }
}
