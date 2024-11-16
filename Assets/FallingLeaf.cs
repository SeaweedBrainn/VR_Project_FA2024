using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FallingLeaf : MonoBehaviour
{
    System.Random rnd = new System.Random();
    public int mapRange = 100;
    public float posx;
    public float posz;
    private float lastActionTime; // Store the last action time
    private Rigidbody rb;
    private Transform leaf;
    public float y = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        leaf = GetComponent<Transform>();
        lastActionTime = Time.time; // Initialize last action time
    }

    void Update()
    {
        // Check if agent has stopped and if enough time has passed
        if (rb.velocity.sqrMagnitude <= 0.0001 && Time.time - lastActionTime >= 1.0f)
        {
            ExecuteAction();
            Vector3 randomRange = new Vector3(posx, y, posz);
            leaf.position = randomRange;
        }
    }

    void ExecuteAction()
    {
        posx = rnd.Next(-mapRange, mapRange);
        posz = rnd.Next(-mapRange, mapRange);
        lastActionTime = Time.time; // Update last action time
    }
}
