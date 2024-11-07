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
    System.Random rnd = new System.Random();
    public float fireDelay = 10.0f;
    public int stalkRange = 10;
    public int mapRange = 100;
    private bool chase = false;
    private bool stalk = false;
    public float posx;
    public float posz;
    public int stalkChance = 3;
    public int hideChance = 3;
    public Material stalkMaterial;
    public Material chaseMaterial;
    public Material hideMaterial;
    private Coroutine sparseCoroutine;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        chase = true;
        sparseCoroutine = StartCoroutine(SparseUpdate());

    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {
            agent.destination = target.position;
            //Debug.Log("Chasing");
        }
        else if (stalk)
        {
            Vector3 randomRange = new Vector3(posx, 0.0f, posz);
            agent.destination = target.position + randomRange;
            //Debug.Log("Hiding");
        }
        else {
            Vector3 randomRange = new Vector3(posx, 0.0f, posz);
            agent.destination = randomRange;
            //Debug.Log("Hiding")
        }
        if (agent.velocity.sqrMagnitude <= 0.0001)
        {
            ExecuteAction(); // Execute action when stopped
            ResetCoroutine();
        }
    }

    IEnumerator SparseUpdate()
    {
        while (true)
        {
            ExecuteAction();
            yield return new WaitForSeconds(fireDelay); // Wait for 3 seconds before executing again
        }
    }

    void ExecuteAction() {
        int num = rnd.Next(10);
        if (num < stalkChance)
        {
            chase = false;
            stalk = true;
            posx = rnd.Next(-stalkRange, stalkRange);
            posz = rnd.Next(-stalkRange, stalkRange);
            GetComponent<Renderer>().material = stalkMaterial;
        }
        else if (num >= stalkChance && num < hideChance + stalkChance)
        {
            chase = false;
            stalk = false;
            posx = rnd.Next(-mapRange, mapRange);
            posz = rnd.Next(-mapRange, mapRange);
            GetComponent<Renderer>().material = hideMaterial;
        }
        else {
            chase = true;
            stalk = false;
            GetComponent<Renderer>().material = chaseMaterial;
        }
    }
    
    void ResetCoroutine()
    {
        if (sparseCoroutine != null)
        {
            StopCoroutine(sparseCoroutine);
        }
        sparseCoroutine = StartCoroutine(SparseUpdate());
    }

}
