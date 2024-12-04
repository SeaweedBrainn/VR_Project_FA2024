using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInRoom : MonoBehaviour
{
    private Transform t;
    private Vector3 pos;

    private void Start()
    {
        t = GetComponent<Transform>();
        pos = t.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollisionBox"))
        {
            Debug.Log(other.name);
            t.position = pos;
        }
    }
    
}
