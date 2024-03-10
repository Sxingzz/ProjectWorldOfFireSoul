using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAgent : MonoBehaviour
{
    public Transform playerTransform;
    public EnemyStateMachine stateMachine;
    public EnemyStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public EnemyHealthBar UIHealthBar;
    public EnemyWeapons weapons;
    public float maxTime;
    public float maxDistance;
    public float dieForce;
    public float maxSightDistance;

    public float detectionRange = 10f;

    public static int aliveEnemyCount = 0;


    private void Awake()
    {
        aliveEnemyCount = 0;

        if (DataManager.HasInstance)
        {
            maxTime = DataManager.Instance.DataConfig.MaxTime;
            maxDistance = DataManager.Instance.DataConfig.MaxDistance;
            dieForce = DataManager.Instance.DataConfig.DieForce;
            maxSightDistance = DataManager.Instance.DataConfig.MaxSightDistance;
        }
    }

    void Start()
    {
        aliveEnemyCount++;
        Debug.Log("alive "+ aliveEnemyCount);

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
        EnemyController();

    }

    public void DisableAll()
    {
        var allComponents = GetComponents<MonoBehaviour>();

        foreach (var comp in allComponents)
        {
            comp.enabled = false;
        }

        navMeshAgent.enabled = false;
    }

    public void EnemyController()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (stateMachine.currentState != EnemyStateID.AttackPlayer)
        {
            if (distanceToPlayer <= detectionRange)
            {
                stateMachine.ChangeState(EnemyStateID.FindWeapon);
            }
            else
            {
                stateMachine.currentState = EnemyStateID.Idle;
            }
        }

        if (stateMachine.currentState == EnemyStateID.ChasePlayer && distanceToPlayer <= 5f)
        {
            stateMachine.ChangeState(EnemyStateID.AttackPlayer);
        }
    }



}
