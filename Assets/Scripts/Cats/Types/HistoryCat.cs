using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class HistoryCat : Cat
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Debug.Log(this.GetCatType());
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void InitializeCatType()
    {
        school_tip = "There’s a Filipinana Section in Learning Commons! It’s on the 9th floor.";
        this.type = cat_type.history_cat;
    }

    //protected override void InitializeCatFavors()
    //{
    //    InitializeFoodFavors(10, 20, 5);
    //    InitializeToyFavors(10, 5, 20);

    //}

    protected override void InitializeEvolutionPath()
    {
        evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();


    }
}
