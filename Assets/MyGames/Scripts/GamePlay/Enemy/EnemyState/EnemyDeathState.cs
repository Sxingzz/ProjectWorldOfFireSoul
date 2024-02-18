using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public Vector3 direction;

    public void Enter(EnemyAgent agent)
    {
        if (agent.ragdoll)
        {
            agent.ragdoll.ActiveRagdoll();
            direction.y = 1f;
            agent.ragdoll.ApplyFore(direction * agent.dieForce);
        }

        agent.UIHealthBar.Deactive();
        agent.weapons.DropWeapon();
    }

    public void Exit(EnemyAgent agent)
    {

    }

    public EnemyStateID GetID()
    {
        return EnemyStateID.Death;
    }

    public void Update(EnemyAgent agent)
    {

    }
}
