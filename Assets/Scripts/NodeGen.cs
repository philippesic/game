using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class NodeGen : MonoBehaviour
{
    public List<GameObject> nodes = new();
    public float scatterRadius;

    

    void Start()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData terrainData = terrain.terrainData;

        for (int i = 0; i < nodes.Count; i++)
        {

            GameObject prefab = nodes[i];

            float rarityMultiplier = i + 100;

            int numberOfInstances = Mathf.RoundToInt(rarityMultiplier * 10);

            for (int j = 0; j < numberOfInstances; j++)
            {
                float randomX = UnityEngine.Random.Range(0f, terrainData.size.x);
                float randomZ = UnityEngine.Random.Range(0f, terrainData.size.z);

                Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

                if (terrain.SampleHeight(randomPosition) > 15) {
                    randomPosition.y = terrain.SampleHeight(randomPosition); //yes i know this is shit fix but unity hates destroying assets
                    GameObject newPrefabInstance = Instantiate(prefab, randomPosition, Quaternion.identity);
                    newPrefabInstance.transform.SetParent(transform);
                }

            }
        }
    }

}

//items at a higher list position (eg list element 12) have higher number of instances
//i know fix is shit too bad