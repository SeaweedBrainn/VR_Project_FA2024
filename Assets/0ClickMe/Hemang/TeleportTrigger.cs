using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    // Assign the teleport location in the Unity editor
    public Transform teleportDestination;

    // Reference to the player’s root object (XR Rig or whatever moves the player)
    public Transform playerRoot;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that enters the trigger is the VR player
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the teleport destination
            playerRoot.position = teleportDestination.position;

            // Get the direction from player to the world origin (0,0,0)
            Vector3 directionToFace = Vector3.zero - playerRoot.position;
            directionToFace.y = 0;  // Keep the rotation horizontal, ignore vertical rotation

            // Rotate the player's root object to face the origin
            Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
            playerRoot.rotation = targetRotation;
        }
    }
}