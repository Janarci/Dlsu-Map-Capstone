using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    
    public static event Action<int> OnMissionComplete;
    public static event Action<Cat> OnCatClick;
    public static event Action<Cat> OnCatBefriendSuccess;
    public static event Action<Cat,Cat, cat_type> OnCatEvolve;
    public static event Action<Cat, bool> OnCatBefriend;

    public static event Action OnInitializeMap;

    private void Awake()
    {
        
    }

    public static void MissionComplete(int missionId)
    {
        if(OnMissionComplete != null)
        {
            OnMissionComplete(missionId);
        }
    }

    public static void CatBefriendSuccess(Cat befriendedCat)
    {
        if (OnCatBefriendSuccess != null)
        {
            OnCatBefriendSuccess(befriendedCat);
            Values.befriended_cats.Add(befriendedCat.gameObject);
        }
    }

    public static void CatBefriend(Cat befriendedCat, bool isSuccess)
    {
        if (OnCatBefriend != null)
        {
            OnCatBefriend(befriendedCat, isSuccess);
            //Debug.Log("");
        }
    }

    public static void CatEvolve(Cat oldCat, Cat newCat, cat_type evolvedType)
    {
        if (OnCatEvolve != null)
        {
            OnCatEvolve(oldCat, newCat, evolvedType);
        }
    }

    public static void CatClick(Cat clickedCat)
    {
        if(OnCatClick!= null)
        {
            OnCatClick(clickedCat);
        }
    }

    public static void InitializeMap()
    {
        if(OnInitializeMap != null)
        {
            OnInitializeMap();
        }
    }
}
