using UnityEngine;

public class FollowParentPosition : MonoBehaviour
{
    public Transform parentObject; // Assign the parent object in the Inspector

    void Start()
    {
        // Scale the object along the Y-axis to 400 while keeping the X and Z the same
        Vector3 newScale = transform.localScale;
        newScale.y = 400; // Set the Y-axis to 400
        transform.localScale = newScale;
    }

    void LateUpdate()
    {
        // Sync the child’s position to the parent’s position, but don't change rotation
        transform.position = parentObject.position;
    }
}