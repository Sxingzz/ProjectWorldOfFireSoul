using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public Vector3 direction;


    public EnemyStateID GetID()
    {
        return EnemyStateID.Death;
    }
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
        agent.DisableAll();
    }

    public void Exit(EnemyAgent agent)
    {

    }
    public void Update(EnemyAgent agent)
    {

    }
   
}
