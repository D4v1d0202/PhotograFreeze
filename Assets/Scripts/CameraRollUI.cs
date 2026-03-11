using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRollUI : MonoBehaviour
{
    public PhotoRaycaster photoRaycaster; 
    public RawImage photoPreview;
    public Button nextButton;
    public Button prevButton;
    public Button deleteButton;

    private bool currentlyEnabled = false;
    private int currentIndex = 0;
    [SerializeField] private GameObject Player;

    void Start()
    {
        CloseMenu();
        SetupButtons();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !Player.GetComponent<PlayerLife>().isDying)
        {
            if(currentlyEnabled)
                CloseMenu();
            else
                OpenMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && currentlyEnabled)
            CloseMenu();
    }

    void OpenMenu()
{
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None; // unlock cursor
    GetComponent<Canvas>().enabled = true;
    GetComponent<GraphicRaycaster>().enabled = true;
    currentlyEnabled = true;
    Time.timeScale = 0f; // freeze while viewing

    UpdatePreview();
}

void CloseMenu()
{
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked; // lock cursor again
    GetComponent<Canvas>().enabled = false;
    GetComponent<GraphicRaycaster>().enabled = false;
    currentlyEnabled = false;
    Time.timeScale = 1f;
}


    void SetupButtons()
    {
        nextButton.onClick.AddListener(NextPhoto);
        prevButton.onClick.AddListener(PrevPhoto);
        deleteButton.onClick.AddListener(DeletePhoto);
    }

    void UpdatePreview()
    {
        if(photoRaycaster.cameraRoll.Count == 0)
        {
            photoPreview.texture = null;
            return;
        }

        // clamp index
        currentIndex = Mathf.Clamp(currentIndex, 0, photoRaycaster.cameraRoll.Count - 1);

        // show photo
        photoPreview.texture = photoRaycaster.cameraRoll[currentIndex].screenshot;
    }

    public void NextPhoto()
    {
        if(photoRaycaster.cameraRoll.Count == 0) return;

        currentIndex++;
        if(currentIndex >= photoRaycaster.cameraRoll.Count)
            currentIndex = 0; 

        UpdatePreview();
    }

    public void PrevPhoto()
    {
        if(photoRaycaster.cameraRoll.Count == 0) return;

        currentIndex--;
        if(currentIndex < 0)
            currentIndex = photoRaycaster.cameraRoll.Count - 1; 

        UpdatePreview();
    }

    public void DeletePhoto()
    {
        if(photoRaycaster.cameraRoll.Count == 0) return;

        photoRaycaster.DeletePhoto(currentIndex);

        if(currentIndex >= photoRaycaster.cameraRoll.Count)
            currentIndex = photoRaycaster.cameraRoll.Count - 1;

        UpdatePreview();
    }
}
