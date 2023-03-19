using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CatDatabase;

public class ChillSpacesManager : MonoBehaviour
{
    [SerializeField] private List<ChillSpace> chillSpacesList;
    [SerializeField] private Dictionary<ChillSpace.Area, ChillSpace> chillspaceData;

    public static ChillSpacesManager Instance;

    public bool isInitialized { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        InitializeChillspaceManager();
    }

    void InitializeChillspaceManager()
    {
        StartCoroutine(Initialize());
    }
    IEnumerator Initialize()
    {
        chillspaceData = new Dictionary<ChillSpace.Area, ChillSpace>();
        int i = 0;

        while (i < chillSpacesList.Count)
        {
            ChillSpace cs = chillSpacesList[i];
            chillspaceData[cs.GetArea()] = cs;
            ChillSpaceDatabase.Instance.AddChillspaceToDatabase(cs);
            i++;
            Debug.Log(i);
            if (Values.unlocked_chillspaces.Contains(cs.GetArea()))
                cs.Unlock();

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
        }
    }

    public void EndChillspaceCooldown(ChillSpace.Area area)
    {
        chillspaceData[area].EndCooldown();
    }

    public void GetItemFromChillSpace(int i)
    {
        chillSpacesList[i].GiveItem();
    }

    void OnMissionComplete(int missionID)
    {
        if (missionID >= 13 && missionID <= 29)
        {
            if (!(chillSpacesList[missionID - 13]).isLocked)
                GetItemFromChillSpace(missionID - 13);
        }
    }
}
