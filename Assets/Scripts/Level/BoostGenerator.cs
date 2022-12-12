using System.Collections.Generic;
using UnityEngine;

public class BoostGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPath;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform boosts;

    [SerializeField] private GameObject boostPrefab;

    [SerializeField] private int distance;
    [SerializeField] private int maxPositionZ;

    [SerializeField] private float boostStep;

    private List<Path> paths = new List<Path>();

    private Transform currentBoost = null;

    private float boostP = .5f;
    private int boostS = 0;
    private float boostT = 0;

    private bool isEnabled = false;

    private void Awake() { EventManager.PathReadyEvent.AddListener(GenerateLevel); }

    private void FixedUpdate() { if (isEnabled) { if (playerTransform.position.z - boosts.GetChild(0).position.z > maxPositionZ) { MoveBoost(); }}}

    private void GenerateLevel() {
        GetPaths();

        for(; boostP < distance; boostP += boostStep) {
            boostS = Mathf.RoundToInt(Mathf.Floor(boostP));

            if (boostS != 0) { boostT = boostP % boostS; }
            else { boostT = boostP; }

            GameObject boost = Instantiate(boostPrefab, boosts);

            boost.transform.position = Bezier.GetPoint(paths[boostS].p0.position, paths[boostS].p1.position, paths[boostS].p2.position, paths[boostS].p3.position, boostT);
            boost.transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[boostS].p0.position, paths[boostS].p1.position, paths[boostS].p2.position, paths[boostS].p3.position, boostT)) * Quaternion.Euler(0, 0, Random.Range(0, 360));
        }

        isEnabled = true;
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void MoveBoost() {
        currentBoost = boosts.GetChild(0);

        boostS = Mathf.RoundToInt(Mathf.Floor(boostP));
        boostT = boostP % boostS;

        currentBoost.position = Bezier.GetPoint(paths[boostS].p0.position, paths[boostS].p1.position, paths[boostS].p2.position, paths[boostS].p3.position, boostT);
        currentBoost.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(paths[boostS].p0.position, paths[boostS].p1.position, paths[boostS].p2.position, paths[boostS].p3.position, boostT));
        
        currentBoost.SetAsLastSibling();
        boostP += boostStep;
    }
}
