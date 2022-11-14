using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] GameObject progressBar;
    [SerializeField] private int levelTime = 60;

    private LevelProgressBar levelProgressBar;

    private float timer = 0;

    private bool timerIsStarted = false;

    private void Start() { EventManager.LevelStartEvent.AddListener(StartTimer); }

    private void Update() {
        if (timerIsStarted) {
            timer += Time.deltaTime;
            levelProgressBar.UpdateProgress(timer / levelTime);
            if (timer >= levelTime) {
                timerIsStarted = false;
                HideBar();
                EventManager.DeadEvent.Invoke();
            }
        }
    }

    private void StartTimer() {
        timerIsStarted = true;
        ShowBar();
        levelProgressBar = progressBar.GetComponent<LevelProgressBar>();
    }

    private void HideBar() { progressBar.SetActive(false); }

    private void ShowBar() { progressBar.SetActive(true); }
}
