using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    
    public static event Action<int> OnMissionComplete;
    public static event Action<Cat> OnCatClick;
    public static event Action<Cat> OnCatBefriendSuccess;
    public static event Action<Cat, int> OnHQCatReplace;
    public static event Action<Cat,Cat, CatType.Type> OnCatEvolve;
    public static event Action<Cat, bool> OnCatBefriend;


    public static event Action<Sector, Building> OnInitializeSector;
    public static event Action<Sector, Building> OnReleaseSector;


    public static event Action OnInitializeMap;

    public static event Action<Achievement.AchievementCode> OnAchievementUnlock;
    public static event Action<Quest.QuestCode> OnQuestProgress;

    private void Awake()
    {
        
    }

    public static void MissionPerformed(int missionId)
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
            CatsManager.instance.befriended_cats.Add(befriendedCat.gameObject);
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

    public static void HQCatReplaced(Cat replacementCat, int index)
    {
        if (OnHQCatReplace != null)
        {
            Debug.Log("replaced cat");
            if(index >= 0 && index<= 3)
            {
                CatsManager.instance.selected_cats[index] = replacementCat.gameObject;
            }

            OnHQCatReplace(replacementCat, index);
        }
    }

    public static void CatEvolve(Cat oldCat, Cat newCat, CatType.Type evolvedType)
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

    public static void InitializeSector(Sector sector, Building building)
    {
        if(OnInitializeSector != null)
        {
            OnInitializeSector(sector, building);
        }
    }

    public static void ReleaseSector(Sector sector, Building building)
    {
        if (OnReleaseSector != null)
        {
            OnReleaseSector(sector, building);
        }
    }

    public static void InitializeMap()
    {
        if(OnInitializeMap != null)
        {
            OnInitializeMap();
        }
    }

    public static void UnlockAchievement(Achievement.AchievementCode _code)
    {
        if(OnAchievementUnlock != null)
        {
            OnAchievementUnlock(_code);
        }
    }

    public static void ProgressQuest(Quest.QuestCode _code)
    {
        if(OnQuestProgress != null)
        {
            OnQuestProgress(_code);
        }
    }
}
