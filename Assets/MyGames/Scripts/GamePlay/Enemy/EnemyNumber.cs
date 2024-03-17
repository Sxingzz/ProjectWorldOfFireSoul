using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNumber : MonoBehaviour
{
    public static int aliveEnemyCount = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        aliveEnemyCount = 0;
    }
    void Start()
    {
        aliveEnemyCount++;
        Debug.Log("alive " + aliveEnemyCount);
        ListenerManager.Instance.BroadCast(ListenType.UPDATE_ENEMY_COUNT, this);
    }

}
