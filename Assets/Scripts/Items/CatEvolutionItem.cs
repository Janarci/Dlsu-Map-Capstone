using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEvolutionItem : Item
{
    public enum cat_evolution_item_type
    {
        homework,
        paycheck
    }

    public cat_evolution_item_type evolutionItemType
    {
        get;
        private set;
    }
    public CatEvolutionItem(cat_evolution_item_type evolution_item_type) : base(item_type.cat_evolution_material)
    {
        evolutionItemType = evolution_item_type;
    }
}

