using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuitGame : BasePopup
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
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void OnClickNoButton()
    {
        Hide();
    }

}
