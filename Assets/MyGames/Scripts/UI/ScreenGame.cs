using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenGame : BaseScreen
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthImage;

    public override void Init()
    {
        base.Init();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_AMMO, OnUpdateAmmo);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_HEALTH, OnUpdateHealth);
        }
        
    }

    public override void Show(object data)
    {
        base.Show(data); 

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_AMMO, OnUpdateAmmo);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_HEALTH, OnUpdateHealth);
        }
    }

    public override void Hide()
    {
        base.Hide();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_AMMO, OnUpdateAmmo);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_HEALTH, OnUpdateHealth);
        }
    }

    private void OnUpdateAmmo(object value)
    {
        if (value is RaycastWeapon weapon)
        {
            if (weapon.equipWeaponBy == EquipWeaponBy.Player)
            {
                ammoText.text = weapon.ammoCount.ToString();
                magazineText.text = weapon.magazineSize.ToString();
            }
        }
    }
    private void OnUpdateHealth(object value)
    {
        if (value is PlayerHealth health)
        {
            Debug.Log(health.currentHealth.ToString());
            healthText.text = health.currentHealth.ToString();
            healthImage.fillAmount = health.currentHealth / health.maxHealth;
        }
    }

}
