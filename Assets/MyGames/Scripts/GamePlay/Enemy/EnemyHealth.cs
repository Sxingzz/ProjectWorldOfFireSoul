using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private float blinkDuration;
    private EnemyAgent agent;
    private SkinnedMeshRenderer meshRenderer;
    private EnemyHealthBar healthBar;
    private bool isDead = false;

    protected override void OnStart()
    {
        agent = GetComponent<EnemyAgent>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        if (DataManager.HasInstance)
        {
            blinkDuration = DataManager.Instance.DataConfig.BlinkDuration;
            maxHealth = DataManager.Instance.DataConfig.EnemyHealth;
            currentHealth = maxHealth;
        }
    }

    protected override void OnDamage(Vector3 direction)
    {
        StartCoroutine(EnemyFlash());
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
    }

    protected override void OnDeath(Vector3 direction)
    {
        EnemyDeathState deathState = agent.stateMachine.GetState(EnemyStateID.Death) as EnemyDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(EnemyStateID.Death);

        if (isDead)
            return;

        isDead = true;

        EnemyNumber.aliveEnemyCount--;
        Debug.Log("aliveEnemyCount: " + EnemyNumber.aliveEnemyCount);
        if (EnemyNumber.aliveEnemyCount == 0)
        {
            ShowWinPopup();
            Debug.Log("aliveEnemyCount: " + EnemyNumber.aliveEnemyCount);
        }
        ListenerManager.Instance.BroadCast(ListenType.UPDATE_ENEMY_COUNT, EnemyNumber.aliveEnemyCount);
    }


    private IEnumerator EnemyFlash()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(blinkDuration);
        meshRenderer.material.DisableKeyword("_EMISSION");
        StopCoroutine(nameof(EnemyFlash));
    }
    private void ShowWinPopup()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            if (UIManager.HasInstance)
            {
                string message = "Win";
                UIManager.Instance.ShowPopup<PopupMessage>(data: message);
            }
        });
    }
}
