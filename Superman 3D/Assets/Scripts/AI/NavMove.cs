using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMove : MonoBehaviour
{
    public GameObject NodeParent;
    public Transform goal = null;

    NavMeshAgent agent;
    public List<Transform> NodesList;
    int nodeSelected = 0;

    void Start()
    {
        int ClosestNode = 0;
        agent = GetComponent<NavMeshAgent>();
        if (NodesList.Count == 0)
        {
            foreach (Transform child in NodeParent.transform)
            {
                NodesList.Add(child);

                if (Vector3.Distance(NodesList[ClosestNode].position, transform.position) > Vector3.Distance(child.position, transform.position))
                    ClosestNode = NodesList.Count - 1;

            }
        }

        nodeSelected = ClosestNode;
        goal = NodesList[nodeSelected];
        agent.destination = goal.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Distance to Node: " + Vector3.Distance(transform.position, goal.position));
        if (Vector3.Distance(transform.position, goal.position) <= 3f || goal == null)
        {
            //Debug.Log("Initialising Search for Node");
            nodeSelected++;
            if (NodesList.Count - 1 <= nodeSelected)
                nodeSelected = 0;

            //Debug.Log(this.gameObject + " is heading to " + NodesList[nodeSelected]);
            goal = NodesList[nodeSelected];
            agent.destination = goal.position;
        }
    }
}
