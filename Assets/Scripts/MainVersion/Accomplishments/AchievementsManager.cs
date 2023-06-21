using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementsManager : MonoBehaviour, IDataPersistence
{
    public static AchievementsManager instance { get; private set; }
    public Dictionary<Achievement.AchievementCode, bool> achievementsAcquired { get; private set; }
    public Dictionary<Quest.QuestCode, int> questsProgress { get; private set; }

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
        questsProgress = new Dictionary<Quest.QuestCode, int>();

        foreach (Quest q in AccomplishmentDatabase.Instance.questsList)
        {
            yield return null;
            questsProgress.Add(q.code, 0);
        }

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

    public void ProgressQuest(Quest.QuestCode _type, int _amount)
    {
        if (questsProgress.ContainsKey(_type))
        {
            if (!(questsProgress[_type] >= AccomplishmentDatabase.Instance.GetQuesttData(_type).tasks.Last().requirement))
            {
                questsProgress[_type] = Math.Min(AccomplishmentDatabase.Instance.GetQuesttData(_type).tasks.Last().requirement, questsProgress[_type] + _amount);
                Debug.Log("quest progress: " + questsProgress[_type]);
            }

            QuestsList ql = null;
            ql = FindObjectOfType<QuestsList>(true);
            if (ql)
            {
                ql.UpdateQuestItem(_type, questsProgress[_type]);
            }
        }
    }

    public void LoadGameData(GameData gameData)
    {
        foreach(GameData.QuestData _qd in gameData.quest_progress)
        {
            if(questsProgress.ContainsKey(_qd.type))
            {
                questsProgress[_qd.type] = _qd.progress;
            }
        }
    }

    public void SaveGameData(ref GameData gameData)
    {
        foreach(Quest.QuestCode _qc in questsProgress.Keys)
        {
            bool newQuest = true;

            foreach(GameData.QuestData _qd in gameData.quest_progress)
            {
                if(_qd.type == _qc)
                {
                    newQuest = false;
                    _qd.progress = questsProgress[_qc];
                    break;
                }
            }

            if(newQuest)
            {
                GameData.QuestData _qd = new GameData.QuestData();
                _qd.type = _qc;
                _qd.progress = questsProgress[_qc];
                gameData.quest_progress.Add(_qd);
            }
        }
    }
}
