using DG.Tweening;
using UnityEngine;

public class BikeWiggle : MonoBehaviour
{
    [SerializeField] float amplitudeMax = .15f;
    private Transform bikeModel;

    private void Start() {
        bikeModel = transform.Find("Model");
        bikeModel.DOShakePosition(1f, new Vector3(Random.Range(-amplitudeMax, amplitudeMax), Random.Range(-amplitudeMax, amplitudeMax), 0), 1, 90f).SetLoops(-1);
    }

    private void OnDestroy() { bikeModel.DOKill(); }
}
