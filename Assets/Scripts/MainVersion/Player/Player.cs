using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Item> inventory;
    //public static Player Instance;

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }


    //    else
    //        Destroy(this.gameObject);

    //}

    [SerializeField] GameObject warningObj;
    void Start()
    {
        //inventory= new List<Item>();
    }
    private Sector currentSector;
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Sector"))
        {
            Sector sectorScript = other.transform.parent.GetComponent<Sector>();
            if (sectorScript != null)
            {
                Debug.Log("entered a sector");

                foreach (Building.Type b in SectorManager.Instance.sectorList.Keys)
                {
                    if (sectorScript == SectorManager.Instance.sectorList[b] && !SectorManager.Instance.sectorList[b].isUnlocked)//not unlocked
                    {
                        SectorManager.Instance.UnlockSector(b); ;
                    }
                }

                if (sectorScript.GetBuilding().type == Building.Type.gokongwei_hall || sectorScript.GetBuilding().type == Building.Type.agno_gate || sectorScript.GetBuilding().type == Building.Type.sci_tech_research_center)
                {
                    //PopupGenerator.Instance.GenerateCloseablePopup("Stay alert of moving vehicles in this area!");
                    warningObj?.SetActive(true);

                }
            }

            
		}

	}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sector"))
        {
            Sector sectorScript = other.transform.parent.GetComponent<Sector>();
            if (sectorScript != null)
            {
                Debug.Log("exited a sector");


                if (sectorScript.GetBuilding().type == Building.Type.gokongwei_hall || sectorScript.GetBuilding().type == Building.Type.agno_gate || sectorScript.GetBuilding().type == Building.Type.sci_tech_research_center)
                {
                    //PopupGenerator.Instance.GenerateCloseablePopup("Stay alert of moving vehicles in this area!");
                    warningObj?.SetActive(false);

                }
            }


        }
    }

}
