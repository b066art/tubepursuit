using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Image background;

    private void Start() {
        background = GetComponent<Image>();
        EventManager.PausedEvent.AddListener(ShowMenu);
    }

    private void ShowMenu() {
        background.enabled = true;
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(true); }
        Time.timeScale = 0f;
    }

    private void HideMenu() {
        background.enabled = false;
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(false); }
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        HideMenu();
        EventManager.UnpausedEvent.Invoke();
    }
}
