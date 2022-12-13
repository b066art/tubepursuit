using UnityEngine;

public class PlayerVibration : MonoBehaviour
{
    private void Start() {
        EventManager.BoostEvent.AddListener(ShortVibrate);
        EventManager.CriminalCaughtEvent.AddListener(ShortVibrate);
        EventManager.DeadEvent.AddListener(LongVibrate);
        EventManager.HitEvent.AddListener(ShortVibrate);
        EventManager.JumpEvent.AddListener(DoubleVibrate);
    }

    private void LongVibrate() { if (SettingsMenu.Instance.vibration) { Vibration.Vibrate(750); }}

    private void ShortVibrate() { if (SettingsMenu.Instance.vibration) { Vibration.Vibrate(250); }}

    private void DoubleVibrate() {
        if (SettingsMenu.Instance.vibration) { 
            ShortVibrate();
            Invoke("ShortVibrate", 1f);
        }
    }
}
