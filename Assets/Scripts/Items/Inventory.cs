using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CatDatabase;
using static CatEvolutionItem;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public class ItemData
    {
        public CatEvolutionItem.cat_evolution_item_type type;
        public Sprite icon;
    }

    public List<ItemData> allItemsList;

    private Dictionary<cat_evolution_item_type, ItemData> itemDatabase;
    public static Inventory Instance;


    Dictionary<cat_evolution_item_type, int> itemList;
    Dictionary<cat_evolution_item_type, GameObject> inventoryItems;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private GameObject inventoryContent;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


        else
            Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        itemDatabase = new Dictionary<cat_evolution_item_type, ItemData>();
        itemList = new Dictionary<cat_evolution_item_type, int>();
        inventoryItems = new Dictionary<cat_evolution_item_type, GameObject>();
        foreach (ItemData item in allItemsList)
        {
            itemDatabase[item.type] = item;
        }

        //itemList[cat_evolution_item_type.dental_probe] = 6;
        //itemList[cat_evolution_item_type.basketball] = 5;
        //itemList[cat_evolution_item_type.boombox] = 9;
        //itemList[cat_evolution_item_type.book] = 11;


        AddToInventory(cat_evolution_item_type.dental_probe, 6);
        AddToInventory(cat_evolution_item_type.basketball, 9);
        AddToInventory(cat_evolution_item_type.boombox, 11);
        AddToInventory(cat_evolution_item_type.book, 15);

        AddToInventory(cat_evolution_item_type.dental_probe, 20);



        //UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    private void UpdateInventory()
    {
        Debug.Log("adding item to inventory: ");

        foreach (KeyValuePair<cat_evolution_item_type, int> pair in itemList)
        {
            if (!(inventoryItems.ContainsKey(pair.Key)))
            {
                Debug.Log("adding new item to inventory: " + pair.Key);
                GameObject itemToAdd = Instantiate(itemUI, inventoryContent.transform);

                Text itemCountTxt = itemToAdd.GetComponentInChildren<Text>();
                itemCountTxt.text = pair.Value.ToString();

                Sprite itemIcon = itemDatabase[pair.Key].icon;

                if (itemIcon)
                    itemToAdd.GetComponent<Image>().sprite = itemIcon;

                inventoryItems[pair.Key] = itemToAdd;
            }

            else
            {
                
            }
        }
    }

    public void AddToInventory(cat_evolution_item_type item, int amount)
    {
        if (!(inventoryItems.ContainsKey(item)))
        {

            itemList[item] = amount;

            Debug.Log("adding new item to inventory: " + item);
            GameObject itemToAdd = Instantiate(itemUI, inventoryContent.transform);

            Text itemCountTxt = itemToAdd.GetComponentInChildren<Text>();
            itemCountTxt.text = amount.ToString();

            Sprite itemIcon = itemDatabase[item].icon;

            if (itemIcon)
                itemToAdd.GetComponent<Image>().sprite = itemIcon;

            inventoryItems[item] = itemToAdd;
        }

        else
        {
            itemList[item] += amount;
            Text textComp = inventoryItems[item].GetComponentInChildren<Text>();
            if(textComp)
            {
                textComp.text = itemList[item].ToString();
            }
                
        }
    }

    public void RemoveFromInventory(cat_evolution_item_type item, int amount)
    {
        if (!(inventoryItems.ContainsKey(item)))
        {
            Debug.Log("dont have item, cant remove: " + item);
        }

        else
        {
            itemList[item] -= amount;

            if (itemList[item] <= 0)
            {
                itemList.Remove(item);

                GameObject itemToDestroy = inventoryItems[item];
                inventoryItems.Remove(item);
                Destroy(itemToDestroy);

            }
            else
            {
                Text textComp = inventoryItems[item].GetComponentInChildren<Text>();
                if (textComp)
                {
                    textComp.text = itemList[item].ToString();
                }
            }
        }
    }

    public void GiveToCat(Cat cat, CatEvolutionItem.cat_evolution_item_type item)
    {
        if(itemList.ContainsKey(item))
        {
            if (itemList.ContainsKey(item) && itemList[item] != 0) 
            {
                cat.GiveEvolutionMaterial(item);
                RemoveFromInventory(item, 1);
            }
        }
    }

}
