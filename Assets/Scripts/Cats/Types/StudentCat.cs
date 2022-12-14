using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class StudentCat : Cat
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
        school_tip = "Students can enter DLSU by scanning the tag found on either their ID or EAF";
        this.type = cat_type.student_cat;
    }

    //protected override void InitializeCatFavors()
    //{
    //    InitializeFoodFavors(10, 20, 5);
    //    InitializeToyFavors(10, 5, 20);

    //}

    protected override void InitializeEvolutionPath()
    {
        EvolutionMaterialInventory actorCatEvolutionInventory = new EvolutionMaterialInventory();
        actorCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.script, 3);

        EvolutionMaterialInventory COBCatEvolutionInventory = new EvolutionMaterialInventory();
        actorCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.paycheck, 3);

        evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        evolution_requirements.Add(cat_type.actor_cat, actorCatEvolutionInventory);
        evolution_requirements.Add(cat_type.COB_cat, COBCatEvolutionInventory);


    }
}
