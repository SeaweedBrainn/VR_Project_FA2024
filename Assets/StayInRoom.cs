using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StayInRoom : MonoBehaviour
{
    private Transform t;
    private Vector3 pos;
    private Quaternion dir;

    private void Start()
    {
        t = GetComponent<Transform>();
        pos = t.position;
        dir = t.rotation;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollisionBox"))
        {
            Debug.Log(other.name);
            t.position = pos;
            t.rotation = dir;
        }
    }
    
}
