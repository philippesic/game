using UnityEngine;

public class FactPlaceTest : MonoBehaviour
{
    public SnapGrid grid;
    public GameObject silhouetteCubePrefab; 
    private GameObject silhouetteCube; 

    private void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (silhouetteCube == null)
            {
                
                silhouetteCube = Instantiate(silhouetteCubePrefab);
            }

            PlaceSilhouetteCube(hitInfo.point);
            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceCubeNear(hitInfo.point);
            }
        }
        else
        {
            
            if (silhouetteCube != null)
            {
                Destroy(silhouetteCube);
                silhouetteCube = null;
            }
        }
    }

    private void PlaceSilhouetteCube(Vector3 clickPoint)
    {
        
        var finalPosition = grid.GetNearestGridPoint(clickPoint);
        silhouetteCube.transform.position = finalPosition;
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        
        var finalPosition = grid.GetNearestGridPoint(clickPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
}
