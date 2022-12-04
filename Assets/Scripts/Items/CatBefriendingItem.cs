using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CatBefriendingItem : Item
{
    public enum cat_befriending_item_type
    {
        food,
        toy
    }

    public enum cat_befriending_food
    {
        cat_nip,
        cat_food,
        fish
    }

    public enum cat_befriending_toy
    {
        yarn,
        laser,
        box
    }

    public cat_befriending_item_type befriendingItemType;

    private CatBefriendingItem(cat_befriending_item_type befriending_item_type) : base(item_type.cat_befriending_material)
    {
        this.befriendingItemType = cat_befriending_item_type.food;
    }

    public class CatFood : CatBefriendingItem
    {
        public cat_befriending_food foodType
        {
            get;
            private set;
        }
        public CatFood(cat_befriending_food food_type) : base(cat_befriending_item_type.food)
        {
            foodType = food_type;
        }
    }

    public class CatToy : CatBefriendingItem
    {
        public cat_befriending_toy toyType
        {
            get;
            private set;
        }
        
        public CatToy(cat_befriending_toy toy_type) : base(cat_befriending_item_type.toy)
        {
            toyType = toy_type;
        }
    }
}

