using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : BaseManager<ObjectPool>
{
    public List<Bullet> PlayerPooledObjects;
    //public List<Bullet> EnemyPooledObjects;
    public Bullet PlayerObjectToPool;
    //public Bullet EnemyObjectToPool;
    [SerializeField]
    private int amountToPool;

    private void Start()
    {
        PlayerPooledObjects = new List<Bullet>();
        //EnemyPooledObjects = new List<Bullet>();

        Bullet tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(PlayerObjectToPool, this.transform, true);
            tmp.Deactive();
            PlayerPooledObjects.Add(tmp);
        }

        //for (int i = 0; i < amountToPool; i++)
        //{
        //    tmp = Instantiate(EnemyObjectToPool, this.transform, true);
        //    tmp.Deactive();
        //    EnemyPooledObjects.Add(tmp);
        //}
    }

    public Bullet GetPooledObject()
    {
        for (int i = 0; i < PlayerPooledObjects.Count; i++)
        {
            if (!PlayerPooledObjects[i].IsActive)
            {
                return PlayerPooledObjects[i];
            }
        }
        return null;
    }
    //public Bullet GetPooledObject(EquipWeaponBy equipBy)
    //{
    //    List<Bullet> poolToUse = null;
    //    switch (equipBy)
    //    {
    //        case EquipWeaponBy.Player:
    //            poolToUse = PlayerPooledObjects;
    //            break;
    //        case EquipWeaponBy.Enemy:
    //            poolToUse = EnemyPooledObjects;
    //            break;
    //        default:
    //            Debug.LogError("Unknown equip by type: " + equipBy);
    //            return null;
    //    }

    //    for (int i = 0; i < poolToUse.Count; i++)
    //    {
    //        if (!poolToUse[i].IsActive)
    //        {
    //            return poolToUse[i];
    //        }
    //    }
    //    return null;
    //}
}

