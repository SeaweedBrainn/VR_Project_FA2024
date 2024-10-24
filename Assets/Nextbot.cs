using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nextbot : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private NavMeshAgent agent;

    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
    }
}
