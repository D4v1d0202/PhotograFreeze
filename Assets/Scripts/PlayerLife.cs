using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Collider playerCollider;
    private Rigidbody rb;
    private PlayerMovement movement;
    [SerializeField] private MonoBehaviour cameraLook; // drag your camera script here

    [SerializeField] private Canvas deathScreenUI;
    [SerializeField] private Transform respawnPoint;
    private Collider maliciousObject;

    private bool isDying;

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
        playerCollider.enabled = false;

        deathScreenUI.gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        isDying = false;

        movement.enabled = true;
        cameraLook.enabled = true;

        rb.isKinematic = false;
        playerCollider.enabled = true;

        deathScreenUI.gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        maliciousObject.enabled = true; 
    }
}
