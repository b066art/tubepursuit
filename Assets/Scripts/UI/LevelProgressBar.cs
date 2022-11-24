using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject playerMark;
    [SerializeField] private GameObject enemy1Mark;
    [SerializeField] private GameObject enemy2Mark;
    [SerializeField] private GameObject enemy3Mark;

    private RectTransform playerMarkRT;
    private RectTransform enemy1MarkRT;
    private RectTransform enemy2MarkRT;
    private RectTransform enemy3MarkRT;

    private float startPositionX = -820f;
    private float endPositionX = 820f;

    private void Start() {
        playerMarkRT = playerMark.GetComponent<RectTransform>();
        enemy1MarkRT = enemy1Mark.GetComponent<RectTransform>();
        enemy2MarkRT = enemy2Mark.GetComponent<RectTransform>();
        enemy3MarkRT = enemy3Mark.GetComponent<RectTransform>();

        EventManager.LevelFinishEvent.AddListener(HideBar);
        EventManager.LevelStartEvent.AddListener(ShowBar);
    }

    public void UpdateBar(float playerProgress, float enemy1Progress, float enemy2Progress, float enemy3Progress) {
        playerMarkRT.anchoredPosition = Vector3.right * (startPositionX + (endPositionX - startPositionX) * playerProgress);
        enemy1MarkRT.anchoredPosition = Vector3.right * (startPositionX + (endPositionX - startPositionX) * enemy1Progress);
        enemy2MarkRT.anchoredPosition = Vector3.right * (startPositionX + (endPositionX - startPositionX) * enemy2Progress);
        enemy3MarkRT.anchoredPosition = Vector3.right * (startPositionX + (endPositionX - startPositionX) * enemy3Progress);
    }

    private void HideBar() {
        bg.SetActive(false);
        playerMark.SetActive(false);
        enemy1Mark.SetActive(false);
        enemy2Mark.SetActive(false);
        enemy3Mark.SetActive(false);
    }

    private void ShowBar() {
        bg.SetActive(true);
        playerMark.SetActive(true);
        enemy1Mark.SetActive(true);
        enemy2Mark.SetActive(true);
        enemy3Mark.SetActive(true);
    }
}
