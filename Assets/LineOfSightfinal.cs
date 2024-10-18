using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineOfSightfinal : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject targetObject;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Debug.Log("Line of Sight : Target : " + (targetObject != null ? targetObject.name : "null"));
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshHit hit;

        Debug.Log("Sampling Navmesh at: " + targetObject.transform.position);
        if (NavMesh.SamplePosition(targetObject.transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {

            Vector3 direction = hit.position - targetObject.transform.position;

            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, direction.normalized, out raycastHit, direction.magnitude))
            {
                if (raycastHit.transform == targetObject)
                {
                    Debug.Log("Target is in Line of Sight");

                }
                Debug.Log("Target isn't in Line of Sight");
            }
            else
            {
                Debug.Log("Target is in Line of Sight");

            }
        }
        else {
            Debug.Log("Not Even Entering Condition CASE");
        }
        
    }
}
