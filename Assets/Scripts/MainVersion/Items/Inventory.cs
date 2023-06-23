using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CatDatabase;
using static CatEvolutionItem;

public class Inventory : MonoBehaviour, IDataPersistence
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

    public bool isInitialized { get; private set; }


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
        isInitialized = false;
        //foreach (ItemData item in allItemsList)
        //{
        //    itemDatabase[item.type] = item;
        //    AddToInventory(item.type, 99);
        //    Debug.Log("adding item " + item.type);
        //}

        //Debug.Log("Items declared: " + allItemsList.Count);

        



        //for(int i = 0; i < itemList.Count; i++)
        //{
        //    ItemData item = itemList.Keys;
        //    AddToInventory(item.type, 99);

        //}
        //itemList[cat_evolution_item_type.dental_probe] = 6;
        //itemList[cat_evolution_item_type.basketball] = 5;
        //itemList[cat_evolution_item_type.boombox] = 9;
        //itemList[cat_evolution_item_type.book] = 11;


        //AddToInventory(cat_evolution_item_type.dental_probe, 6);
        //AddToInventory(cat_evolution_item_type.basketball, 9);
        //AddToInventory(cat_evolution_item_type.boombox, 11);
        //AddToInventory(cat_evolution_item_type.book, 15);

        //AddToInventory(cat_evolution_item_type.dental_probe, 20);

        //AddToInventory(cat_evolution_item_type.cso_flag, 3);


        //UpdateInventory();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeInventory()
    {
        if(!isInitialized)
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        for (int i = 0; i < allItemsList.Count; i++)
        {
            ItemData item = allItemsList[i];
            itemDatabase[item.type] = item;
            //AddToInventory(item.type, 99);
            //Debug.Log("initial add of " + item.type);
            yield return null;
        }

        isInitialized = true;
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
        //Debug.Log("adding item to inventory: ");

        foreach (KeyValuePair<cat_evolution_item_type, int> pair in itemList)
        {
            if (!(inventoryItems.ContainsKey(pair.Key)))
            {
                //Debug.Log("adding new item to inventory: " + pair.Key);
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

            //Debug.Log("adding new item to inventory: " + item);
            //Debug.Log(inventoryContent == null); Debug.Log(itemUI == null);

            GameObject itemToAdd = Instantiate(itemUI, inventoryContent.transform);

            Text itemCountTxt = itemToAdd.GetComponentInChildren<Text>();
            itemCountTxt.text = amount.ToString();

            Sprite itemIcon = itemDatabase[item].icon;

            if (itemIcon)
                itemToAdd.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;

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

    //public void AddToInventory(cat_evolution_item_type item, int amount, Transform _inventoryContent)
    //{
    //    if (!(inventoryItems.ContainsKey(item)))
    //    {

    //        itemList[item] = amount;

    //        //Debug.Log("adding new item to inventory: " + item);
    //        //Debug.Log(inventoryContent == null); Debug.Log(itemUI == null);

    //        GameObject itemToAdd = Instantiate(itemUI, inventoryContent.transform);

    //        Text itemCountTxt = itemToAdd.GetComponentInChildren<Text>();
    //        itemCountTxt.text = amount.ToString();

    //        Sprite itemIcon = itemDatabase[item].icon;

    //        if (itemIcon)
    //            itemToAdd.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;

    //        inventoryItems[item] = itemToAdd;
    //    }

    //    else
    //    {
    //        itemList[item] += amount;
    //        Text textComp = inventoryItems[item].GetComponentInChildren<Text>();
    //        if (textComp)
    //        {
    //            textComp.text = itemList[item].ToString();
    //        }

    //    }
    //}

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

    public ItemData GetDataInfo(CatEvolutionItem.cat_evolution_item_type item)
    {
        return itemDatabase[item];
    }

    public int Has(CatEvolutionItem.cat_evolution_item_type item)
    {
        if (itemList.ContainsKey(item))
        {
            return itemList[item];
        }

        else return 0;
    }

    public void RestartInventoryUI(GameObject newContent)
    {
        if(inventoryContent == null)
        {
            inventoryContent = newContent;
            inventoryItems.Clear();
            foreach (KeyValuePair<cat_evolution_item_type, int> pair in itemList)
            {
                if (!(inventoryItems.ContainsKey(pair.Key)))
                {
                    //Debug.Log("re-adding new item to inventory: " + pair.Key);
                    GameObject itemToAdd = Instantiate(itemUI, inventoryContent.transform);

                    Text itemCountTxt = itemToAdd.GetComponentInChildren<Text>();
                    itemCountTxt.text = pair.Value.ToString();

                    Sprite itemIcon = itemDatabase[pair.Key].icon;

                    if (itemIcon)
                        itemToAdd.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;

                    inventoryItems[pair.Key] = itemToAdd;
                }

                else
                {

                }
            }
        }
    }

    public void StartInventoryUI(GameObject newContent)
    {
        if (inventoryContent == null)
        {
            inventoryContent = newContent;
            inventoryItems.Clear();
            foreach (KeyValuePair<cat_evolution_item_type, int> pair in itemList)
            {
                if (!(inventoryItems.ContainsKey(pair.Key)))
                {
                    //Debug.Log("re-adding new item to inventory: " + pair.Key);
                    GameObject itemToAdd = Instantiate(itemUI, inventoryContent.transform);

                    Text itemCountTxt = itemToAdd.GetComponentInChildren<Text>();
                    itemCountTxt.text = pair.Value.ToString();

                    Sprite itemIcon = itemDatabase[pair.Key].icon;

                    if (itemIcon)
                        itemToAdd.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;

                    inventoryItems[pair.Key] = itemToAdd;
                }

                else
                {

                }
            }
        }
    }

    public void LoadGameData(GameData gameData)
    {
        foreach (GameData.ItemData _item in gameData.inventory_items)
        {
            AddToInventory(_item.type, _item.amount);
        }
    }

    public void SaveGameData(ref GameData gameData)
    {
        
        foreach (CatEvolutionItem.cat_evolution_item_type _type in itemList.Keys)
        {
            GameData.ItemData new_item = null;
            bool isNewItem = true;
            foreach (GameData.ItemData _itemData in gameData.inventory_items)
            {
                if(_itemData.type == _type)
                {
                    isNewItem = false;
                    _itemData.amount = itemList[_type];
                    break;
                }
            }

            if(isNewItem)
            {
                new_item = new GameData.ItemData();
                new_item.type = _type;
                new_item.amount = itemList[_type];
                gameData.inventory_items.Add(new_item);
            }
        }
    }
}