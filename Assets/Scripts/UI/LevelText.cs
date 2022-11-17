using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    private TMP_Text levelText;

    private void Start() {
        levelText = GetComponent<TMP_Text>();
        EventManager.LevelFinishEvent.AddListener(ShowText);
        EventManager.LevelStartEvent.AddListener(HideText);
        EventManager.NewLevelEvent.AddListener(UpdateText);
        UpdateText();
    }

    private void HideText() { levelText.enabled = false; }

    private void ShowText() { levelText.enabled = true; }

    private void UpdateText() { levelText.text = "LEVEL " + CurrentLevel.Instance.GetLevel(); }
}
