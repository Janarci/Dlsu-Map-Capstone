using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
   
    
    void Start()
    {
        isLocked = false;


        ui.SetName(detail.areaName);
        ui.SetInfo(detail.info);
        ui.SetNumber(detail.contactNumber);
        ui.SetEmail(detail.email);
        ui.SetItems(detail.giveawayItems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void GiveItem()
    {
        if(!isLocked)
        {
            foreach (CatEvolutionItem.cat_evolution_item_type item in detail.giveawayItems)
            {
                Inventory.Instance?.AddToInventory(item, 1);
            }
        }

        
    }

    public Area GetArea()
    {
        return detail.area;
    }
}
