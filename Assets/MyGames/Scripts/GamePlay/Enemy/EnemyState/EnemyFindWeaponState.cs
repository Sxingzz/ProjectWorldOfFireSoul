using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFindWeaponState : EnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.FindWeapon;
    }
    public void Enter(EnemyAgent agent)
    {
        WeaponPickup pickup = FindClosetWeapon(agent);
        if (pickup)
        {
            agent.navMeshAgent.destination = pickup.transform.position;
            agent.navMeshAgent.stoppingDistance = 0f;
            agent.navMeshAgent.speed = 5f;
        }
    }

    public void Exit(EnemyAgent agent)
    {

    }

    public void Update(EnemyAgent agent)
    {
        if (agent.weapons.HasWeapon())
        {
            agent.stateMachine.ChangeState(EnemyStateID.AttackPlayer);
        }
    }

    private WeaponPickup FindClosetWeapon(EnemyAgent agent)
    {
        WeaponPickup[] weapons = Object.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closetWeapon = null;
        float closestDistance = float.MaxValue;

        foreach (var weapon in weapons)
        {
            float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
            if (distanceToWeapon < closestDistance)
            {
                closestDistance = distanceToWeapon;
                closetWeapon = weapon;
            }
        }

        return closetWeapon;
    }
}
