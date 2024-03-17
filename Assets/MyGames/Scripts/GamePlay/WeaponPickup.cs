using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        //Player
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        
        if (activeWeapon != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ITEMPICKUP);
            }
            Destroy(gameObject);
        }
        

        //Enemy
        EnemyWeapons enemyWeapons = other.gameObject.GetComponent<EnemyWeapons>();
        if (enemyWeapons != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            enemyWeapons.EquipWeapon(newWeapon);
            Destroy(gameObject);
        }
    }
}
