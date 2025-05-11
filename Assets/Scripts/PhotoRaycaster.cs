using UnityEngine;

public class PhotoRaycaster : MonoBehaviour
{
    public Camera photoCam;
    public int raysPerAxis = 10;
    public float rayDistance = 100f;

    void ShootPhoto()
    {
        for (int x = 0; x < raysPerAxis; x++)
        {
            for (int y = 0; y < raysPerAxis; y++)
            {
                float u = (x + 0.5f) / raysPerAxis;
                float v = (y + 0.5f) / raysPerAxis;

                Ray ray = photoCam.ViewportPointToRay(new Vector3(u, v, 0));
                if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);

                    // Attempt to freeze object
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject != null && hitObject.GetComponent<WaypointFollower>() != null)
                    {
                        hitObject.GetComponent<WaypointFollower>().Freeze();
                    }
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetMouseButton(1)) // Left-click to "take photo"
        {
            ShootPhoto();
        }
    }
}
