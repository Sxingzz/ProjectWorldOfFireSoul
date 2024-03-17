using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCElsa : NPCController
{
    public GameObject panelForLButton_Elsa;
    public GameObject panelForText_Elsa;
    public TextMeshProUGUI messageText_Elsa;

    public void SetTalkToCaptain(bool value)
    {
        TalkToCaptain = value;
    }

    protected override void Start()
    {
        base.Start();


        panelForLButton = panelForLButton_Elsa;
        messageText = messageText_Elsa;
        panelForText = panelForText_Elsa;

        // Thêm các đoạn văn bản vào danh sách
        textList.Add("Hello Alex Hunter");
        textList.Add("You didn't have too much trouble finding me, right?");
        textList.Add("I'm Elsa, now I will guide you to get used to fighting here");
        textList.Add("There are 3 guns over there");
        textList.Add("When picking them up, you can use key 1,2,3 to select the gun to use.");
        textList.Add("There are also some first aid boxes and ammo boxes in front if you need to use them, pick them up.");
        textList.Add("Don't worry haha, this is for you to get used to because our base is temporarily safe from the robots out there.");
        textList.Add("Now Soldier, pick up the gun and practice, then find jack near the exit of this base to receive the next mission.");
        textList.Add("Bug");
    }

    protected override void Update()
    {
        base.Update();

            if (panelForLButton != null && panelForText != null && messageText != null)
            {
                if (TalkToCaptain && Vector3.Distance(transform.position, player.transform.position) <= playerDetectionRange)
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
        QuestManager.Instance.CompleteQuest("Task 1: Find Elsa");
        
        
        QuestManager.Instance.AddQuest("Task 2: Find Jack");

        questGiven = true; // Đánh dấu rằng nhiệm vụ đã được giao

        TalkToElsa = true;
        Debug.Log("TalktoElsa" + TalkToElsa);
        // Truyền giá trị TalkToElsa sang NPCJack
        NPCJack JackNPC = FindObjectOfType<NPCJack>(); // Tìm NPCJack trong Scene
        if (JackNPC != null)
        {
            JackNPC.SetTalkToElsa(true);
        }


    }
    



}


