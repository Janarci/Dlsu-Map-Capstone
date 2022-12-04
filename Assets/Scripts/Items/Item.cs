using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum item_type
{
    cat_befriending_material,
    cat_evolution_material,
    furniture
}
public class Item
{
    

    item_type itemType;
    public Item(item_type type)
    {
        this.itemType = type;
    }
}
