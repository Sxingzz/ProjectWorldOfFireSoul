using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float blinkDuration = 0.1f;

    private EnemyAgent agent;
    private SkinnedMeshRenderer meshRenderer;
    private EnemyHealthBar healthBar;

    void Start()
    {
        agent = GetComponent<EnemyAgent>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        currentHealth = maxHealth;
        SetupHixBox();
    }

    private void SetupHixBox()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.gameObject.AddComponent<HitBox>().AIHealth = this;
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        StartCoroutine(EnemyFlash());
        currentHealth -= damageAmount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
    {
        EnemyDeathState deathState = agent.stateMachine.GetState(EnemyStateID.Death) as EnemyDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(EnemyStateID.Death);
    }

    private IEnumerator EnemyFlash()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(blinkDuration);
        meshRenderer.material.DisableKeyword("_EMISSION");
        StopCoroutine(nameof(EnemyFlash));
    }
}
