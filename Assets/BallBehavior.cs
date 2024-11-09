using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject teleportTarget;  // GameObject to teleport the player to
    public float disappearTime = 3.0f; // Time after which the ball disappears

    private void Start()
    {
        // Destroy the ball after the specified time
        Destroy(gameObject, disappearTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Check if the ball hits the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Teleport the player to the teleport target position
            //if (teleportTarget != null)
                // Teleport the player to the target position
                other.gameObject.transform.position = teleportTarget.transform.position;
                Destroy(gameObject);
            
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}