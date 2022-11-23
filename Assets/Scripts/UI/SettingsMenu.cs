using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance;

    [SerializeField] private RectTransform musicButton;
    [SerializeField] private RectTransform soundsButton;
    [SerializeField] private RectTransform vibrationButton;
    [SerializeField] private RectTransform qualityButton;
    [SerializeField] private RectTransform fpsButton;

    [SerializeField] private Image musicIconOn;
    [SerializeField] private Image musicIconOff;
    [SerializeField] private Image soundsIconOn;
    [SerializeField] private Image soundsIconOff;
    [SerializeField] private Image vibrationIconOn;
    [SerializeField] private Image vibrationIconOff;
    [SerializeField] private Image qualityIconOn;
    [SerializeField] private Image qualityIconOff;
    [SerializeField] private Image fpsIconOn;
    [SerializeField] private Image fpsIconOff;

    private Sequence mySequence;

    private bool isShown = false;

    public bool music = true;
    public bool sounds = true;
    public bool vibration = true;
    public bool quality = true;
    public bool fps = false;

    private void Awake() { Instance = this; }

    private void Start() {
        EventManager.LevelStartEvent.AddListener(HideSettings);
        RefreshIcons();
    }

    public void SwitchSettings() {
        if (isShown) {
            HideSettings();
            isShown = false;
        } else {
            ShowSettings();
            isShown = true;
        }
    }

    private void HideSettings() {
        mySequence = DOTween.Sequence();

        mySequence.Append(musicButton.DOAnchorPosX(200f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.05f, soundsButton.DOAnchorPosX(200f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.1f, vibrationButton.DOAnchorPosX(200f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.15f, qualityButton.DOAnchorPosX(200f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.2f, fpsButton.DOAnchorPosX(200f, .25f).SetEase(Ease.InOutQuad));
    }

    private void ShowSettings() {
        mySequence = DOTween.Sequence();

        mySequence.Append(musicButton.DOAnchorPosX(-50f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.05f, soundsButton.DOAnchorPosX(-50f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.1f, vibrationButton.DOAnchorPosX(-50f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.15f, qualityButton.DOAnchorPosX(-50f, .25f).SetEase(Ease.InOutQuad));
        mySequence.Insert(.2f, fpsButton.DOAnchorPosX(-50f, .25f).SetEase(Ease.InOutQuad));
    }

    public void MusicSwitch() {
        music = !music;
        RefreshIcons();
        PauseMenu.Instance.RefreshIcons();
    }

    public void SoundSwitch() {
        sounds = !sounds;
        RefreshIcons();
        PauseMenu.Instance.RefreshIcons(); 
    }

    public void VibrationSwitch() {
        vibration = !vibration;
        RefreshIcons();
        PauseMenu.Instance.RefreshIcons();
    }

    public void QualitySwitch() {
        quality = !quality;
        RefreshIcons();
    }

    public void FPSSwitch() {
        fps = !fps;
        FPSCounter.Instance.ChangeState();
        RefreshIcons();
    }

    private void RefreshIcons() {
        musicIconOn.enabled = music;
        musicIconOff.enabled = !music;

        soundsIconOn.enabled = sounds;
        soundsIconOff.enabled = !sounds;

        vibrationIconOn.enabled = vibration;
        vibrationIconOff.enabled = !vibration;

        qualityIconOn.enabled = quality;
        qualityIconOff.enabled = !quality;

        fpsIconOn.enabled = fps;
        fpsIconOff.enabled = !fps;
    }

    private void OnDisable() { mySequence.Kill(); }

    private void OnDestroy() { mySequence.Kill(); }
}
