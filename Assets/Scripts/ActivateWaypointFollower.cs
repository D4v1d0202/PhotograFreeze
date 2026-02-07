using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectsToActivate;

    private void OnTriggerEnter(Collider other)
    {
            foreach(GameObject i in ObjectsToActivate){
                i.GetComponent<WaypointFollower>().enabled = true;
            }
        }
    }
