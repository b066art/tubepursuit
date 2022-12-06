using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private Transform stars;

    private Image background;
    private Sequence mySequence;

    private void Awake() { EventManager.LevelFinishEvent.AddListener(ShowMenu); }

    private void Start() { background = GetComponent<Image>(); }

    private void ShowMenu() {
        background.enabled = true;
        content.SetActive(true);
        ShowStars();
    }

    private void ShowStars() {
        mySequence = DOTween.Sequence();

        for (int i = 0; i < CriminalsCounter.Instance.GetCount(); i++) {
            mySequence.Append(stars.GetChild(i).GetChild(0).DOScale(Vector3.one, .5f).SetEase(Ease.InOutExpo));
            mySequence.Join(stars.GetChild(i).DOPunchScale(Vector3.one * 1.05f, .25f, 1, 1f).SetEase(Ease.InOutExpo).SetDelay(.25f));
            stars.GetChild(i).GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnDisable() { mySequence.Kill(); }

    private void OnDestroy() { mySequence.Kill(); } 
}
