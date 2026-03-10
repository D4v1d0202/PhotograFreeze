using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerReset : MonoBehaviour
{
    private PlayerLife player;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private Transform[] StartPositions;

    private Vector3[] fixedStartPositions;

    void Start()
    {
        player = GetComponent<PlayerLife>();

        fixedStartPositions = new Vector3[StartPositions.Length];
        for(int i = 0; i < StartPositions.Length; i++)
        {
            fixedStartPositions[i] = StartPositions[i].position;
        }
    }

    void Update()
    {
        if(player.isDying)
        {
            for(int i = 0; i < Enemies.Length; i++){
                if(Enemies[i].GetComponent<WaypointFollower>().enabled)
                {
                    Enemies[i].GetComponent<WaypointFollower>().enabled = false;
                    Enemies[i].transform.position = fixedStartPositions[i];
                }
            }
        }
    }
}