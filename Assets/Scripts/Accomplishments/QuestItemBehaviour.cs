using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuestItemBehaviour : MonoBehaviour
{
    Quest.QuestCode code;
    [SerializeField] Text TxtTask;
    [SerializeField] TextMeshProUGUI TxtCurrentProgress;
    [SerializeField] Image ImgProgressBar;
    int currentIndex = 0;
    int currentProgress = 0;
    int maxProgress = 0;
    string tooltip = string.Empty;

    public bool isSelected = false;

    public Action<Quest.QuestCode> OnMaxOut;
    // Start is called before the first frame update
    void Start()
    {
        //if (GetComponent<EventTrigger>() != null)
        //{
        //    EventTrigger.Entry click = new EventTrigger.Entry();
        //    click.eventID = EventTriggerType.Select;
        //    click.callback = new EventTrigger.TriggerEvent();
        //    click.callback.AddListener(delegate { OnQuestItemUIClick(); });
        //    GetComponent<EventTrigger>().triggers.Add(click);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfo(string _task, int _currentProgress, int _maxProgress, int _currentIndex, string _tooltip, Quest.QuestCode _code)
    {
        TxtTask.text = _task;
        tooltip = _tooltip;

        currentProgress = _currentProgress;
        maxProgress = _maxProgress;
        currentIndex = _currentIndex;

        code = _code;
        UpdateProgressTxt();
        UpdateProgressBar();
    }

    public void SetCurrentProgress(int _additionalProgress)
    {
        currentProgress = _additionalProgress;
        UpdateProgressTxt();
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        int _currentProgress = Math.Min(currentProgress, maxProgress);
        ImgProgressBar.fillAmount = (float)currentProgress / maxProgress;
    }

    void UpdateProgressTxt()
    {
        TxtCurrentProgress.text = currentProgress.ToString() + "/" + maxProgress.ToString();
    }

    void DisplayExpandedQuestTooltip()
    {
        if(PopupGenerator.Instance)
        {
            PopupGenerator.Instance.GenerateCloseablePopup(tooltip);
        }
    }

    void ClaimRewards()
    {
        if(AccomplishmentDatabase.Instance && Inventory.Instance)
        {
            foreach(Quest.QuestReward reward in AccomplishmentDatabase.Instance.GetQuesttData(code).tasks[currentIndex].rewards)
            {
                Debug.LogWarning(reward.ToString());
                Inventory.Instance.AddToInventory(reward.item, reward.amount);
            }
        }
    }

    public void OnQuestItemUIClick()
    {
        if(currentProgress >=  maxProgress)
        {
            ClaimRewards();

            if (AccomplishmentDatabase.Instance)
            {
                Quest quest = AccomplishmentDatabase.Instance.GetQuesttData(code);
                if (quest.tasks.Count > currentIndex + 1)
                {
                    currentIndex++;
                    maxProgress = quest.tasks[currentIndex].requirement;
                    UpdateProgressTxt();
                }

                else
                {
                    MaxOut();
                }
            }

            
        }

        else
        {
            DisplayExpandedQuestTooltip();
        }
    }

    public void MaxOut()
    {
        if (OnMaxOut != null)
        {
            OnMaxOut(code);
        }
    }

    public void OnDestroy()
    {
        
    }
}
