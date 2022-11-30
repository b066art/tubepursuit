using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public static CurrentLevel Instance;

    private int currentLevel = 1;

    private void Awake() { Instance = this; }

    private void Start() { EventManager.LevelFinishEvent.AddListener(IncreaseLevel); }
    
    public int GetLevel() { return currentLevel; }

    private void IncreaseLevel() {
        currentLevel++;
        Debug.Log(currentLevel);
        }

    public void SetLevel(int level) { currentLevel = level; }
}
