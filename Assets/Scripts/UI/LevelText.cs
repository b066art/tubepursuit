using System.Text;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public static LevelText Instance;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject stars;

    private StringBuilder outputText = new StringBuilder(15);
    private TMP_Text levelText;

    private void Awake() { Instance = this; }

    private void Start() {
        levelText = GetComponent<TMP_Text>();
        EventManager.LevelStartEvent.AddListener(HideText);
        UpdateText();
    }

    private void HideText() {
        levelText.enabled = false;
        nextButton.SetActive(false);
        previousButton.SetActive(false);
        stars.SetActive(false);
    }

    private void ShowText() {
        levelText.enabled = true;
        nextButton.SetActive(true);
        previousButton.SetActive(true);
        stars.SetActive(true);
    }

    public void UpdateText() {
        outputText.Length = 0;
        outputText.Append("LEVEL ");
        outputText.Append(CurrentLevel.Instance.GetLevel());
        levelText.text = outputText.ToString();
    }
}
