using System.Text;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    private StringBuilder outputText = new StringBuilder(15);
    private TMP_Text levelText;

    private void Start() {
        levelText = GetComponent<TMP_Text>();
        EventManager.LevelStartEvent.AddListener(HideText);
        EventManager.NewLevelEvent.AddListener(UpdateText);
        UpdateText();
    }

    private void HideText() { levelText.enabled = false; }

    private void ShowText() { levelText.enabled = true; }

    private void UpdateText() {
        outputText.Length = 0;
        outputText.Append("LEVEL ");
        outputText.Append(CurrentLevel.Instance.GetLevel());
        levelText.text = outputText.ToString();
    }
}
