using UnityEngine;


public class SnapGrid : MonoBehaviour
{

    [SerializeField]
    private float size = 1f;

    public Vector3 GetNearestGridPoint(Vector3 position) {
         position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size
        );

        result += transform.position;

        return result; 
    }
}