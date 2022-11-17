using System.Collections;
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
        StartCoroutine(TimerToHide());
    }

    private IEnumerator TimerToHide() {
        yield return new WaitForSeconds(10f);
        HideTutorial();
    }
}
