using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StickyPlatformTracker : MonoBehaviour
{
    private MovingPlatform currentPlatform;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovingPlatform>(out var platform))
        {
            currentPlatform = platform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovingPlatform>() == currentPlatform)
        {
            currentPlatform = null;
        }
    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            rb.position += currentPlatform.Velocity * Time.fixedDeltaTime;
        }
    }
}
