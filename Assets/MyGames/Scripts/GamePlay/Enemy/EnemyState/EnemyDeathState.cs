using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        agent.DisableAll();

        DOVirtual.DelayedCall(2f, () =>
        {
            if (UIManager.HasInstance)
            {
                string message = "Win";
                UIManager.Instance.ShowPopup<PopupMessage>(data: message);
            }
        });
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
