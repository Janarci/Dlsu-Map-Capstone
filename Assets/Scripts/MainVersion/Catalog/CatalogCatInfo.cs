using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogCatInfo : MonoBehaviour
{
    public Image catPicture;
    public TextMeshProUGUI catType;
    public Text catTooltip;
    public GameObject catItem;
    public Transform catItemsContent;

    public GameObject habitatItemObj;
    public Transform habitatItemsContent;

    public CatalogChillSpaceInfo chillspaceInfo;

    public GameObject catInfoMenu;
    public GameObject habitatInfoMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCatPicture(CatType.Type type)
    {

        if(CatDatabase.Instance.GetCatData(type).icon != null)
        {
            //if (CatsList.instance.collected_cat_types.Contains(type))
            //    catPicture.sprite = CatDatabase.Instance.GetCatData(type).icon;

            //else
            //    catPicture.sprite = null;

            catPicture.sprite = CatDatabase.Instance.GetCatData(type).icon;

        }
    }
    public void SetCatInfo(CatType.Type type)
    {
        catType.text = CatDatabase.Instance.GetCatData(type).catTypeLabel.ToString();

        if (catItemsContent.childCount > 0)
            for (int i = catItemsContent.childCount - 1; i >= 0; i--)
            {
                Destroy(catItemsContent.GetChild(i).gameObject);
            }

        if (CatsManager.instance.collected_cat_types.Contains(type))
        {
            catTooltip.text = CatDatabase.Instance.GetCatData(type).script.GetCatTooltip();

            List<CatEvolutionItem.cat_evolution_item_type> addedItems = new List<CatEvolutionItem.cat_evolution_item_type>();
            foreach (CatEvolutionRequirement direct_evolutions in CatDatabase.Instance.GetCatData(type).evolutions)
            {
                //GameObject _catItem = Instantiate(catItem, catItemsContent);
                //_catItem.GetComponent<Image>().sprite = Inventory.Instance?.GetDataInfo(item).icon;
                foreach (CatEvolutionRequirement.EvolutionRequirement requirement in direct_evolutions.requirement)
                {
                    CatEvolutionItem.cat_evolution_item_type item = requirement.item;
                    if (!addedItems.Contains(item))
                    {
                        addedItems.Add(item);
                        GameObject _catItem = Instantiate(catItem, catItemsContent);
                        _catItem.GetComponent<Image>().sprite = Inventory.Instance?.GetDataInfo(item).icon;
                        Debug.Log("added item: " + item);
                    }


                }
            }

            addedItems.Clear();
        }

        else
            catTooltip.text = "LOCKED";

    }

    public void SetCatHabitats(CatType.Type type)
    {
        CatDatabase.CatData data = CatDatabase.Instance?.GetCatData(type);

        if (habitatItemsContent.childCount > 0)
            for (int i = habitatItemsContent.childCount - 1; i >= 0; i--)
            {
                Destroy(habitatItemsContent.GetChild(i).gameObject);
            }

        if(CatsManager.instance.collected_cat_types.Contains(type))
        {
            foreach (ChillSpace.Area chillspace in ChillSpaceDatabase.Instance.GetDataList())
            {
                if (ChillSpaceDatabase.Instance.GetDataInfo(chillspace).cateredCats.Contains(type))
                {
                    GameObject newButtonObj = Instantiate(habitatItemObj, habitatItemsContent);
                    Button buttonComp = newButtonObj.transform.GetChild(0).gameObject.GetComponent<Button>();
                    Image imageComp = buttonComp.GetComponent<Image>();
                    GameObject textComp = newButtonObj.transform.GetChild(1).gameObject;
                    textComp.GetComponent<Text>().text = ChillSpaceDatabase.Instance.GetDataInfo(chillspace).areaName;

                    buttonComp.onClick.AddListener(delegate { chillspaceInfo.SetChillSpaceDetails(ChillSpaceDatabase.Instance.GetDataInfo(chillspace)); DisplayHabitatInfo(); });

                    if (ChillSpaceDatabase.Instance.GetDataInfo(chillspace).picture)
                    {
                        imageComp.sprite = ChillSpaceDatabase.Instance.GetDataInfo(chillspace).picture;
                    }
                }
            }
        }
    }

    public void DisplayHabitatInfo()
    {

        Catalog.currentMenu.SetActive(false);
        Catalog.menuHistory.Add(Catalog.currentMenu);
        Catalog.currentMenu = habitatInfoMenu;
        Catalog.currentMenu.SetActive(true);

        //buildingInfoMenu.SetActive(false);
        //chillSpaceInfoMenu.SetActive(true);

        
    }
}
