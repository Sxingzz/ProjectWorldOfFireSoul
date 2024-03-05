using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopupBackToMenu : BasePopup
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

    public void OnClickYesButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
        }
    }
    public void OnClickNoButton()
    {
        Hide();
    }

}
