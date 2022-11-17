using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public static CurrentLevel Instance;

    private int currentLevel = 1;

    private void Awake() { Instance = this; }

    public int GetLevel() { return currentLevel; }

    private void IncreaseLevel() { currentLevel++; }

    public void SetLevel(int level) { currentLevel = level; }
}
