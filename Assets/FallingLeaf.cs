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
    public float delayTime = 1.0f;
    private float timer = 0.0f;
    private bool waiting = false;
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
            if (!waiting) // Start the timer if not already waiting
            {
                waiting = true;
                timer = 0.0f; // Reset the timer
            }

            // Increment the timer
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                ExecuteAction();
                Vector3 randomRange = new Vector3(posx, y, posz);
                leaf.position = randomRange;
                waiting = false; // Reset the waiting state
            }
            
        }
        else
        {
            waiting = false; // Reset waiting if the leaf moves again
        }
        
    }

    void ExecuteAction()
    {
        posx = rnd.Next(-mapRange, mapRange);
        posz = rnd.Next(-mapRange, mapRange);
        lastActionTime = Time.time; // Update last action time
    }
}
