using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEventTrigger : MonoBehaviour
{
    // The tag of the objects to detect collision with
    public string targetTag = "Player";

    // Unity Event to invoke upon collision with the target object
    public UnityEvent onTargetCollision;

    // This method will be called when the object collides with another
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Invoke the Unity Event
            onTargetCollision.Invoke();
        }
    }

    // Alternatively, you can use OnTriggerEnter if the objects have Trigger colliders
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the specified tag
        if (other.CompareTag(targetTag))
        {
            // Invoke the Unity Event
            onTargetCollision.Invoke();
        }
    }
}