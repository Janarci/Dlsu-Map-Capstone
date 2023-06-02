using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AccomplishmentDatabase : MonoBehaviour
{

    public static AccomplishmentDatabase Instance;

    public List<Achievement> achievementsList;
    public List<Quest> questsList;

    private Dictionary<Achievement.AchievementCode, Achievement> achievementMappedDatabase;
    private Dictionary<Quest.QuestCode, Quest> questMappedDatabase;

    public bool isInitialized { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


        else
            Destroy(this.gameObject);

    }

    void Start()
    {
        isInitialized = false;
        //InitializeAchievementsAndQuests();
    }

    public void InitializeAchievementsAndQuests()
    {
        if (!isInitialized)
        {
            StartCoroutine(Initialize());
        }
    }

    IEnumerator Initialize()
    {
        int i = 0;
        int j = 0;
        achievementMappedDatabase = new Dictionary<Achievement.AchievementCode, Achievement>();
        questMappedDatabase = new Dictionary<Quest.QuestCode, Quest>();


        while (i < achievementsList.Count)
        {
            Achievement dataInstance = achievementsList[i];
            achievementMappedDatabase.Add(dataInstance.code, dataInstance);
            i++;
            //Debug.Log(i);
            yield return null;
        }

        while (j < questsList.Count)
        {
            Quest dataInstance = questsList[j];
            questMappedDatabase.Add(dataInstance.code, dataInstance);
            j++;
            //Debug.Log(i);
            yield return null;
        }
        isInitialized = true;
        //foreach (CatData dataInstance in data)
        //{

        //}
    }

    public Achievement GetAchievementData(Achievement.AchievementCode _type)
    {
        return achievementMappedDatabase[_type];
    }

    public Quest GetQuesttData(Quest.QuestCode _type)
    {
        return questMappedDatabase[_type];
    }
}
