using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class StudentCat : Cat
{
    private static EvolutionMaterialInventory scholarCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.book, 3 },
    };

    private static EvolutionMaterialInventory cobCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.paycheck, 3 },
    };

    private Dictionary<CatType.Type, EvolutionMaterialInventory> student_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.actor_cat,  scholarCatEvolutionInventory},
        //{CatType.Type.COB_cat, cobCatEvolutionInventory }
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return student_cat_evolution_requirements;
        }
    }
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
        this.type = CatType.Type.student_cat;

        int catTraitType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(CatTrait.Type)).Length);
        trait = (CatTrait.Type)catTraitType;
    }

    //protected override void InitializeCatFavors()
    //{
    //    InitializeFoodFavors(10, 20, 5);
    //    InitializeToyFavors(10, 5, 20);

    //}

    protected override void InitializeEvolutionPath()
    {
        //EvolutionMaterialInventory actorCatEvolutionInventory = new EvolutionMaterialInventory();
        //actorCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.script, 3);

        //EvolutionMaterialInventory COBCatEvolutionInventory = new EvolutionMaterialInventory();
        //actorCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.paycheck, 3);

        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        //evolution_requirements.Add(cat_type.actor_cat, actorCatEvolutionInventory);
        //evolution_requirements.Add(cat_type.COB_cat, COBCatEvolutionInventory);


    }
}
