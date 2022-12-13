using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLevel : MonoBehaviour
{
    public static CurrentLevel Instance;

    [SerializeField] private Transform starsImages;

    [SerializeField] private Sprite starBlanked;
    [SerializeField] private Sprite starFilled;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;

    private Dictionary<int, int> levels = new Dictionary<int, int>() { [0] = 0, [1] = 0, [2] = 0, [3] = 0, [4] = 0, [5] = 0, [6] = 0, [7] = 0, [8] = 0, [9] = 0, [10] = 0, [11] = 0, [12] = 0, [13] = 0, [14] = 0, [15] = 0, [16] = 0, [17] = 0, [18] = 0, [19] = 0, [20] = 0, [21] = 0, [22] = 0, [23] = 0, [24] = 0, [25] = 0, [26] = 0, [27] = 0, [28] = 0, [29] = 0, [30] = 0, [31] = 0, [32] = 0, [33] = 0, [34] = 0, [35] = 0, [36] = 0, [37] = 0, [38] = 0, [39] = 0, [40] = 0, [41] = 0, [42] = 0, [43] = 0, [44] = 0, [45] = 0, [46] = 0, [47] = 0, [48] = 0, [49] = 0, [50] = 0, [51] = 0, [52] = 0, [53] = 0, [54] = 0, [55] = 0, [56] = 0, [57] = 0, [58] = 0, [59] = 0, [60] = 0, [61] = 0, [62] = 0, [63] = 0 };
    private int currentLevel = 1;
    private int playerLevel = 1;

    private void Awake() { Instance = this; }

    private void Start() {
        EventManager.LevelFinishEvent.AddListener(IncreaseLevel);
    }

    public int GetPlayerLevel() { return playerLevel; }
    
    public int GetLevel() { return currentLevel; }

    public Dictionary<int, int> GetLevels() { return levels; }

    private void IncreaseLevel() {
        SetRating(CriminalsCounter.Instance.GetCount());
        playerLevel++;
    }

    public void SetLevel(int level) {
        playerLevel = level;
        currentLevel = playerLevel;
        CheckButtons();
        UpdateStars();
    }

    public void SetLevels(Dictionary<int, int> levelsSaved) { levels = levelsSaved; }

    public void SetRating(int stars) { if (stars > levels[currentLevel - 1]) { levels[currentLevel - 1] = stars; }}

    private void UpdateStars() {
        for (int i = 0; i < 3; i++) { starsImages.GetChild(i).GetComponent<Image>().sprite = starBlanked; }
        for (int i = 0; i < levels[currentLevel - 1]; i++) { starsImages.GetChild(i).GetComponent<Image>().sprite = starFilled; }
    }

    public void NextLevel() {
        currentLevel++;
        CheckButtons();
        LevelText.Instance.UpdateText();
        UpdateStars();
    }

    public void PreviousLevel() {
        currentLevel--;
        CheckButtons();
        LevelText.Instance.UpdateText();
        UpdateStars();
    }

    private void CheckButtons() {
        if (currentLevel + 1 > playerLevel || currentLevel == levels.Count) { nextButton.SetActive(false); } else { nextButton.SetActive(true); }
        if (currentLevel - 1 == 0) { previousButton.SetActive(false); } else { previousButton.SetActive(true); }
    }
}
