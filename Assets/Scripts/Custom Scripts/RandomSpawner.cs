using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> leafPrefabs;

    [Header("Number of Itemes to spawn per click")]
    [SerializeField]
    [Range(1, 10)]
    private int spawnNumber = 1;


    [Header("Adjust the range of the X Positions")]
    [SerializeField]
    [Range(-20, 0)]
    private float minimumSizeX = -20;
    [SerializeField]
    [Range(0, 20)]
    private float maximumSizeX = 20;

    [Header("Adjust how high the objects spawn")]
    [SerializeField]
    [Range(0, 20)]
    private float spawnHeight = 5;

    [Header("Adjust the range of the Z Positions")]
    [SerializeField]
    [Range(-20, 0)]
    private float minimumSizeZ = -20;
    [Range(0, 20)]
    private float maximumSizeZ = 20;


    private void Update()
    {
        GeneratePrefabsInRandomPositions();
    }

    public void GeneratePrefabsInRandomPositions()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (leafPrefabs != null)
            {
                Vector3 randomPosition = new Vector3(Random.Range(minimumSizeX, maximumSizeX), spawnHeight, Random.Range(minimumSizeZ, maximumSizeZ));
                Quaternion randomRot = Quaternion.AngleAxis(Random.Range(0, 360),new Vector3(0,1,0));
                int leafIndex = Random.Range(0, leafPrefabs.Count);
                for (int i = 0; i < spawnNumber; i++)
                {
                    GameObject leafObject = Instantiate(leafPrefabs[leafIndex], randomPosition, randomRot);
                    leafObject.transform.parent = transform.parent;
                }
                Debug.Log($"Instanciating object: {leafPrefabs}");
            }
        }
    }
}
