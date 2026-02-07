using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Collider playerCollider;
    private Rigidbody rb;
    private PlayerMovement movement;
    [SerializeField] private MonoBehaviour cameraLook;

    [SerializeField] private Canvas deathScreenUI;
    [SerializeField] private Transform respawnPoint;
    private Collider maliciousObject;

    public bool isDying;

    private void Start()
    {
        playerCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();

        isDying = false;
        deathScreenUI.gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        maliciousObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Malicious"))
        {
            maliciousObject = other;
            maliciousObject.enabled = false;
            Die();
        }
    }

    public void Die()
    {
        if (isDying) return;

        isDying = true;

        movement.enabled = false;
        cameraLook.enabled = false;

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        deathScreenUI.gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
{
    Debug.Log("respawn initiated");

    isDying = false;

    // set position
    rb.isKinematic = true; // temporarily freeze physics to prevent physics interference
    transform.position = respawnPoint.position;
    rb.velocity = Vector3.zero; // clear any residual movement

    // re-enable everything after a frame
    StartCoroutine(EnablePlayerNextFrame());

    deathScreenUI.gameObject.SetActive(false);

    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;

    maliciousObject.enabled = true; 
}

private IEnumerator EnablePlayerNextFrame()
{
    yield return null; // wait one frame
    movement.enabled = true;
    cameraLook.enabled = true;
    rb.isKinematic = false;
}

}
