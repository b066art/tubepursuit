using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private Image image;

    private void Start() {
        EventManager.DeadEvent.AddListener(WaitAndShow);
        image = GetComponent<Image>();
    }

    private void WaitAndShow() { Invoke("ShowMenu", 1f); }

    private void ShowMenu() {
        image.enabled = true;
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(true); }
    }
}
