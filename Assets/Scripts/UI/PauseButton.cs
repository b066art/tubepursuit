using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Image pauseButton;

    private void Start() {
        EventManager.PausedEvent.AddListener(HideButton);
        EventManager.UnpausedEvent.AddListener(ShowButton);
    }

    public void PauseGame() { EventManager.PausedEvent.Invoke(); }

    private void ShowButton() { pauseButton.enabled = true; }

    private void HideButton() { pauseButton.enabled = false; }
}
