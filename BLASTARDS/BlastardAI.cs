using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlastardAI : MonoBehaviour
{
    public List<Transform> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public bool isPickedUp = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Temporarily prevent the NavMeshAgent from updating rotation
        //agent.updateRotation = false;

        // Set the initial rotation
        //transform.rotation = Quaternion.Euler(-90, 0, 45);

        // Enable the NavMeshAgent to update rotation again
        agent.updateRotation = true;

        // Set the desired rotation directly on the Blastard GameObject
        //transform.eulerAngles = new Vector3(-90, 0, 45);

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (isPickedUp)
        {
            return;
        }
        if (points.Count == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Count;
    }
    void FixedUpdate()
    {
        if (isPickedUp)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}