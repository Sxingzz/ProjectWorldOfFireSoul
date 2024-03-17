using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenGame : BaseScreen
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI healthText2;
    [SerializeField] private Image healthImage;

    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI taskComplete;
    private Coroutine taskCompleteCoroutine;
    [SerializeField] private TextMeshProUGUI receiveTask;
    private Coroutine receiveTaskCoroutine;



    public override void Init()
    {
        base.Init();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_AMMO, OnUpdateAmmo);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_HEALTH, OnUpdateHealth);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_ENEMY_COUNT, OnUpdateEnemyCount);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_QUEST_TEXT, OnUpdateQuestText);
        }
        questText.enabled = false;

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_TASK_COMPLETE, OnEnableTaskComplete);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_RECEIVE_TASK, OnEnableReceiveTask);
        }

        OnUpdateEnemyCount(null);
    }

    public override void Show(object data)
    {
        base.Show(data);

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_AMMO, OnUpdateAmmo);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_HEALTH, OnUpdateHealth);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_ENEMY_COUNT, OnUpdateEnemyCount);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_QUEST_TEXT, OnUpdateQuestText);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_TASK_COMPLETE, OnEnableTaskComplete);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_RECEIVE_TASK, OnEnableReceiveTask);
        }
    }

    public override void Hide()
    {
        base.Hide();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_AMMO, OnUpdateAmmo);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_HEALTH, OnUpdateHealth);
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_ENEMY_COUNT, OnUpdateEnemyCount);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_QUEST_TEXT, OnUpdateQuestText);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_TASK_COMPLETE, OnEnableTaskComplete);
        }
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_RECEIVE_TASK, OnEnableReceiveTask);
        }
    }

    private void OnUpdateAmmo(object value)
    {
        if (value is RaycastWeapon weapon)
        {
            if (weapon.equipWeaponBy == EquipWeaponBy.Player)
            {
                ammoText.text = weapon.ammoCount.ToString();
                magazineText.text = weapon.magazineSize.ToString();
            }
        }
    }
    private void OnUpdateHealth(object value)
    {
        if (value is PlayerHealth health)
        {
            Debug.Log(health.currentHealth.ToString());
            healthText.text = health.currentHealth.ToString();
            healthText2.text = health.currentHealth.ToString();
            healthImage.fillAmount = health.currentHealth / health.maxHealth;
        }
    }
    private void OnUpdateEnemyCount(object value)
    {
        enemyText.text = EnemyNumber.aliveEnemyCount.ToString();
    }

    private void OnUpdateQuestText(object value)
    {
        if (value is string questName)
        {
            questText.enabled = !string.IsNullOrEmpty(questName);
            questText.text = questName;
        }
    }

    private void OnEnableTaskComplete(object value)
    {
        if (value is string task)
        {
            if (taskCompleteCoroutine != null)
                StopCoroutine(taskCompleteCoroutine);
            taskCompleteCoroutine = StartCoroutine(HideTaskComplete());

            taskComplete.alpha = 1f;
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_MISSINGCOMPLETE);
            }


        }
    }
    private void OnEnableReceiveTask(object value)
    {
        if (value is string receivetask)
        {
            if (taskComplete.alpha == 1f)
            {
                StartCoroutine(DelayedShowReceiveTask(receivetask));
            }
            else
            {
                if (receiveTaskCoroutine != null)
                    StopCoroutine(receiveTaskCoroutine);
                receiveTaskCoroutine = StartCoroutine(HideReceiveTask());
                receiveTask.alpha = 1f;
                receiveTask.text = receivetask;
                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_APPROVEDMISSION);
                }

            }

        }
    }

        private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowPopup<PopupPause>();

                Time.timeScale = 0f;
            }
        }

    }
    private IEnumerator HideTaskComplete()
    {
        yield return new WaitForSeconds(1f);
        taskComplete.alpha = 0f;
        
    }
    private IEnumerator HideReceiveTask()
    {
        yield return new WaitForSeconds(3f); 

        receiveTask.alpha = 0f;
    }
    private IEnumerator DelayedShowReceiveTask(string receivetask)
    {
        yield return new WaitForSeconds(1f);

        if (receiveTaskCoroutine != null)
            StopCoroutine(receiveTaskCoroutine);
        receiveTaskCoroutine = StartCoroutine(HideReceiveTask());
        receiveTask.alpha = 1f;
        receiveTask.text = receivetask;
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_APPROVEDMISSION);
        }
    }


}
