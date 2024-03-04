using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuitGame : BasePopup
{
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;

    public override void Init()
    {
        base.Init();
        buttonYes.onClick.AddListener(OnClickYesButton);
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
        OnClickNoButton();
    }
    public void OnClickYesButton()
    {
        Application.Quit();
    }
    public void OnClickNoButton()
    {
        Hide();
    }

}
