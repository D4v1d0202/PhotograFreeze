using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAfterEnemiesDefeated : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;

    void Update()
    {
        if(EnemiesDefeated())
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
        else{
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    bool EnemiesDefeated(){
                int count = 0;
                foreach(GameObject i in Enemies){
                    if(i.GetComponent<WaypointFollower>().GetSpeed() == 0)
                        count++;
                }
                if(count == Enemies.Length)
                    return true;
                else
                    return false;
    }

    private void OnTriggerEnter(Collider other)
    {
            foreach(GameObject i in Enemies){
                i.GetComponent<WaypointFollower>().enabled = false;
            }
    }
}
