using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Catventory : MonoBehaviour
{
    private List<CatventoryItem> cats = new List<CatventoryItem>();
    [SerializeField] GameObject catventoryInteractUI;
    [SerializeField] GameObject catventoryItemTemplate;
    [SerializeField] Transform catventoryPrimaryContent;
    [SerializeField] Transform catventorySecondaryContent;
    GameObject selectedCatventoryObj = null;
    int selectedCatventoryIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        AddCats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCats()
    {
        if(!(CatsList.instance.befriended_cats.Count == 0))
        {
            foreach(GameObject befriended_cat in CatsList.instance.befriended_cats)
            {
                GameObject catventory_item = Instantiate(catventoryItemTemplate, catventoryPrimaryContent);
                Cat catComp = befriended_cat.GetComponent<Cat>();
                CatventoryItem itemComp = catventory_item.GetComponent<CatventoryItem>();
                if(catComp && itemComp)
                {
                    itemComp.SetValues(catComp);
                }

                if(catventory_item.GetComponent<EventTrigger>())
                {
                    EventTrigger.Entry click = new EventTrigger.Entry();
                    click.eventID = EventTriggerType.PointerDown;
                    click.callback = new EventTrigger.TriggerEvent();
                    click.callback.AddListener(delegate { OnCatventoryItemPress(catventory_item); });
                    catventory_item.GetComponent<EventTrigger>().triggers.Add(click);
                }

            }
        }
    }

    public void OnCatventoryItemPress(GameObject catventory_item)
    {
        for(int i = 0; i < catventoryPrimaryContent.childCount; i++)
        {
            if(catventory_item == catventoryPrimaryContent.GetChild(i).gameObject)
            {
                selectedCatventoryIndex = i;
                selectedCatventoryObj = catventory_item;

                catventorySecondaryContent.gameObject.SetActive(true);

                if(catventory_item.GetComponent<LayoutElement>() != null)
                    catventory_item.GetComponent<LayoutElement>().enabled= false;

                selectedCatventoryObj.transform.parent = catventorySecondaryContent;

                if (catventory_item.GetComponent<CatventoryItem>())
                    catventory_item.GetComponent<CatventoryItem>().ShowInteractUI(true);
                break;
            }
        }
    }

    public void OnSelectedCatventoryExit()
    {
        if(selectedCatventoryObj)
        {
            selectedCatventoryObj.transform.parent = catventoryPrimaryContent;
            selectedCatventoryObj.transform.SetSiblingIndex(selectedCatventoryIndex);

            if (selectedCatventoryObj.GetComponent<LayoutElement>() != null)
                selectedCatventoryObj.GetComponent<LayoutElement>().enabled = true;

            if (selectedCatventoryObj.GetComponent<CatventoryItem>())
                selectedCatventoryObj.GetComponent<CatventoryItem>().ShowInteractUI(true);

            selectedCatventoryObj = null;
            selectedCatventoryIndex = -1;
        }

        catventorySecondaryContent.gameObject.SetActive(false);
        //catventoryInteractUI.gameObject.SetActive(false);

    }
}
