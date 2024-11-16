using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject player;          // Reference to the player (XR Rig or player GameObject)
    public GameObject ballPrefab;      // Prefab for the turret's ball
    public Transform firePoint;        // Fire point from where the balls will be shot
    public GameObject teleportTarget;  // GameObject to teleport the player to when hit
    public float fireRate = 1.0f;      // Fire rate of the turret
    public float force = 10f;
    public Vector3 activationRange = new Vector3(10, 5, 10); // Range to activate turret

    private float nextFireTime = 0f;

    void Update()
    {
        // Check if the player is within activation range
        if (Vector3.Distance(player.transform.position, transform.position) <= activationRange.magnitude)
        {
            // Shoot if it's time
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        // Create a new ball and get its Rigidbody component
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Calculate direction to the player
        Vector3 direction = (player.transform.position - firePoint.position).normalized;

        // Add force to the ball to shoot it towards the player
        rb.AddForce(direction * force, ForceMode.Impulse); // Adjust the speed (10f) as needed

        // Add the BallController script to the ball and set teleport target
        ball.GetComponent<BallController>().teleportTarget = teleportTarget;
    }
}