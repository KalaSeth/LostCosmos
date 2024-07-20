using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheRicalMove : MonoBehaviour
{
    public Transform sphereCenter; // Reference to the center of the sphere
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the direction of movement based on input
        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Calculate the forward direction based on the player's current position on the sphere
        Vector3 playerPosition = transform.position;
        Vector3 toCenter = (sphereCenter.position - playerPosition).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, toCenter).normalized;
        Vector3 forward = Vector3.Cross(toCenter, right).normalized;

        // Calculate the desired movement direction
        Vector3 moveDirection = (right * inputDirection.x + forward * inputDirection.z).normalized;

        // Calculate the new position and rotation
        Vector3 newPosition = playerPosition + moveDirection * speed * Time.deltaTime;
        Vector3 newToCenter = (sphereCenter.position - newPosition).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(newToCenter, right), newToCenter);

        // Update the player's position and rotation
        rb.MovePosition(newPosition);
        rb.MoveRotation(targetRotation);
    }
}
