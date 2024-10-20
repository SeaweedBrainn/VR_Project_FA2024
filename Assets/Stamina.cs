using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarFollow : MonoBehaviour
{
    public Transform playerHead;   // The VR camera or player's head
    public float distanceFromHead = 1.0f;  // Fixed distance in front of the player's head
    public float minHeightAboveFloor = 0.5f;  // Minimum height to prevent it from going below the floor
    public float heightOffset = -0.3f;  // Vertical offset to keep the bar in a comfortable position
    public float followSpeed = 5f;  // Speed of smooth following

    void Update()
    {
        // Get the position in front of the player's head
        Vector3 forward = playerHead.forward;
        Vector3 targetPosition = playerHead.position + forward * distanceFromHead;

        // Adjust the vertical offset
        targetPosition.y = Mathf.Max(playerHead.position.y + heightOffset, minHeightAboveFloor);

        // Smoothly move the canvas to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Always make the canvas face the player
        transform.LookAt(playerHead);
        transform.Rotate(0, 180f, 0);  // Flip the canvas to face the player properly
    }
}

