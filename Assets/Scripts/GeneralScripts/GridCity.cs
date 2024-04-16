// Version 2023
//  (Updates: supports different root positions)
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class GridCity : MonoBehaviour
    {
        [Header("How many buildings do you want to spawn??")]
        [SerializeField]
        private int numberOfBuildings = 50;
        [SerializeField]
        private float areaWidth = 100f;
        [SerializeField]
        private float areaHeight = 100f;
        public GameObject[] buildingPrefabs;
        [SerializeField]
        private float minimumDistance = 10f;

        [Header("Unique Buildings")]
        [SerializeField]
        private GameObject smallPalacePrefab;
        [SerializeField]
        private int numberOfSmallPalace;
        [SerializeField]
        private GameObject bigPalacePrefab;
        [SerializeField]
        private int numberOfBigPalace;


        public float buildDelaySeconds = 0.1f;

        private List<Vector3> placedPositions = new List<Vector3>();

        void Start()
        {
            Generate();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                DestroyChildren();
                Generate();
            }
        }

        void DestroyChildren()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        void Generate()
        {
            int attempts, maxAttempts = 1000;
            int placedSmallPalaces = 0, placedBigPalaces = 0;

            int totalBuildings = numberOfBuildings + numberOfSmallPalace + numberOfBigPalace;

            for (int i = 0; i < totalBuildings; i++)
            {
                GameObject newBuilding = null;
                Vector3 potentialPosition;
                bool positionFound = false;
                attempts = 0;

                if (placedSmallPalaces < numberOfSmallPalace)
                {
                    if (smallPalacePrefab != null)
                    {
                        newBuilding = Instantiate(smallPalacePrefab, transform);
                        placedSmallPalaces++;
                    }
                }
                else if (placedBigPalaces < numberOfBigPalace)
                {
                    if (bigPalacePrefab != null)
                    {
                        newBuilding = Instantiate(bigPalacePrefab, transform);
                        placedBigPalaces++;
                    }
                }
                else
                {
                    newBuilding = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Length)], transform);
                }

                while (!positionFound && attempts < maxAttempts)
                {
                    potentialPosition = new Vector3(
                        Random.Range(-areaWidth / 2f, areaWidth / 2f), 0,
                        Random.Range(-areaHeight / 2f, areaHeight / 2f));

                    if (IsPositionValid(potentialPosition))
                    {
                        newBuilding.transform.localPosition = potentialPosition;
                        newBuilding.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                        placedPositions.Add(potentialPosition);
                        positionFound = true;
                    }
                    attempts++;
                }

                if (!positionFound)
                {
                    Debug.LogWarning("Max placement attempts reached for a building; it may not be placed.");
                    Destroy(newBuilding);
                }
                else
                {
                    Shape shape = newBuilding.GetComponent<Shape>();
                    if (shape != null)
                    {
                        shape.Generate(buildDelaySeconds);
                    }
                }
            }
        }


        bool IsPositionValid(Vector3 position)
        {
            foreach (Vector3 otherPosition in placedPositions)
            {
                if (Vector3.Distance(position, otherPosition) < minimumDistance)
                {
                    return false;
                }
            }
            return true;
        }
    }
}