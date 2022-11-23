using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private Image settingsButtonImage;

    private Image settingsButton;

    private bool isPressed = false;

    private void Start() {
        settingsButton = GetComponent<Image>();
        EventManager.LevelStartEvent.AddListener(HideButton);
    }

    public void AnimateButton() {
        if (isPressed) {
            settingsButtonImage.transform.DORotate(Vector3.zero, .25f);
            isPressed = false;
        } else {
            settingsButtonImage.transform.DORotate(Vector3.forward * 90f, .25f);
            isPressed = true;
        }
    }

    private void ShowButton() {
        settingsButton.raycastTarget = true;
        settingsButtonImage.transform.DOScale(Vector3.one, .25f);
    }

    private void HideButton() {
        settingsButton.raycastTarget = false;
        settingsButtonImage.transform.localScale = Vector3.zero;
    }
}
