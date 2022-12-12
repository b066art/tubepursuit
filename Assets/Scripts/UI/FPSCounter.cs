using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public static FPSCounter Instance;

    [SerializeField] private Toggle toggleFPS;

    private TMP_Text fpsText;
    private StringBuilder outputText = new StringBuilder(10);

    private int fps;

    private float cooldownTime = .2f;
    private float lastShowTime = 0;

    private bool showFPS = false;

    private void Awake() { Instance = this; }

    private void Start() {
        showFPS = SettingsMenu.Instance.fps;
        fpsText = GetComponent<TMP_Text>();
    }

    private void Update() {
        if (showFPS) {
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

    public void ChangeState() {
        showFPS = SettingsMenu.Instance.fps;
        fpsText.text = null;
    } 
}
