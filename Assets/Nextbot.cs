using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Nextbot : MonoBehaviour
{
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
    private float lastActionTime; // Store the last action time
    public GameObject beacon;
    private Renderer r;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        chase = true;
        lastActionTime = Time.time; // Initialize last action time
        sparseCoroutine = StartCoroutine(SparseUpdate());
        r = beacon.GetComponent<Renderer>();
    }

    void Update()
    {
        if (chase)
        {
            agent.destination = target.position;
        }
        else if (stalk)
        {
            Vector3 randomRange = new Vector3(posx, 0.0f, posz);
            agent.destination = target.position + randomRange;
        }
        else
        {
            Vector3 randomRange = new Vector3(posx, 0.0f, posz);
            agent.destination = randomRange;
        }

        // Check if agent has stopped and if enough time has passed
        if (agent.velocity.sqrMagnitude <= 0.0001 && Time.time - lastActionTime >= 1.0f)
        {
            ExecuteAction();
            ResetCoroutine();
        }
    }

    IEnumerator SparseUpdate()
    {
        while (true)
        {
            ExecuteAction();
            yield return new WaitForSeconds(fireDelay);
        }
    }

    void ExecuteAction()
    {
        int num = rnd.Next(10);
        if ((num >= stalkChance && num < hideChance + stalkChance) || (target.position.y > 100))
        {
            chase = false;
            stalk = false;
            posx = rnd.Next(-mapRange, mapRange);
            posz = rnd.Next(-mapRange, mapRange);
            r.material = hideMaterial;
        }
        else if (num < stalkChance)
        {
            chase = false;
            stalk = true;
            posx = rnd.Next(-stalkRange, stalkRange);
            posz = rnd.Next(-stalkRange, stalkRange);
            r.material = stalkMaterial;
        }
        else
        {
            chase = true;
            stalk = false;
            r.material = chaseMaterial;
        }

        lastActionTime = Time.time; // Update last action time
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
