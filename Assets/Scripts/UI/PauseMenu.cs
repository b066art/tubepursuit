using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    [SerializeField] private Image musicIconOn;
    [SerializeField] private Image musicIconOff;
    [SerializeField] private Image soundsIconOn;
    [SerializeField] private Image soundsIconOff;
    [SerializeField] private Image vibrationIconOn;
    [SerializeField] private Image vibrationIconOff;

    private Image background;

    private void Awake() { Instance = this; }

    private void Start() {
        background = GetComponent<Image>();
        EventManager.PausedEvent.AddListener(ShowMenu);
    }

    private void ShowMenu() {
        background.enabled = true;
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(true); }
        RefreshIcons();
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

    public void RefreshIcons() {
        musicIconOn.enabled = SettingsMenu.Instance.music;
        musicIconOff.enabled = !SettingsMenu.Instance.music;

        soundsIconOn.enabled = SettingsMenu.Instance.sounds;
        soundsIconOff.enabled = !SettingsMenu.Instance.sounds;

        vibrationIconOn.enabled = SettingsMenu.Instance.vibration;
        vibrationIconOff.enabled = !SettingsMenu.Instance.vibration;
    }
}
