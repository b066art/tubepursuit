using DG.Tweening;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    private void Start() {
        SetTimeScale();
        SetTargetFrameRate();
        SetDOTweenCapacity();
    }

    private void SetTimeScale() { Time.timeScale = 1f; }

    private void SetTargetFrameRate() { Application.targetFrameRate = 120; }

    private void SetDOTweenCapacity() { DOTween.SetTweensCapacity(250, 125); }
}
