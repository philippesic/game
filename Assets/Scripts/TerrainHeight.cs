using UnityEngine;

public class TerrainHeight : MonoBehaviour
{
    public int resolution;
    public float scale = 1f;
    public float noiseIntensity = 0.1f;


    void Start()
    {
        TerrainData terrainData = new()
        {
            heightmapResolution = resolution,
            size = new Vector3(transform.localScale[0], 600, transform.localScale[0])
        };
        // resolution = (int) transform.localScale[0] / terrainData.heightmapResolution;
        float[,] heights = GenerateHeights();
        terrainData.SetHeights(0, 0, heights);

        GetComponent<Terrain>().terrainData = terrainData;
        GetComponent<TerrainCollider>().terrainData = terrainData;
    }

    float[,] GenerateHeights()
    {
        float offsetX = Random.Range(0f, 99999f);
        float offsetY = Random.Range(0f, 99999f);
        float[,] heights = new float[resolution, resolution];
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                heights[x, y] = Mathf.PerlinNoise(
                    (float)x / resolution * scale + offsetX,
                    (float)y / resolution * scale + offsetY
                    ) * noiseIntensity;
            }
        }
        return heights;
    }
}