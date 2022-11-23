using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject playerMark;
    [SerializeField] private GameObject enemyMark;

    private RectTransform playerMarkRT;
    private RectTransform enemyMarkRT;

    private float startPositionX = -580f;
    private float endPositionX = 520f;

    private void Start() {
        playerMarkRT = playerMark.GetComponent<RectTransform>();
        enemyMarkRT = enemyMark.GetComponent<RectTransform>();

        EventManager.LevelFinishEvent.AddListener(HideBar);
        EventManager.LevelStartEvent.AddListener(ShowBar);
    }

    public void UpdateBar(float playerProgress, float enemyProgress) {
        playerMarkRT.anchoredPosition = Vector3.right * (startPositionX + (endPositionX - startPositionX) * playerProgress);
        enemyMarkRT.anchoredPosition = Vector3.right * (startPositionX + (endPositionX - startPositionX) * enemyProgress);
    }

    private void HideBar() {
        bg.SetActive(false);
        playerMark.SetActive(false);
        enemyMark.SetActive(false);
    }

    private void ShowBar() {
        bg.SetActive(true);
        playerMark.SetActive(true);
        enemyMark.SetActive(true);
    }
}
