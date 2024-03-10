using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopupPause : BasePopup
{
    public override void Init()
    {
        base.Init();
    }
    public override void Show(object data)
    {
        base.Show(data);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Hide()
    {
        base.Hide();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnClickBackToMenuButton()
    {
        Time.timeScale = 1f;
        Hide();

        if (GameManager.HasInstance)
        {
            GameManager.Instance.RestartGame();
        }

        
    }

    public void OnClickSettingButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupSetting>();
        }
    }

    public void OnClickBackButton()
    {
        Time.timeScale = 1f;
        Hide();

    }

}
