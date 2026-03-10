using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [Header("Movement")]
    [SerializeField] private float speed = 2f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180f;

    [Header("Tick Settings")]
    [SerializeField] private bool useTickDelay = false;  
    [SerializeField] private float tickDelay = 0.5f;     

    private bool waitingForNextTick = false;

    private float preFreezeChangeSpeed = 0f;
    private float preFreezeChangeRotationSpeed = 0f;
    private int freezeCount = 0;
    private bool tickCoroutineRunning = false;


    private void FixedUpdate()
{
    if (freezeCount > 0) return;

    if (waitingForNextTick) return; // stop all motion while waiting for tick

    Transform target = waypoints[currentWaypointIndex].transform;

    // MOVE
    transform.position = Vector3.MoveTowards(
        transform.position,
        target.position,
        Time.fixedDeltaTime * speed
    );

    // ROTATE
if (!useTickDelay)
{
    // normal smooth rotation (non-clock objects)
    transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        target.rotation,
        Time.fixedDeltaTime * rotationSpeed
    );
}


    bool positionReached = Vector3.Distance(target.position, transform.position) < 0.05f;

    bool arrived = useTickDelay ? positionReached :
    positionReached && Quaternion.Angle(transform.rotation, target.rotation) < 3f;

if (arrived && !tickCoroutineRunning)

{
    if (useTickDelay)
    {
        tickCoroutineRunning = true;
        StartCoroutine(TickPause());
    }
    else
    {
        AdvanceWaypoint();
    }
}

}


    IEnumerator TickPause()
{
    waitingForNextTick = true;

    float timer = 0f;

    while (timer < tickDelay)
    {
        if (freezeCount == 0)
            timer += Time.deltaTime;

        yield return null;
    }

    waitingForNextTick = false;

    AdvanceWaypoint();

    tickCoroutineRunning = false;
}



    void AdvanceWaypoint()
{
    if (useTickDelay)
    {
        // snap to CURRENT waypoint before advancing
        transform.rotation = waypoints[currentWaypointIndex].transform.rotation;
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }

    currentWaypointIndex++;

    if (currentWaypointIndex >= waypoints.Length)
        currentWaypointIndex = 0;
}


    public void Freeze()
    {
        freezeCount++;
        preFreezeChangeSpeed = speed;
        speed = 0f;
        preFreezeChangeRotationSpeed = rotationSpeed;
        rotationSpeed = 0f;
    }

    public void Unfreeze()
    {
        freezeCount = Mathf.Max(0, freezeCount - 1);
        if (freezeCount == 0)
        {
            speed = preFreezeChangeSpeed > 0f ? preFreezeChangeSpeed : 2f;
            rotationSpeed = preFreezeChangeRotationSpeed > 0f ? preFreezeChangeRotationSpeed : 180f;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
