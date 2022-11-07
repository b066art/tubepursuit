using DG.Tweening;
using UnityEngine;

public class BikeWiggle : MonoBehaviour
{
    private Transform bikeModel;

    private void Start() {
        bikeModel = transform.Find("Model");
        bikeModel.DOShakePosition(1f, new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0), 1, 90f).SetLoops(-1);
    }
}
