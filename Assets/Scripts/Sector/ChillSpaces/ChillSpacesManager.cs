using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillSpacesManager : MonoBehaviour
{
    [SerializeField] private List<ChillSpace> chillSpacesList;
    [SerializeField] private Dictionary<ChillSpace.Area, ChillSpace> chillspaceData;

    public static ChillSpacesManager Instance;
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

        chillspaceData = new Dictionary<ChillSpace.Area, ChillSpace>();
        foreach(ChillSpace cs in chillSpacesList)
        {
            chillspaceData[cs.GetArea()] = cs;
            ChillSpaceDatabase.Instance.AddChillspaceToDatabase(cs);
        }
    }
    public void UnlockChillSpace(int i)
    {
        Debug.Log("attempting to unlock and give item from sector: " + i);
        if (chillSpacesList[i] != null) 
        {
            chillSpacesList[i].Unlock();
            chillSpacesList[i].GiveItem();
        }
    }

    void OnMissionComplete(int missionID)
    {
        if (missionID >= 12 && missionID <= 29)
        {
            UnlockChillSpace(missionID - 12);
        }
    }
}
