using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhotoData
{
    public Texture2D screenshot;
    public List<WaypointFollower> frozenTargets = new List<WaypointFollower>();
}

public class PhotoRaycaster : MonoBehaviour
{
    public Camera photoCam;
    public int raysPerAxis = 10;
    public float rayDistance = 100f;
    public float photoCooldown = 1f;

    private float lastPhotoTime = -Mathf.Infinity; // Spieler kann direkt nach Spawn Foto machen
    public Canvas viewfinderCanvas;

    // CAMERA ROLL
    public List<PhotoData> cameraRoll = new List<PhotoData>();

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

                    WaypointFollower follower = hit.collider.GetComponentInParent<WaypointFollower>();
                    if (follower != null)
                    {
                        targetsToFreeze.Add(follower);
                    }
                }
            }
        }

        // Freeze all detected objects and store them in a list
List<WaypointFollower> frozenList = new List<WaypointFollower>();
foreach (var follower in targetsToFreeze)
{
    follower.Freeze();
    frozenList.Add(follower);
    Debug.Log("Froze: " + follower.name);
}

        // Capture screenshot after freezing objects
        StartCoroutine(CaptureScreenshot((Texture2D tex) =>
        {
            PhotoData newPhoto = new PhotoData();
            newPhoto.screenshot = tex;
            newPhoto.frozenTargets = frozenList;

            cameraRoll.Add(newPhoto);

            Debug.Log("Photo saved. Camera roll size: " + cameraRoll.Count);
        }));
    }

    IEnumerator CaptureScreenshot(System.Action<Texture2D> onDone)
    {
        viewfinderCanvas.enabled = false;

        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        onDone?.Invoke(tex);

        viewfinderCanvas.enabled = true;
    }

    // Delete a photo from the camera roll and unfreeze its objects
    public void DeletePhoto(int index)
    {
        if (index < 0 || index >= cameraRoll.Count) return;

        PhotoData photo = cameraRoll[index];

        foreach (var follower in photo.frozenTargets)
{
    if (follower != null)
        follower.Unfreeze(); 
}


        cameraRoll.RemoveAt(index);

        Debug.Log("Photo deleted. Camera roll size: " + cameraRoll.Count);
    }
}
