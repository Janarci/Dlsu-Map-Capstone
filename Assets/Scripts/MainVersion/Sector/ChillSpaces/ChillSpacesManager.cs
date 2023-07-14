using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CatDatabase;

public class ChillSpacesManager : MonoBehaviour, IDataPersistence
{   // [SerializeField] private Dictionary<int, ChillSpace> chillSpacesList;

    [SerializeField] private Dictionary<int, ChillSpace> chillSpacesList;

    [SerializeField] private Dictionary<ChillSpace.Area, ChillSpace> chillspaceData;

    public List<ChillSpace.Area> unlocked_chillspaces;
    public static ChillSpacesManager Instance;

    public bool isInitialized { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            

        else
            Destroy(this);
    }
    void Start()
    {
        
        EventManager.OnMissionComplete += OnMissionComplete;


        //foreach(ChillSpace cs in chillSpacesList)
        //{
        //    chillspaceData[cs.GetArea()] = cs;
        //    ChillSpaceDatabase.Instance.AddChillspaceToDatabase(cs);
        //}
        isInitialized = false;
        //InitializeChillspaceManager();
        //InitializeChillspaceManager();
    }

    public void InitializeChillspaceManager()
    {
        if(!isInitialized)
            StartCoroutine(Initialize());
    }
    IEnumerator Initialize()
    {
        chillspaceData = new Dictionary<ChillSpace.Area, ChillSpace>();
        chillSpacesList = new Dictionary<int, ChillSpace>();

        int i = 0;


        ChillSpace[] tempChillSpacesList = GameObject.FindObjectsOfType<ChillSpace>(true);
        //Array.Sort(tempChillSpacesList, tempChillSpacesList.));
        foreach(ChillSpace cs in tempChillSpacesList)
        {
            if(!chillSpacesList.ContainsKey((cs.gameObject.GetComponent<Mission>().getId() - 15)))
            {
                chillSpacesList.Add(cs.gameObject.GetComponent<Mission>().getId()-15, cs);
                Debug.Log("adding chillspaces index " + (cs.gameObject.GetComponent<Mission>().getId() - 15));
                chillspaceData[cs.GetArea()] = cs;
                ChillSpaceDatabase.Instance.AddChillspaceToDatabase(cs);
            }

            yield return null;
        }

        isInitialized = true;

    }
    public void UnlockChillSpace(int i)
    {
        Debug.Log("attempting to unlock chillspace " + i);
        if (chillSpacesList[i] != null) 
        {
            chillSpacesList[i].Unlock();
            
        }
    }

    public void UnlockChillSpace(ChillSpace.Area area)
    {
        Debug.Log("attempting to unlock chillspace: " + area);
        if (chillspaceData.ContainsKey(area))
        {
            chillspaceData[area].Unlock();
            AchievementsManager.instance.PerformMainQuest(MainQuest.QuestCode.unlock_chillspace);
        }
    }

    public void LockChillSpace(ChillSpace.Area area)
    {
        Debug.Log("attempting to lock chillspace: " + area);
        if (chillspaceData.ContainsKey(area))
        {
            chillspaceData[area].Lock();
        }
    }


    public void EndChillspaceCooldown(ChillSpace.Area area)
    {
        chillspaceData[area]?.EndCooldown();
    }

    public void GetItemFromChillSpace(int i)
    {
        chillSpacesList[i].GiveItem();
    }

    public bool GetItemFromChillSpace(ChillSpace.Area area)
    {
        chillspaceData[area].GiveItem();

        return chillspaceData[area].isCooldown;
    }

    public void GetItemFromChillspaceQuiz(ChillSpace.Area area, bool _isSuccess)
    {
        chillspaceData[area].GiveItemFromQuiz(_isSuccess);
    }

    void OnMissionComplete(int missionID)
    {
        //Debug.Log("getting item from chillspace1");

        //if (missionID >= 15 && missionID <= 43)
        //{
        //    if (!(chillSpacesList[missionID - 15]).isLocked)
        //    {
        //        Debug.Log("getting item from chillspace2");
        //        GetItemFromChillSpace(missionID - 15);
        //    }
                
        //}
    }

    public void LoadGameData(GameData gameData)
    {
        foreach (ChillSpace.Area cs in gameData.unlocked_chillspaces)
        {
            unlocked_chillspaces.Add(cs);
            chillspaceData[cs].Unlock();

        }
    }

    public void SaveGameData(ref GameData gameData)
    {
        foreach(ChillSpace.Area cs in unlocked_chillspaces)
        {
            
            if(!gameData.unlocked_chillspaces.Contains(cs))
            {
                gameData.unlocked_chillspaces.Add(cs);
            }
            
        }
    }
}
