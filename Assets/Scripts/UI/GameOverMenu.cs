using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private Image image;

    private void Start() {
        EventManager.DeadEvent.AddListener(ShowMenu);
        image = GetComponent<Image>();
    }

    private void ShowMenu() {
        image.enabled = true;
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(true); }
        Time.timeScale = 0f;
    }
}
