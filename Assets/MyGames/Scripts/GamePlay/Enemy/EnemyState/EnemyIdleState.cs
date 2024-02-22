using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    EnemyStateID EnemyState.GetID()
    {
        return EnemyStateID.Idle;
    }

    void EnemyState.Enter(EnemyAgent agent)
    {
        agent.weapons.DeActiveWeapon();
        agent.navMeshAgent.ResetPath();
    }

    void EnemyState.Exit(EnemyAgent agent)
    {

    }

    void EnemyState.Update(EnemyAgent agent)
    {
        if (agent.playerTransform.GetComponent<Health>().IsDead()) return;

        Vector3 playerDirection = agent.playerTransform.position - agent.playerTransform.position;
        if (playerDirection.sqrMagnitude > agent.maxSightDistance * agent.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0)
        {
            agent.StateMachine.ChangeState(EnemyStateID.ChasePlayer);
        }
    }
}
