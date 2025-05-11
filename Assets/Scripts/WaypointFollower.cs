using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints; //with SerializeField, you can change it in the editor
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    private void FixedUpdate()
    {
        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f) //true wenn sich Plattform und Waypoint praktisch berühren
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed); //Time.deltaTime to make it independent from frame rate
        
    }

    public GameObject[] GetWaypoints(){
        return waypoints;
    }

    public void SwapFirstTwoWaypoints(GameObject w1, GameObject w2){
        if(waypoints[0] != w1){
            waypoints[0] = w1;
            waypoints[1] = w2;
        }
        else{
            waypoints[1] = w1;
            waypoints[0] = w2;
        }
    }

    public void IncreaseSpeedBy(int increase){
        speed = speed + increase;
    }

    public void Freeze(){
        speed = 0;
    }
}
