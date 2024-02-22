using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPlayerState : EnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.AttackPlayer;
    }
    public void Enter(EnemyAgent agent)
    {
        agent.weapons.ActiveWeapon();
        agent.weapons.SetTarget(agent.playerTransform);
        agent.navMeshAgent.stoppingDistance = 5.0f;
        agent.weapons.SetFiring(true);
    }

    public void Exit(EnemyAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 0f;
    }

    public void Update(EnemyAgent agent)
    {
        agent.navMeshAgent.destination = agent.playerTransform.position;

        if (agent.playerTransform.GetComponent<Health>().IsDead())
        {
            agent.StateMachine.ChangeState(EnemyStateID.Idle);
        }
    }


}
