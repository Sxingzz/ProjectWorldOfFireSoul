using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : BaseManager<QuestManager>
{
    // Danh sách các nhiệm vụ của người chơi
    private List<Quest> playerQuests = new List<Quest>();

    protected override void Awake()
    {
        base.Awake();
        var instance = Instance;
    }
    // Hàm để thêm một nhiệm vụ vào danh sách nhiệm vụ của người chơi
    public void AddQuest(string title)
    {
        playerQuests.Add(new Quest(title));
        Debug.Log("đã thêm 1 nv");
        ListenerManager.Instance.BroadCast(ListenType.UPDATE_QUEST_TEXT, title);
        ListenerManager.Instance.BroadCast(ListenType.UPDATE_RECEIVE_TASK, title);

    }

    // Hàm để kiểm tra xem một nhiệm vụ đã hoàn thành chưa
    public bool IsQuestCompleted(string title)
    {
        foreach (Quest quest in playerQuests)
        {
            if (quest.title == title && quest.isCompleted)
            {
                return true;
            }
        }
        return false;
    }

    // Hàm để đánh dấu một nhiệm vụ là đã hoàn thành
    public void CompleteQuest(string title)
    {
        foreach (Quest quest in playerQuests)
        {
            if (quest.title == title)
            {
                quest.isCompleted = true;
                Debug.Log("Quest completed: " + title);
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_TASK_COMPLETE, title);
                return;
            }
        }
    }
}

// Lớp đại diện cho một nhiệm vụ
[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    public bool isCompleted;

    public Quest(string title)
    {
        this.title = title;
        this.isCompleted = false;
    }
}