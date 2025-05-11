using System.Collections.Generic;
using UnityEngine;

public class PhotoRaycaster : MonoBehaviour
{
    public Camera photoCam;
    public int raysPerAxis = 10;
    public float rayDistance = 100f;
    public float photoCooldown = 1f;

    private float lastPhotoTime = -Mathf.Infinity; // Spieler kann direkt nach Spawn Foto machen

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetMouseButton(1))
        {
            if (Time.time - lastPhotoTime >= photoCooldown)
            {
                ShootPhoto();
                lastPhotoTime = Time.time;
            }
        }
    }

    void ShootPhoto()
    {
        HashSet<WaypointFollower> targetsToFreeze = new HashSet<WaypointFollower>();

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

                    WaypointFollower follower = hit.collider.GetComponent<WaypointFollower>();
                    if (follower != null)
                    {
                        targetsToFreeze.Add(follower);
                    }
                }
            }
        }

        // Now freeze each object only once
        foreach (var follower in targetsToFreeze)
        {
            follower.FreezeChange();
            Debug.Log("Froze: " + follower.name);
        }
    }
}
