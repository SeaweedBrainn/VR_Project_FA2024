using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setYPosition : MonoBehaviour

{
    public float newYPosition;
    public void setY()
    {
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}
