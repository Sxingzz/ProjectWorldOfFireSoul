using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasePlayerState : EnemyState
{
    private float timer = 0f;

    public EnemyStateID GetID()
    {
        return EnemyStateID.ChasePlayer;
    }

    public void Enter(EnemyAgent agent)
    {

    }

    public void Exit(EnemyAgent agent)
    {

    }

    public void Update(EnemyAgent agent)
    {
        if (!agent.enabled) return;

        timer -= Time.deltaTime;

        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }

        if (timer < 0)
        {
            Vector3 direction = agent.playerTransform.position - agent.navMeshAgent.destination;
            direction.y = 0;
            float sqrDistance = direction.sqrMagnitude;
            if (sqrDistance > agent.maxDistance * agent.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }
            timer = agent.maxTime;
        }
    }
}
