using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject viewfinder;
    [SerializeField] private GameObject arm;

    private bool usingCamera = false;
    
    void Start()
    {
        viewfinder.SetActive(false);
        arm.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) && !usingCamera)
        {
            viewfinder.SetActive(true);
            arm.SetActive(false);
            usingCamera = true;
        }
        else if(!Input.GetMouseButton(1) && usingCamera)
        {
            viewfinder.SetActive(false);
            arm.SetActive(true);
            usingCamera = false;
        }
    }
}
