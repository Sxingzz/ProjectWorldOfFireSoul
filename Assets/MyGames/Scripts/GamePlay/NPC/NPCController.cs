using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    protected GameObject panelForLButton;
    protected TextMeshProUGUI messageText;
    protected GameObject panelForText;
    protected GameObject player;
    protected float playerDetectionRange = 5f;
    protected List<string> textList = new List<string>();
    protected int currentIndex = 0;
    protected bool questGiven = false;
    protected bool TalkToCaptain = false;
    protected bool TalkToElsa = false;



    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Player object not found in the scene!");
        }

        if (panelForLButton == null || panelForText == null || messageText == null)
        {
           Debug.Log("Panel or Text components are not assigned properly!");
        }
        else
        {
            SetPanelAlpha(panelForLButton, 0);
            panelForText.SetActive(false);
        }
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void ShowNextText()
    {
        if (currentIndex < textList.Count)
        {
            messageText.text = textList[currentIndex];
            currentIndex++;
        }
       
    }
    protected virtual void SetPanelAlpha(GameObject panel, float alpha)
    {
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            Color color = panelImage.color;
            color.a = alpha;
            panelImage.color = color;
        }
    }
    protected virtual void HandleInteractInput()
    {
        if (panelForText.activeSelf)
        {
            ShowNextText();
            if (currentIndex >= textList.Count)
            {
                GiveQuestToPlayer(); // Giao nhiệm vụ
                currentIndex = 0; // Thiết lập lại currentIndex để bắt đầu từ đầu
                panelForText.SetActive(false);
            }
        }
        else
        {
            currentIndex = 0;
        }
    }
    protected virtual void GiveQuestToPlayer()
    {
        
    }
   
}