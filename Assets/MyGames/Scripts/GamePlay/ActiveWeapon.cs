using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class ActiveWeapon : MonoBehaviour
{
    public CharacterAiming characterAiming;
    public Transform[] weaponSlots;
    public Transform crossHairTarget;
    public Animator rigController;
    public ReloadWeapon reloadWeapon;
    public bool isChangingWeapon;
    public bool canFire;

    private RaycastWeapon[] equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;
    private bool isHolsterd = false;

    // Start is called before the first frame update

    void Start()
    {
        reloadWeapon = GetComponent<ReloadWeapon>();
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);

        canFire = !isHolsterd && !reloadWeapon.isReloading;
        bool notSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("notSprinting");

        if (weapon != null)
        {
            if (Input.GetButtonDown("Fire1") && canFire && !weapon.isFiring)
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring && notSprinting) // hàm này khi giữ chuột thì nó bắn liên tục
            {
                weapon.UpdateWeapon(Time.deltaTime, crossHairTarget.position);
            }
            weapon.UpdateBullets(Time.deltaTime);

            if (Input.GetButtonUp("Fire1") || !canFire)
            {
                weapon.StopFiring();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //bool isHostered = rigController.GetBool("hoister_weapon"); // set bien ishostered = true để play animation rút súng
            //rigController.SetBool("hoister_weapon", !isHostered); // !isHostered set = false để đưa súng về túi
            ToggleActiveWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }

    }

    public bool IsFiring()
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFiring;
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
    }

    private RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index > equippedWeapons.Length)
        {
            return null;
        }
        return equippedWeapons[index];
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);


        weapon = newWeapon;
        weapon.weaponRecoil.characterAiming = characterAiming;
        weapon.weaponRecoil.rigController = rigController;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        rigController.Play("equip_" + weapon.weaponName);

        equippedWeapons[weaponSlotIndex] = weapon;
        activeWeaponIndex = weaponSlotIndex;

        SetActiveWeapon(newWeapon.weaponSlot);

    }

    private void ToggleActiveWeapon()
    {
        bool isHoslsterd = rigController.GetBool("holster_weapon");
        if (isHoslsterd)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }
    private IEnumerator HolsterWeapon(int index)
    {
        isChangingWeapon = true;
        isHolsterd = true;
        var weapon = GetWeapon(index);
        if (weapon != null)
        {
            rigController.SetBool("holster_weapon", true);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        isChangingWeapon = false;
    }

    private IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapon = true;
        var weapon = GetWeapon(index);
        if (weapon != null)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolsterd = false;
        }
        isChangingWeapon = false;
    }

    public void DropWeapon()
    {
        var currentWeapon = GetActiveWeapon();

        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            equippedWeapons[activeWeaponIndex] = null;
        }
    }

}
