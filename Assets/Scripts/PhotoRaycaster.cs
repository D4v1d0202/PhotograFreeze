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
    public int raysPerAxis = 250000;
    public float rayDistance = 200f;
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

    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(photoCam);

    WaypointFollower[] allFollowers = FindObjectsOfType<WaypointFollower>();

    foreach (var follower in allFollowers)
    {
        Renderer rend = follower.GetComponentInChildren<Renderer>();
        if (rend == null) continue;

        // check if inside camera view
        if (!GeometryUtility.TestPlanesAABB(planes, rend.bounds))
            continue;

        // check if visible (not behind wall)
        Vector3 dir = (rend.bounds.center - photoCam.transform.position).normalized;
        float dist = Vector3.Distance(photoCam.transform.position, rend.bounds.center);

        if (Physics.Raycast(photoCam.transform.position, dir, out RaycastHit hit, dist))
        {
            if (hit.collider.GetComponentInParent<WaypointFollower>() == follower)
            {
                targetsToFreeze.Add(follower);
            }
        }
    }

    // Freeze all detected objects
    List<WaypointFollower> frozenList = new List<WaypointFollower>();
    foreach (var follower in targetsToFreeze)
    {
        follower.Freeze();
        frozenList.Add(follower);
        Debug.Log("Froze: " + follower.name);
    }

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
