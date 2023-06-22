using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogBuildingInfo : MonoBehaviour
{
    [SerializeField] Image buildingPicture;
    [SerializeField] TextMeshProUGUI buildingName;
    [SerializeField] Text buildingInfo;
    [SerializeField] Transform chillspaceListContent;
    [SerializeField] GameObject chillspaceItem;

    [SerializeField] CatalogChillSpaceInfo chillSpaceInfo;

    public GameObject buildingInfoMenu;
    public GameObject chillSpaceInfoMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBuildingDetails(BuildingDatabase.BuildingData data)
    {
        //bool isSectorUnlocked = false;

        //for (int i = 0; i < DataPersistenceManager.instance.gameData.unlocked_sectors.Count; i++)
        //{
        //    //int j = DataPersistenceManager.instance.gameData.unlocked_sectors[i];
        //    Building.Type bldgType = DataPersistenceManager.instance.gameData.unlocked_sectors[i];

        //    if (bldgType == data.type)
        //    {
        //        isSectorUnlocked = true;
        //        break;
        //    }
        //}
        
        if(SectorManager.Instance.unlockedSectors.Contains(data.type))
        {
            buildingPicture.sprite = data.picture;
            buildingName.text = data.name;
            buildingInfo.text = data.description;

            AddChillSpaces(data.chillspaces);
        }
        
        else
        {
            buildingPicture.sprite = data.picture;
            buildingName.text = data.name;
            buildingInfo.text = "LOCKED";

            AddChillSpaces(null);
        }
        
    }

    public void AddChillSpaces(List<ChillSpace.Area> data)
    {
        //foreach(GameObject c in chillspaceListContent.transform)
        //{
        //    Destroy(c);
        //}
        
        if(chillspaceListContent.transform.childCount > 0)
        for(int i = chillspaceListContent.transform.childCount - 1; i >= 0; i--)
        {
            
            Destroy(chillspaceListContent.transform.GetChild(i).gameObject);
        }

        if (data == null)
            return;


        foreach (ChillSpace.Area area in data)
        {
            if(ChillSpacesManager.Instance.unlocked_chillspaces.Contains(area))
            {
                GameObject newButtonObj = Instantiate(chillspaceItem, chillspaceListContent);
                Button buttonComp = newButtonObj.transform.GetChild(1).GetComponent<Button>();
                GameObject textComp1 = newButtonObj.transform.GetChild(1).GetChild(0).gameObject;
                textComp1.GetComponent<Text>().text = ChillSpaceDatabase.Instance.GetDataInfo(area).areaName;

                buttonComp.onClick.AddListener(delegate { chillSpaceInfo.SetChillSpaceDetails(ChillSpaceDatabase.Instance.GetDataInfo(area)); DisplayChillSpaceMenu(); });
                GameObject textComp2 = newButtonObj.transform.GetChild(0).GetChild(0).gameObject;
                textComp2.GetComponent<Text>().text = ChillSpaceDatabase.Instance.GetDataInfo(area).location;
            }

            else
            {
                GameObject newButtonObj = Instantiate(chillspaceItem, chillspaceListContent);
                Button buttonComp = newButtonObj.transform.GetChild(1).GetComponent<Button>();
                GameObject textComp1 = newButtonObj.transform.GetChild(1).GetChild(0).gameObject;
                textComp1.GetComponent<Text>().text = "LOCKED";

                buttonComp.onClick.AddListener(delegate { chillSpaceInfo.SetChillSpaceDetails(ChillSpaceDatabase.Instance.GetDataInfo(area)); DisplayChillSpaceMenu(); });
                GameObject textComp2 = newButtonObj.transform.GetChild(0).GetChild(0).gameObject;
                textComp2.GetComponent<Text>().text = ChillSpaceDatabase.Instance.GetDataInfo(area).location;
            }
            

            //chillSpaceItemeObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = 
        }
    }
    public void SetChillspaceDetails(ChillSpace.Area area)
    {

    }

    public void DisplayChillSpaceMenu()
    {
        Catalog.currentMenu.SetActive(false);
        Catalog.menuHistory.Add(Catalog.currentMenu);
        Catalog.currentMenu = chillSpaceInfoMenu;
        Catalog.currentMenu.SetActive(true);

        //buildingInfoMenu.SetActive(false);
        //chillSpaceInfoMenu.SetActive(true);

    }
}
