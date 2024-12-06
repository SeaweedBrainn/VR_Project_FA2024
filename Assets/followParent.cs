using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followParent : MonoBehaviour
{
    public Transform parentObject;
    public Vector3 offset = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(parentObject.position.x+offset.x, transform.position.y, parentObject.position.z+offset.z);
        transform.rotation = parentObject.rotation;
    }
}
