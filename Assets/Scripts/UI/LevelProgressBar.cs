using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    private Slider progressSlider;

    private void OnEnable() { progressSlider = GetComponent<Slider>(); }

    public void UpdateProgress(float value) { progressSlider.value = value; }
}
