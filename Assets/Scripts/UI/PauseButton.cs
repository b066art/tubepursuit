using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Transform pauseButton;

    private Image settingsButton;

    private void Start() {
        settingsButton = GetComponent<Image>();

        EventManager.LevelStartEvent.AddListener(ShowButton);
        EventManager.PausedEvent.AddListener(HideButton);
        EventManager.UnpausedEvent.AddListener(ShowButton);
    }

    public void PauseGame() { EventManager.PausedEvent.Invoke(); }

    private void ShowButton() {
        settingsButton.raycastTarget = true;
        pauseButton.DOScale(Vector3.one, .25f);
    }

    private void HideButton() {
        settingsButton.raycastTarget = false;
        pauseButton.localScale = Vector3.zero;
    }
}
