using DG.Tweening;
using UnityEngine;

public class BikeWiggle : MonoBehaviour
{
    [SerializeField] Transform bikeModel;
    [SerializeField] float strength = .15f;

    private void Start() { bikeModel.DOShakePosition(1f, strength, 1, 90f).SetLoops(-1); }

    private void OnDestroy() { bikeModel.DOKill(); }
}