using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CatalogChillSpaceInfo : MonoBehaviour
{
    [SerializeField] Image chillSpacePicture;
    [SerializeField] TextMeshProUGUI chillSpaceName;
    [SerializeField] Text chillSpaceInfo;
    [SerializeField] Transform itemsListContent;
    [SerializeField] GameObject itemsListItem;
    [SerializeField] Text hrsTxt;
    [SerializeField] Text contactsTxt;

    [SerializeField] CatalogCatInfo catalogCatInfo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetChillSpaceDetails(ChillSpace.Detail data)
    {
        if(DataPersistenceManager.instance.gameData.unlocked_chillspaces.Contains(data.area))
        {
            chillSpacePicture.sprite = data.picture;
            chillSpaceName.text = data.areaName;
            chillSpaceInfo.text = data.info;

            hrsTxt.text = data.officeHours;
            contactsTxt.text = data.email + "\n" + data.contactNumber;

            AddItems(ChillSpaceDatabase.Instance.GetDataInfo(data.area).giveawayItems);
        }

        else
        {
            chillSpacePicture.sprite = data.picture;
            chillSpaceName.text = data.areaName;
            chillSpaceInfo.text = null;

            hrsTxt.text = null;
            contactsTxt.text = null;

            AddItems(null);
        }
        
    }

    public void AddItems(List<CatEvolutionItem.cat_evolution_item_type> data)
    {
        //foreach(GameObject c in chillspaceListContent.transform)
        //{
        //    Destroy(c);
        //}

        if (itemsListContent.transform.childCount > 0)
            for (int i = itemsListContent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(itemsListContent.transform.GetChild(i).gameObject);
            }

        if (data == null)
            return;

        foreach (CatEvolutionItem.cat_evolution_item_type item in data)
        {
            GameObject itemObj = Instantiate(itemsListItem, itemsListContent);
            itemObj.GetComponent<Image>().sprite = Inventory.Instance.GetDataInfo(item).icon;
        }
    }

    
}
