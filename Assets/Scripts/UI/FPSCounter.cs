using System.Text;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text fpsText;
    private StringBuilder outputText = new StringBuilder(10);

    private int fps;

    private float cooldownTime = .25f;
    private float lastShowTime = 0;

    private void Start() { fpsText = GetComponent<TMP_Text>(); }

    private void Update() {
        if (lastShowTime > cooldownTime) {
            fps = Mathf.RoundToInt(1.0f / Time.deltaTime);

            outputText.Length = 0;
            outputText.Append("FPS: ");
            outputText.Append(fps);

            fpsText.text = outputText.ToString();
            lastShowTime = 0;
        }

        lastShowTime += Time.deltaTime;
    }
}
