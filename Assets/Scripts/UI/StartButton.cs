using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartLevel() {
        EventManager.LevelStartEvent.Invoke();
        gameObject.SetActive(false);
    }
}
