using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Catalog : MonoBehaviour
{
    public GameObject catTypeBtn;
    public GameObject catEvolutionPathBtn;

    public GameObject catEvolutionPathMenu;
    public GameObject catEvolutionPathContent;

    public GameObject catTypeTxt;

    public GameObject catalogMenuObj;
    public GameObject catListItemObj;
    public GameObject catListContent;

    public GameObject catalogMenu;
    public GameObject allBuildingsMenu;
    public GameObject buildingInfoMenu;
    public GameObject chillSpaceInfoMenu;
    public GameObject allCatsMenu;
    public GameObject catInfoMenu;
    public GameObject catTreeMenu;



    public GameObject buildingListItemObj;
    public GameObject buildingListContent;


    public GameObject catsListItemObj2;
    public GameObject catsListContent2;

    int currx = 0; int curry = 0;

    CatType.Type currentCatTypeDisplayed;

    [SerializeField] private CatalogBuildingInfo buildingInfo;
    [SerializeField] private CatalogCatInfo catInfo;

    private List<CatDatabase.CatData> catalogCats;

    private List<LineRenderer> lr;

    public static List<GameObject> menuHistory;
    public static GameObject currentMenu;

    // Start is called before the first frame update
    void Start()
    {
        catalogCats = new List<CatDatabase.CatData>();
        //DisplayCatalog();
        
        lr = new List<LineRenderer>();

        Debug.Log("in catalog start");

        menuHistory= new List<GameObject>();
        currentMenu = catalogMenu;
    }

    private void DisplayEvolutionPathsAvailable(CatType.Type type, int x, GameObject parentButton )
    {
        
        if(!(catEvolutionPathMenu.activeInHierarchy))
        {
            catEvolutionPathMenu.SetActive(true);
        }

        if (!(catTypeTxt.activeInHierarchy))
        {
            catTypeTxt.SetActive(true);
        }

        if(parentButton == null)
            catTypeTxt.GetComponent<Text>().text = type.ToString();

        Cat cat = CatDatabase.Instance.GetCatData(type).script;
        foreach (KeyValuePair<CatType.Type, Dictionary<CatEvolutionItem.cat_evolution_item_type, int>> pair in cat.evolution_requirements)
        {
            //Debug.Log(pair.Key);


            GameObject newButtonObj = Instantiate(catEvolutionPathBtn, new Vector3(0,0,0),  Quaternion.identity, catEvolutionPathContent.transform);
            

            //newButtonObj.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
            Button buttonComp = newButtonObj.GetComponent<Button>();
            GameObject textComp = newButtonObj.transform.GetChild(0).gameObject;
            

            if(pair.Key != cat.evolution_requirements.Keys.First())
            curry++;

            Cat nextCat = CatDatabase.Instance.GetCatData(pair.Key).script;
            string tooltip = nextCat.GetCatTooltip();

            newButtonObj.transform.localPosition = new Vector3((x * 100) + 100, (curry * -100) - 200, 0);

            if(Values.collected_cat_types.Contains(pair.Key))
            {
                textComp.GetComponent<Text>().text = pair.Key.ToString();
                buttonComp.onClick.AddListener(delegate { PopupGenerator.Instance?.GenerateCloseablePopup(tooltip); });
            }

            else
            {
                textComp.GetComponent<Text>().text = "Locked";
            }


            //Debug.Log(pair.Key + ", " + (pair.Key != cat.evolution_requirements.Keys.First()).ToString());

            if (parentButton != null)
            {
                LineRenderer newLine = new GameObject().AddComponent<LineRenderer>();

                newLine.positionCount = 2;
                newLine.startWidth = 1;
                newLine.endWidth = 1;

                if (newLine == null)
                    Debug.Log("Line renderer is null");

                newLine.SetPosition(0, new Vector3(parentButton.transform.position.x + 5, parentButton.transform.position.y, parentButton.transform.position.z - 0.05f));
                newLine.SetPosition(1, new Vector3(newButtonObj.transform.position.x - 5, newButtonObj.transform.position.y, newButtonObj.transform.position.z - 0.05f));

                lr.Add(newLine);
            }
            
            DisplayEvolutionPathsAvailable(pair.Key, x+1, newButtonObj);


        }
    }

    public void DisplayNextCatalogPage()
    {
        

        int index = (int)currentCatTypeDisplayed;
        Debug.Log(index);
        if(Enum.IsDefined(typeof(CatType.Type), index + 1))
        {
            ClearCatalogPageContent();
            index++;
            CatType.Type nextCat = (CatType.Type)index;
            currentCatTypeDisplayed = nextCat;
            Debug.Log(index + " " + nextCat.ToString());


            DisplayEvolutionPathsAvailable(nextCat, 0, null);
        }

        
    }


    public void DisplayPreviousCatalogPage()
    {
        

        int index = (int)currentCatTypeDisplayed;
        Debug.Log(index);
        if (Enum.IsDefined(typeof(CatType.Type), index - 1))
        {
            ClearCatalogPageContent();
            index--;
            CatType.Type prevCat = (CatType.Type)index;
            currentCatTypeDisplayed = prevCat;
            Debug.Log(index + " " + prevCat.ToString());


            DisplayEvolutionPathsAvailable(prevCat, 0, null);
        }


    }

    private void ClearCatalogPageContent()
    {
        for (int i = 1; i < catEvolutionPathContent.transform.childCount; i++)
        {
            Destroy(catEvolutionPathContent.transform.GetChild(i).gameObject);
        }

        foreach(LineRenderer line in lr)
        {
            Destroy(line.gameObject);
        }

        lr.Clear();

        curry = 0;
    }

    public void DisplayAllCatsList()
    {
        currentMenu.SetActive(false);
        menuHistory.Add(currentMenu);
        currentMenu = allCatsMenu;
        currentMenu.SetActive(true);


        //catalogMenu.SetActive(false);
        //allCatsMenu.SetActive(true);
        if (catsListContent2.transform.childCount == 0)
            foreach (CatDatabase.CatData catData in CatDatabase.Instance.data)
            {
                //Debug.Log("here");
                GameObject newButtonObj = Instantiate(catsListItemObj2, catsListContent2.transform);
                Button buttonComp = newButtonObj.transform.GetChild(1).GetComponent<Button>();
                GameObject textComp = buttonComp.transform.GetChild(0).gameObject;
                textComp.GetComponent<Text>().text = catData.type.ToString();

                buttonComp.onClick.AddListener(delegate { DisplayCatInfo(catData.type); });
            }


        

    }

    public void DisplayAllBuildingsList()
    {
        currentMenu.SetActive(false);
        menuHistory.Add(currentMenu);
        currentMenu = allBuildingsMenu;
        currentMenu.SetActive(true);

        //catalogMenu.SetActive(false);
        //allBuildingsMenu.SetActive(true);
        Debug.Log(BuildingDatabase.Instance.data.Count);
        if(buildingListContent.transform.childCount == 0)
        foreach (BuildingDatabase.BuildingData building in BuildingDatabase.Instance.data)
        {
            //Debug.Log("here");
            GameObject newButtonObj = Instantiate(buildingListItemObj, buildingListContent.transform);
            Button buttonComp = newButtonObj.GetComponent<Button>();
            GameObject textComp = newButtonObj.transform.GetChild(0).gameObject;
            textComp.GetComponent<Text>().text = BuildingDatabase.Instance.GetDataInfo(building.type).name.ToString();

            buttonComp.onClick.AddListener(delegate { DisplayBuildingInfo(building); });
        }
    }

    public void DisplayBuildingInfo(BuildingDatabase.BuildingData buildingData)
    {
        currentMenu.SetActive(false);
        menuHistory.Add(currentMenu);
        currentMenu = buildingInfoMenu;
        currentMenu.SetActive(true);

        //allBuildingsMenu.SetActive(false);
        //buildingInfoMenu.SetActive(true);

        buildingInfo.SetBuildingDetails(buildingData);
    }

    public void DisplayCatInfo(CatType.Type catType)
    {
        currentMenu.SetActive(false);
        menuHistory.Add(currentMenu);
        currentMenu = catInfoMenu;
        currentMenu.SetActive(true);

        //allCatsMenu.SetActive(false);
        //catInfoMenu.SetActive(true);

        catInfo.SetCatInfo(catType);
        catInfo.SetCatHabitats(catType);
    }

    public void DisplayChillspaceInfo()
    {
        currentMenu.SetActive(false);
        menuHistory.Add(currentMenu);
        currentMenu = chillSpaceInfoMenu;
        currentMenu.SetActive(true);
    }

    public void DisplayCatEvolutionTree()
    {
        currentMenu.SetActive(false);
        menuHistory.Add(currentMenu);
        currentMenu = catTreeMenu;
        currentMenu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayCatalog()
    {
        if (CatDatabase.Instance && catalogMenuObj)
        {
            catalogMenuObj.SetActive(true);
            foreach (CatDatabase.CatData type in CatDatabase.Instance.data)
            {
                if (!catalogCats.Contains(type))
                {
                    GameObject newButtonObj = Instantiate(catTypeBtn, catalogMenuObj.transform);
                    Button buttonComp = newButtonObj.GetComponent<Button>();
                    GameObject textComp = newButtonObj.transform.GetChild(0).gameObject;
                    textComp.GetComponent<Text>().text = type.type.ToString();

                    buttonComp.onClick.AddListener(delegate { DisplayEvolutionPathsAvailable(type.type, 0, null); currentCatTypeDisplayed = type.type; });

                    catalogCats.Add(type);
                    
                    //Debug.Log(int(type.type));
                }

            }
        }
    }

    public void GoToPreviousMenu()
    {
        if(!(menuHistory.Count == 0))
        {
            currentMenu.SetActive(false);
            currentMenu = menuHistory.Last();
            menuHistory.Remove(currentMenu);
            currentMenu.SetActive(true);
        }

        else
        {
            currentMenu.SetActive(false);
            currentMenu = menuHistory.Last();
            currentMenu = catalogMenu;
            catalogMenu.SetActive(true);
        }

        
    }

    public void HideCatalog()
    {
        ClearCatalogPageContent();
        catEvolutionPathMenu.SetActive(false);
    }

    public void GoBackToMapScene()
    {
        LoadScene.LoadSectorUnlockingScene();
    }
}
