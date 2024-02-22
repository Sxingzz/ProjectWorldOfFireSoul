using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public Transform playerTransform;

    public EnemyStateMachine StateMachine;
    public EnemyStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public EnemyHealthBar UIHealthBar;
    public EnemyWeapon weapons;

    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce;
    public float maxSightDistance = 10f;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<EnemyHealthBar>();
        weapons = GetComponentInChildren<EnemyWeapon>();

        navMeshAgent.stoppingDistance = maxDistance;
        StateMachine = new EnemyStateMachine(this);

        StateMachine.RegisterState(new EnemyChasePlayerState());
        StateMachine.RegisterState(new EnemyDeathState());
        StateMachine.RegisterState(new EnemyIdleState());
        StateMachine.RegisterState(new EnemyFindWeaponState());
        StateMachine.RegisterState(new EnemyAttackPlayerState());

        StateMachine.ChangeState(initState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.update();
    }

    public void DisableAll() // fix lỗi enemy chết bị văng
    {
        var allComponents = GetComponents<MonoBehaviour>();

        foreach (var comp in allComponents)
        {
            comp.enabled = false;
        }

        navMeshAgent.enabled = false;
    }
}
