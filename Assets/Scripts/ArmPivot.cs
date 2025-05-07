using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPivot : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform arm;

    void LateUpdate()
    {
            Vector3 armEuler = arm.eulerAngles;
            Vector3 cameraEuler = camera.eulerAngles;

            // Keep X and Z, only take Y
            arm.rotation = Quaternion.Euler(armEuler.x, cameraEuler.y, armEuler.z);
    }
}
