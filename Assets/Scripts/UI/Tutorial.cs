using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject fingerprint;

    private void Start() { EventManager.LevelStartEvent.AddListener(ShowTutorial); }

    private void HideTutorial() {
        arrow.SetActive(false);
        fingerprint.SetActive(false);
    }

    private void ShowTutorial() {
        arrow.SetActive(true);
        fingerprint.SetActive(true);
        Invoke("HideTutorial", 10f);
    }
}
