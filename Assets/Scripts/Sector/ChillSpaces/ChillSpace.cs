using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillSpace : MonoBehaviour
{
    public bool isLocked
    {
        get;
        private set;
    }

    [SerializeField] private List<CatEvolutionItem.cat_evolution_item_type> giveawayItems;
    // Start is called before the first frame update
    void Start()
    {
        
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
        foreach(CatEvolutionItem.cat_evolution_item_type item in giveawayItems) 
        {
            Inventory.Instance?.AddToInventory(item, 1);
        }
    }
}
