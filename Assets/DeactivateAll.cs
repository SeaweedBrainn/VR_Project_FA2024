using UnityEngine;

public class DeactivateObjectsByTag : MonoBehaviour
{
    [SerializeField] public string targetTag = "room2Buttons"; // Set this to the tag you want to deactivate

    // This function sets all objects with the specified tag to inactive
    public void DeactivateAllWithTag()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(false);
        }

        Debug.Log($"All objects with tag '{targetTag}' have been deactivated.");
    }
}