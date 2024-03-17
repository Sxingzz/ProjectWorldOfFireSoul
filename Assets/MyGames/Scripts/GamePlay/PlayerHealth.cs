using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PlayerHealth : Health
{
    private Ragdoll ragdoll;
    private ActiveWeapon activeWeapon;
    private CharacterAiming characterAiming;
    private VolumeProfile postProcessing;
    private Vignette vignette;

    protected override void OnStart()
    {
        ragdoll = GetComponent<Ragdoll>();
        activeWeapon = GetComponent<ActiveWeapon>();
        characterAiming = GetComponent<CharacterAiming>();
        postProcessing = FindObjectOfType<Volume>().profile;

        if (DataManager.HasInstance)
        {
            maxHealth = DataManager.Instance.DataConfig.PlayerMaxHealth;
            currentHealth = maxHealth;
        }

        DOVirtual.DelayedCall(3f, () =>
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
            }
        });
    }

    protected override void OnDamage(Vector3 direction)
    {
        if (postProcessing.TryGet(out vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.4f;
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
        }

    }

    protected override void OnDeath(Vector3 direction)
    {
        ragdoll.ActiveRagdoll();
        direction.y = 1f;
        ragdoll.ApplyFore(direction);
        activeWeapon.DropWeapon();
        characterAiming.enabled = false;
        if (CameraManager.HasInstance)
        {
            CameraManager.Instance.EnableKillCam();
        }

        Transform enemyThatKilledPlayer = enemyKilledPlayer();
        CameraManager.Instance.SetEnemyThatKilledPlayer(enemyThatKilledPlayer);

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
        }

        DOVirtual.DelayedCall(3f, () =>
        {
            if (UIManager.HasInstance)
            {
                string message = "Lose";
                UIManager.Instance.ShowPopup<PopupMessage>(data: message);
            }
        });
    }

    protected override void OnHealth(float amount)
    {
        if (postProcessing.TryGet(out Vignette vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.4f;
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
        }
    }
    private Transform enemyKilledPlayer()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Tìm tất cả các đối tượng có tag "Enemy"
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // Ghi nhận enemy đã giết player
        if (closestEnemy != null && CameraManager.HasInstance)
        {
            CameraManager.Instance.SetEnemyThatKilledPlayer(closestEnemy.transform); // Truyền Transform của closestEnemy
            CameraManager.Instance.EnableKillCam();
        }

        return closestEnemy != null ? closestEnemy.transform : null; // Trả về Transform của enemy đã giết player, hoặc null nếu không tìm thấy enemy nào
    }
}
