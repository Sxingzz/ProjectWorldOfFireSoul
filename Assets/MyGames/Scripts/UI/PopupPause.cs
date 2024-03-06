using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopupPause : BasePopup
{
    public override void Init()
    {
        base.Init();
    }
    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();
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
        Hide();


    }

}
