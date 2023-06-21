using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CatEvolutionRequirement
{
    [Serializable]
    public class EvolutionRequirement
    {
        public CatEvolutionItem.cat_evolution_item_type item;
        public int amount;
    }
    public CatType.Type catType;
    public List<EvolutionRequirement> requirement;
}
