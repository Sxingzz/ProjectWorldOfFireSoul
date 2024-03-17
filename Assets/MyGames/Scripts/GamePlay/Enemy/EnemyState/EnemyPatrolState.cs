using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    private NavMeshAgent navMeshAgent;
    private float patrolRadius;

    public EnemyPatrolState(NavMeshAgent navMeshAgent, float patrolRadius)
    {
        this.navMeshAgent = navMeshAgent;
        this.patrolRadius = patrolRadius;
    }

    public void Enter(EnemyAgent agent)
    {
        SetRandomPatrolDestination();
        Debug.Log("Next patrol point: " + navMeshAgent.destination);
    }

    public void Exit(EnemyAgent agent)
    {
        
    }

    public void Update(EnemyAgent agent)
    {
        if (navMeshAgent.remainingDistance < 0.5f || !navMeshAgent.pathPending)
        {
            SetRandomPatrolDestination();
            Debug.Log("Next patrol point: " + navMeshAgent.destination);
        }
    }

    public EnemyStateID GetID()
    {
        return EnemyStateID.Patrol;
    }

    private void SetRandomPatrolDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += navMeshAgent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
    }
}
