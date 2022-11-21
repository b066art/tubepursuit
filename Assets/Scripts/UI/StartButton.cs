using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private TMP_Text startButtonText;

    private void Start() {
        EventManager.PausedEvent.AddListener(HideText);
        EventManager.UnpausedEvent.AddListener(ShowText);
    }

    public void StartLevel() {
        EventManager.LevelStartEvent.Invoke();
        gameObject.SetActive(false);
    }

    private void HideText() { startButtonText.enabled = false; }

    private void ShowText() { startButtonText.enabled = true; }
}
