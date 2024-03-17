using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCJack : NPCController
{
    public GameObject panelForLButton_Jack;
    public GameObject panelForText_Jack;
    public TextMeshProUGUI messageText_Jack;

    public void SetTalkToElsa(bool value)
    {
        TalkToElsa = value;
    }
    protected override void Start()
    {
        base.Start();

        panelForLButton = panelForLButton_Jack;
        messageText = messageText_Jack;
        panelForText = panelForText_Jack;

        // Thêm các đoạn văn bản vào danh sách
        textList.Add("Hello Alex Hunter");
        textList.Add("I'm Jack. I'm waiting for you");
        textList.Add("You've gone through Elsa's training, right?");
        textList.Add("Now the situation is very dangerous out there");
        textList.Add("Now your mission is to find the ghost city area, destroy the robots there, and regain control of this area.");
        textList.Add("That area is quite remote, rest assured because we have airdropped many supply boxes including first aid and ammo in that area.");
        textList.Add("they will be useful to you.");
        textList.Add("I'll guide you some more advanced operations, it might help you.");
        textList.Add("Press shift while moving to speed up.");
        textList.Add("Press R to reload ammo");
        textList.Add("Press V to throw away the gun");
        textList.Add("Press X to put away the gun");
        textList.Add("Now go out and do your mission, good luck.");
        textList.Add("Bug");


    }

    protected override void Update()
    {
        base.Update();
        if (panelForLButton != null && panelForText != null && messageText != null)
        {
            if (TalkToElsa && Vector3.Distance(transform.position, player.transform.position) <= playerDetectionRange)
            {
                SetPanelAlpha(panelForLButton, 1);

                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlaySE(AUDIO.SE_CLICK);
                    }

                    panelForText.SetActive(true);
                    HandleInteractInput();

                }
            }
            else
            {
                SetPanelAlpha(panelForLButton, 0);
                panelForText.SetActive(false);
                currentIndex = 0;
            }
        }
    }
    protected override void GiveQuestToPlayer()
    {
        base.GiveQuestToPlayer();
        QuestManager.Instance.CompleteQuest("Task 2: Find Jack");
        QuestManager.Instance.AddQuest("Task 3: Destroy the enemy and gain control of the Gosh city ");

        questGiven = true; // Đánh dấu rằng nhiệm vụ đã được giao

        
    }


}


