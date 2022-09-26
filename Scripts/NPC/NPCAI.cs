using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //NavMeshPlus thingy
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        startingPosition = transform.position;
        roamPosition = GetRandomPosition();
    }

    private void Update()
    {
        agent.SetDestination(roamPosition);
        if (agent.remainingDistance < 1f) roamPosition = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        return startingPosition + randomDir * Random.Range(4f, 15f);
    }
}