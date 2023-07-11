using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementsManager : MonoBehaviour, IDataPersistence
{
    public static AchievementsManager instance { get; private set; }
    public Dictionary<Achievement.AchievementCode, bool> achievementsAcquired { get; private set; }
    public Dictionary<SideQuest.QuestCode, int> sideQuestsProgress { get; private set; }

    public MainQuest currentMainQuest;

    public bool isInitialized { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isInitialized = false;
    }

    public void InitializeAccomplishmentsManager()
    {
        if(!isInitialized)
        {
            StartCoroutine(Initialize());
        }
    }
    IEnumerator Initialize()
    {
        achievementsAcquired = new Dictionary<Achievement.AchievementCode, bool>();
        sideQuestsProgress = new Dictionary<SideQuest.QuestCode, int>();

        foreach (SideQuest q in AccomplishmentDatabase.Instance.sideQuestList)
        {
            yield return null;
            sideQuestsProgress.Add(q.code, 0);
        }

        currentMainQuest = AccomplishmentDatabase.Instance.mainQuestList[0];

        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockAchievement(Achievement.AchievementCode _type)
    {
        if(!achievementsAcquired.ContainsKey(_type))
        {
            if (!achievementsAcquired[_type])
                achievementsAcquired[_type] = true;

            if(PopupGenerator.Instance)
            {
                PopupGenerator.Instance.GenerateAchievementPopup(_type);
            }
        }
    }

    public void ProgressSideQuest(SideQuest.QuestCode _type, int _amount)
    {
        if (sideQuestsProgress.ContainsKey(_type))
        {
            if (!(sideQuestsProgress[_type] >= AccomplishmentDatabase.Instance.GetSideQuestData(_type).tasks.Last().requirement))
            {
                sideQuestsProgress[_type] = Math.Min(AccomplishmentDatabase.Instance.GetSideQuestData(_type).tasks.Last().requirement, sideQuestsProgress[_type] + _amount);
                Debug.Log("quest progress: " + sideQuestsProgress[_type]);
            }

            QuestsList ql = null;
            ql = FindObjectOfType<QuestsList>(true);
            if (ql)
            {
                ql.UpdateSideQuestItem(_type, sideQuestsProgress[_type]);
            }
        }
    }

    public void FinishCurrentMainQuest(MainQuest.QuestCode _type)
    {
        if(currentMainQuest.info.type == _type && AccomplishmentDatabase.Instance)
        {
            currentMainQuest = AccomplishmentDatabase.Instance.GetMainQuestData(currentMainQuest.info.next);

            QuestsList ql = null;
            ql = FindObjectOfType<QuestsList>(true);
            if (ql)
            {
                ql.UpdateMainQuestItem(_type, currentMainQuest.instruction);
            }
        }
    }

    public void LoadGameData(GameData gameData)
    {
        foreach(GameData.QuestData _qd in gameData.quest_progress)
        {
            if(sideQuestsProgress.ContainsKey(_qd.type))
            {
                sideQuestsProgress[_qd.type] = _qd.progress;
            }
        }
    }

    public void SaveGameData(ref GameData gameData)
    {
        foreach(SideQuest.QuestCode _qc in sideQuestsProgress.Keys)
        {
            bool newQuest = true;

            foreach(GameData.QuestData _qd in gameData.quest_progress)
            {
                if(_qd.type == _qc)
                {
                    newQuest = false;
                    _qd.progress = sideQuestsProgress[_qc];
                    break;
                }
            }

            if(newQuest)
            {
                GameData.QuestData _qd = new GameData.QuestData();
                _qd.type = _qc;
                _qd.progress = sideQuestsProgress[_qc];
                gameData.quest_progress.Add(_qd);
            }
        }
    }
}
