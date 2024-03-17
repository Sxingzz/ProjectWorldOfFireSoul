using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : BaseManager<CameraManager>
{
    public CinemachineVirtualCameraBase killCamera;
    public CinemachineTargetGroup targetGroup;

    private Transform enemyThatKilledPlayer;
    public Transform playerTransform;
    private float enemyWeight = 0.7f;
    private float enemyRadius = 2f;

    float playerWeight = 1.0f;
    float playerRadius = 2.0f;

    public void EnableKillCam()
    {
        killCamera.Priority = 20;

        if (enemyThatKilledPlayer != null)
        {
            AssignEnemyToTargetGroup(enemyThatKilledPlayer);
        }
        // Gán đối tượng người chơi vào target group khi bật kill cam
        if (playerTransform != null)
        {
            AssignPlayerToTargetGroup(playerTransform);
        }
    }

    public void ResetKillCam()
    {
        killCamera.Priority = 0;
    }

    public void SetEnemyThatKilledPlayer(Transform enemy)
    {
        enemyThatKilledPlayer = enemy;
    }

    private void AssignEnemyToTargetGroup(Transform enemy)
    {
        targetGroup.AddMember(enemy, enemyWeight, enemyRadius);
    }
    private void AssignPlayerToTargetGroup(Transform player)
    {
        targetGroup.AddMember(player, playerWeight, playerRadius);
    }
}
