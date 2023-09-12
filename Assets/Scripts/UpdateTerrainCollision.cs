using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTerrainCollision : MonoBehaviour
{
    public Terrain terrain;
    private TerrainCollider terrainCollider;

    void Start()
    {
        terrainCollider = terrain.GetComponent<TerrainCollider>();
        UpdateTerrainCollider();
    }

    void UpdateTerrainCollider()
    {
        terrainCollider.terrainData = terrain.terrainData;
    }

}
