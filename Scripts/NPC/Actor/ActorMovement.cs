using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorMovement : MonoBehaviour
{
    private enum ActorStates
    {
        Moving,
        Idle
    }
    private ActorStates state;

    private NavMeshAgent agent;

    private bool scenePlaying;
    private int sceneIndex;
    public Scene[] scenes;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        state = ActorStates.Moving;
        agent.SetDestination(scenes[0].actorSpot.position);
        scenePlaying = false;
    }

    void Start()
    {
        //NavMeshPlus thingy
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        sceneIndex = 0;
    }

    void Update()
    {
        switch (state)
        {
            case ActorStates.Moving:
                if (sceneIndex > 0 && sceneIndex < scenes.Length)
                {
                    agent.SetDestination(scenes[sceneIndex].actorSpot.position);
                }

                if( sceneIndex < scenes.Length)
                {
                    if (Vector2.Distance(transform.position, scenes[sceneIndex].actorSpot.position) <= 0.3f)
                    {
                        state = ActorStates.Idle;
                    }
                }
                break;

            case ActorStates.Idle:
                if (!scenePlaying) StartCoroutine(PlayScene());
                break;
        }
    }

    IEnumerator PlayScene()
    {
        scenePlaying = true;
        for (int i = 0; i < scenes[sceneIndex].actingScenes.sequences.Length; i++)
        {
            if (!scenes[sceneIndex].actingScenes.sequences[i].hasToMove)
                ChatBubble.Create(transform, new Vector3(0.5f, 1), scenes[sceneIndex].actingScenes.sequences[i].line, 4f);
            else
            {
                agent.SetDestination(transform.position + scenes[sceneIndex].actingScenes.sequences[i].moveSpot);
                ChatBubble.Create(transform, new Vector3(0.5f, 1), scenes[sceneIndex].actingScenes.sequences[i].line, 4f);
            }
            yield return new WaitForSeconds(5f);
        }
        scenePlaying = false;
        if (sceneIndex > scenes.Length)
        {
            StopCoroutine(PlayScene());
            yield break; //All Scenes have ended.
        }
        else sceneIndex++;
        state = ActorStates.Moving;
        StopCoroutine(PlayScene());
    }
}

[System.Serializable]
public class Scene
{
    public Transform actorSpot;
    public ActingScene actingScenes;
}