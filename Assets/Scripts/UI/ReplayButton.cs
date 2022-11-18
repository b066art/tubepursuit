using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void ResetScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
}
