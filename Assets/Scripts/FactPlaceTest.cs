using UnityEngine;

public class FactPlaceTest : MonoBehaviour
{
    public GameObject factory;
    private GameObject silhouetteCube;
    public GameObject placer;

    private void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (silhouetteCube == null)
            {

                silhouetteCube = Instantiate(factory);
            }

            PlaceSilhouetteCube(hitInfo.point);

            if (Input.GetMouseButtonDown(0))
            {
                PlaceCubeNear(hitInfo.point);
                placer.SetActive(false);
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

        var finalPosition = WorldBlockContainer.VecToGrid(clickPoint);
        silhouetteCube.transform.position = finalPosition;
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {

        var finalPosition = WorldBlockContainer.VecToGrid(clickPoint);
        factory.transform.position = finalPosition;
    }
}
