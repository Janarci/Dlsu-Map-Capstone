using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChillSpace : MonoBehaviour
{
    [Serializable]
    public enum Area
    {
        agno_stalls,
        bloemen_stalls,
        culture_and_arts_office,
        enrollment_services_hub,
        gate_1,
        gate_2,
        gate_3,
        gate_4a,
        gate_4b,
        gate_5a,
        gate_5b,
        gate_6,
        gate_7,
        gate_8,
        health_services_office,
        la_casita_roja_restaurant,
        natividad_fajardo_rosario_gonzales,
        office_of_admissions_and_scholarships,
        office_of_information_technology_services,
        office_of_sports_development,
        office_of_student_affairs,
        office_of_student_leadership,
        office_of_the_university_registrar,
        pericos_grill,
        student_disciple_formation_office,
        student_media_office,
        teresa_g_yuchengco_auditorium,
        the_learning_commons,
        william_shaw_little_theater
    }

    [Serializable]
    public class Detail
    {
        public string areaName;
        [SerializeField] public Area area;
        [SerializeField] public Sprite picture;
        [SerializeField] public string location;
        [SerializeField] public string info;
        [SerializeField] public string email;
        [SerializeField] public string contactNumber;
        [SerializeField] public string officeHours;
        [SerializeField] public List<CatEvolutionItem.cat_evolution_item_type> giveawayItems;
        [SerializeField] public List<CatType.Type> cateredCats;

    }
            
    public bool isLocked
    {
        get;
        private set;
    }

    [SerializeField] ChillspaceUI ui;
    [SerializeField] public Detail detail;
    [SerializeField] bool isCooldown;



    void Start()
    {
        isLocked = true;

        ui.OnChillspaceUIButtonPress += GiveItem;
        ui.OnChillspaceUIPicturePress += DisplayChillspaceInfoInCatalog;

        ui.SetName(detail.areaName);
        //ui.SetInfo(detail.info);
        //ui.SetNumber(detail.contactNumber);
        //ui.SetEmail(detail.email);
        //ui.SetItems(detail.giveawayItems);
        ui.SetPicture(detail.picture);
    }

    // Update is called once per frame
    void Update()
    {
        if(Timers.Instance.chillspaceCooldowns.ContainsKey(detail.area))
        {
            int intTimeRemaining = (int)Timers.Instance?.chillspaceCooldowns[detail.area].TimeRemaining();
            ui?.SetCD(intTimeRemaining.ToString());
        }
        
    }

    //public void Lock()
    //{
    //    isLocked = true;
    //}
    public void Unlock()
    {
        if(isLocked)
        {
            //DataPersistenceManager.instance.gameData.unlocked_chillspaces.Add(this.GetArea());
            Timers.Instance?.AddChillspaceAreaCooldown(this.GetArea());
            Debug.Log("Unlocked a chillspace");
            ui.SetBtnText("Claim Item");
            isLocked = false;

            if(!ChillSpacesManager.Instance.unlocked_chillspaces.Contains(detail.area))
            {
                ChillSpacesManager.Instance.unlocked_chillspaces.Add(detail.area);
            }
        }
    }

    public void Lock()
    {
        if (!isLocked)
        {
            //DataPersistenceManager.instance.gameData.unlocked_chillspaces.Add(this.GetArea());
            Timers.Instance?.RemoveChillspaceAreaCooldoown(this.GetArea());
            Debug.Log("Lock a chillspace");
            ui.SetBtnText("LOCKED");
            isLocked = true;
        }
    }

    public void GiveItem()
    {
        Debug.Log("giving item");
        if(isCooldown)
        {
            Debug.Log("on cooldown");
        }

        else if(isLocked)
        {
            Debug.Log(detail.areaName + "is locked");
        }

        if(!isLocked && !isCooldown)
        {

            if(detail.giveawayItems.Count == 23)
            {
                int rndItemIndex = UnityEngine.Random.Range(0, 23);
                CatEvolutionItem.cat_evolution_item_type q = (CatEvolutionItem.cat_evolution_item_type)rndItemIndex;
                Inventory.Instance?.AddToInventory(q, 2);
                AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 2);

            }
            foreach (CatEvolutionItem.cat_evolution_item_type item in detail.giveawayItems)
            {
                Inventory.Instance?.AddToInventory(item, 2);
                AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 2);

            }
            TriggerCooldown();
        }


    }

    public void GiveItemFromQuiz(bool _isSuccess)
    {
        Debug.Log("giving item");
        if (isCooldown)
        {
            Debug.Log("on cooldown");
        }

        else if (isLocked)
        {
            Debug.Log(detail.areaName + "is locked");
        }

        if (!isLocked && !isCooldown)
        {

            if (detail.giveawayItems.Count == 23)
            {
                int rndItemIndex = UnityEngine.Random.Range(0, 23);
                CatEvolutionItem.cat_evolution_item_type q = (CatEvolutionItem.cat_evolution_item_type)rndItemIndex;

                if(_isSuccess)
                {
                    Inventory.Instance?.AddToInventory(q, 2);
                    AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 2);
                }
                
                else
                {
                    Inventory.Instance?.AddToInventory(q, 2);
                    AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 2);
                }

            }

            else
            {
                if(_isSuccess)
                {
                    foreach (CatEvolutionItem.cat_evolution_item_type item in detail.giveawayItems)
                    {
                        Inventory.Instance?.AddToInventory(item, 2);
                        AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 2);

                    }
                }

                else
                {
                    foreach (CatEvolutionItem.cat_evolution_item_type item in detail.giveawayItems)
                    {
                        Inventory.Instance?.AddToInventory(item, 1);
                        AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 1);

                    }
                }    
                
            }
            foreach (CatEvolutionItem.cat_evolution_item_type item in detail.giveawayItems)
            {
                Inventory.Instance?.AddToInventory(item, 2);
                AchievementsManager.instance?.ProgressQuest(Quest.QuestCode.claim_chillspace_items, 2);

            }
            TriggerCooldown();
        }


    }

    public Area GetArea()
    {
        return detail.area;
    }

    public void TriggerCooldown()
    {
        isCooldown = true;
        Timers.Instance?.chillspaceCooldowns[detail.area].StartTimer();
    }

    public void EndCooldown()
    {
        isCooldown = false;
    }

    public void DisplayChillspaceInfoInCatalog()
    {
        MapUI mu = FindObjectOfType<MapUI>(true);
        if (mu != null)
        {
            mu.HideMainCanvasUI();
            mu.CatalogCanvas.SetActive(true);
        }
        Catalog c = FindObjectOfType<Catalog>(true);
        if(c)
        {
            //c.Start();
            c.DisplayChillspaceInfo();
        }
        CatalogChillSpaceInfo ccsi = FindObjectOfType<CatalogChillSpaceInfo>(true);
        if (ccsi)
        {
            ccsi.SetChillSpaceDetails(detail);
        }
    }

    public void UnlockCatalogTutorial()
    {
        TutorialManager.instance?.UnlockTutorial(TutorialManager.Type.open_catalog);
    }

    public void OnDestroy()
    {
        ui.OnChillspaceUIButtonPress -= DisplayChillspaceInfoInCatalog;
    }
}
