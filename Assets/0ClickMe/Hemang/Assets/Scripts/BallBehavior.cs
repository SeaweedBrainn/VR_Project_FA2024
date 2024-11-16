using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject teleportTarget;  // GameObject to teleport the player to
    public float disappearTime = 3.0f; // Time after which the ball disappears
    public float bounceForce = 10f;    // Force applied when bouncing off an obstacle
    public AudioSource hitNoise;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Destroy the ball after the specified time
        Destroy(gameObject, disappearTime);
    }

    private void BounceOffObstacle()
    {
        // Generate random angles for both horizontal and vertical bounce directions
        float randomHorizontalAngle = Random.Range(-45f, 45f); // Left/right angle
        float randomVerticalAngle = Random.Range(-30f, 30f);   // Up/down angle

        // Calculate new direction by rotating the current velocity
        Vector3 newDirection = Quaternion.Euler(randomVerticalAngle, randomHorizontalAngle, 0) * -rb.velocity.normalized;

        // Set the ball's velocity to zero and apply the new direction with force
        rb.velocity = Vector3.zero;
        rb.AddForce(newDirection * bounceForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            BounceOffObstacle();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hitNoise.Play();
            other.gameObject.transform.position = teleportTarget.transform.position;
        }
        else
        {
            BounceOffObstacle();
        }
    }
}