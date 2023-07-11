using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AccomplishmentDatabase : MonoBehaviour
{

    public static AccomplishmentDatabase Instance;

    public List<Achievement> achievementsList;
    public List<SideQuest> sideQuestList;
    public List<MainQuest> mainQuestList;

    private Dictionary<Achievement.AchievementCode, Achievement> achievementMappedDatabase;
    private Dictionary<SideQuest.QuestCode, SideQuest> sideQuestMappedDatabase;
    private Dictionary<MainQuest.QuestCode, MainQuest> mainQuestMappedDatabase;

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
        int k = 0;
        achievementMappedDatabase = new Dictionary<Achievement.AchievementCode, Achievement>();
        sideQuestMappedDatabase = new Dictionary<SideQuest.QuestCode, SideQuest>();
        mainQuestMappedDatabase = new Dictionary<MainQuest.QuestCode, MainQuest>();


        while (i < achievementsList.Count)
        {
            Achievement dataInstance = achievementsList[i];
            achievementMappedDatabase.Add(dataInstance.code, dataInstance);
            i++;
            //Debug.Log(i);
            yield return null;
        }

        while (j < sideQuestList.Count)
        {
            SideQuest dataInstance = sideQuestList[j];
            sideQuestMappedDatabase.Add(dataInstance.code, dataInstance);
            j++;
            //Debug.Log(i);
            yield return null;
        }

        while (k < mainQuestList.Count)
        {
            MainQuest dataInstance = mainQuestList[k];
            mainQuestMappedDatabase.Add(dataInstance.info.type, dataInstance);
            k++;
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

    public SideQuest GetSideQuestData(SideQuest.QuestCode _type)
    {
        return sideQuestMappedDatabase[_type];
    }

    public MainQuest GetMainQuestData(MainQuest.QuestCode _type)
    {
        return mainQuestMappedDatabase[_type];
    }
}
