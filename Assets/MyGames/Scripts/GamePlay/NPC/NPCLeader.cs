using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCLeader : NPCController
{
    public GameObject panelForLButton_Leader;
    public GameObject panelForText_Leader;
    public TextMeshProUGUI messageText_Leader;




    protected override void Start()
    {
        base.Start();

        panelForLButton = panelForLButton_Leader;
        messageText = messageText_Leader;
        panelForText = panelForText_Leader;


        // Thêm các đoạn văn bản vào danh sách
        textList.Add("Hello Alex Hunter");
        textList.Add("This is our base");
        textList.Add("I'm the captain at this base");
        textList.Add("You are a mercenary with combat experience, hired by us");
        textList.Add("However, I will also guide you through a few basic operations");
        textList.Add("To move, you need to press W, S, to move up and down, and press A, D to move left and right");
        textList.Add("Press Space to jump");
        textList.Add("Your first task is find the shooting range to meet Elsa");
        textList.Add("She will guide you through some necessary steps before going to the battlefield to fight the enemy.");
        textList.Add("Bug");


    }

    protected override void Update()
    {
        base.Update();
        if (panelForLButton != null && panelForText != null && messageText != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= playerDetectionRange)
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
        // Gọi QuestManager để thêm nhiệm vụ vào danh sách của người chơi
        QuestManager.Instance.AddQuest("Task 1: Find Elsa");

        questGiven = true; // Đánh dấu rằng nhiệm vụ đã được giao
        TalkToCaptain = true;
        
        Debug.Log("Talk To Captain = " + TalkToCaptain);
        // Truyền giá trị TalkToCaptain sang NPCElsa
        NPCElsa elsaNPC = FindObjectOfType<NPCElsa>(); // Tìm NPCElsa trong Scene
        if (elsaNPC != null)
        {
            elsaNPC.SetTalkToCaptain(true);
        }

    }


}



