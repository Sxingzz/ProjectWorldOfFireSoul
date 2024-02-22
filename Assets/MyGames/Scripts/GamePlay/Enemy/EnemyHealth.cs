using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public float blinkDuration = 0.1f;


    private EnemyAgent agent;
    private SkinnedMeshRenderer meshRenderer;
    private EnemyHealthBar healthBar;

    protected override void OnStart()
    {
        agent = GetComponent<EnemyAgent>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }
    protected override void OnDamaged(Vector3 direction)
    {
        StartCoroutine(EnemyFlash());
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
    }
    protected override void OnDeath(Vector3 direction)
    {
        EnemyDeathState deathState = agent.StateMachine.GetState(EnemyStateID.Death) as EnemyDeathState;
        deathState.direction = direction;
        agent.StateMachine.ChangeState(EnemyStateID.Death);
    }
    private IEnumerator EnemyFlash()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(blinkDuration);
        meshRenderer.material.DisableKeyword("_EMISSION");
        StopCoroutine(nameof(EnemyFlash));
    }
}