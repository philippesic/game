using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class NodeGen : MonoBehaviour
{
    public List<GameObject> nodes = new();
    public float scatterRadius;
    public float rarity = 1.2f;



    void Start()
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData terrainData = terrain.terrainData;

        for (int i = 0; i < nodes.Count; i++)
        {

            GameObject node = nodes[i];
            NodeID nodeScript = node.GetComponent<NodeID>();
            nodeScript.id = AllGameData.itemIDs[node.name];

            float rarityMultiplier = (float)Math.Floor((i + 1) * rarity);

            int numberOfInstances = Mathf.RoundToInt(rarityMultiplier * 10);

            for (int j = 0; j < numberOfInstances; j++)
            {
                Vector3 randomPosition = new(
                    UnityEngine.Random.Range(0f, terrainData.size.x),
                    0f,
                    UnityEngine.Random.Range(0f, terrainData.size.z)
                    );

                if (terrain.SampleHeight(randomPosition) > 15)
                {
                    randomPosition.y = terrain.SampleHeight(randomPosition); //yes i know this is shit fix but unity hates destroying assets
                    GameObject newNodeInstance = Instantiate(node, randomPosition, Quaternion.identity);
                    newNodeInstance.transform.SetParent(transform);
                }
                else
                {
                    j--;
                }
            }
        }
    }

}

//items at a higher list position (eg list element 12) have higher number of instances
//i know fix is shit too bad