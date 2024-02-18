using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public Transform playerTransform;
    public EnemyStateMachine stateMachine;
    public EnemyStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public EnemyHealthBar UIHealthBar;
    public EnemyWeapons weapons;
    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce = 10f;
    public float maxSightDistance = 10f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<EnemyHealthBar>();
        weapons = GetComponent<EnemyWeapons>();
        navMeshAgent.stoppingDistance = maxDistance;
        stateMachine = new EnemyStateMachine(this);
        stateMachine.RegisterState(new EnemyChasePlayerState());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyFindWeaponState());
        stateMachine.RegisterState(new EnemyAttackPlayerState());
        stateMachine.ChangeState(initState);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
