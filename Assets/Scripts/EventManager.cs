using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int> OnMissionComplete;
    public event Action<Cat> OnCatBefriendSuccess;
    public event Action<Cat> OnCatBefriendFail;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this);
    }

    public void MissionComplete(int missionId)
    {
        if(OnMissionComplete != null)
        {
            OnMissionComplete(missionId);
        }
    }

    public void CatBefriendSuccess(Cat befriendedCat)
    {
        if (OnCatBefriendSuccess != null)
        {
            OnCatBefriendSuccess(befriendedCat);
            Values.befriended_cats.Add(befriendedCat.gameObject);
        }
    }

    public void CatBefriendFail(Cat befriendedCat)
    {
        if (OnCatBefriendFail != null)
        {
            OnCatBefriendFail(befriendedCat);
            Debug.Log("fail");
        }
    }
}
