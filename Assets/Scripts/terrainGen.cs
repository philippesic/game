using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 512;       
    public int height = 512;      
    public float scale = 10f;     
    public float offsetX = 100f;  
    public float offsetY = 100f;  
    public float noiseIntensity = 0.1f; 

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();

        
        TerrainData terrainData = new TerrainData();

        
        terrainData.heightmapResolution = width;
        terrainData.size = new Vector3(width, 600, height);

        
        float[,] heights = GenerateHeights();
        terrainData.SetHeights(0, 0, heights);

        
        terrain.terrainData = terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                
                heights[x, y] = noiseValue * noiseIntensity;
            }
        }

        return heights;
    }
}