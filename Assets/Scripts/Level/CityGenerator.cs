using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPath;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform city;

    [SerializeField] private GameObject cityPrefab;

    [SerializeField] private int distance;
    [SerializeField] private int maxPositionZ;

    [SerializeField] private float cityStep;

    private List<Path> paths = new List<Path>();

    private Transform currentLine = null;

    private float cityP = .5f;
    private int cityS = 0;
    private float cityT = 0;

    private bool isEnabled = false;

    private void Awake() { EventManager.PathReadyEvent.AddListener(GenerateCity); }

    private void FixedUpdate() { if (isEnabled) { if (playerTransform.position.z - city.GetChild(0).position.z > maxPositionZ) { MoveLine(); }}}

    private void GenerateCity() {
        GetPaths();

        for(; cityP < distance; cityP += cityStep) {
            cityS = Mathf.RoundToInt(Mathf.Floor(cityP));

            if (cityS != 0) { cityT = cityP % cityS; }
            else { cityT = cityP; }

            GameObject cityLine = Instantiate(cityPrefab, city);

            cityLine.transform.position = Bezier.GetPoint(paths[cityS].p0.position, paths[cityS].p1.position, paths[cityS].p2.position, paths[cityS].p3.position, cityT) + Vector3.down * 7.5f;
        }

        isEnabled = true;
    }

    private void GetPaths() {
        paths.Clear();
        for (int i = 0; i < PathGenerator.Instance.pathLength; i++) { paths.Add(levelPath.GetChild(i).GetComponent<Path>()); }
    }

    private void MoveLine() {
        currentLine = city.GetChild(0);

        cityS = Mathf.RoundToInt(Mathf.Floor(cityP));
        cityT = cityP % cityS;

        currentLine.position = Bezier.GetPoint(paths[cityS].p0.position, paths[cityS].p1.position, paths[cityS].p2.position, paths[cityS].p3.position, cityT);
        
        currentLine.SetAsLastSibling();
        cityP += cityStep;
    }
}
