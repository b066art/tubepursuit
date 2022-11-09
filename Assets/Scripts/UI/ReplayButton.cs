using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void ResetScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
