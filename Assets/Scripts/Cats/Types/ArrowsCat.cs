using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;

public class ArrowsCat : Cat
{

    private Dictionary<CatType.Type, EvolutionMaterialInventory> arrows_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {

    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return arrows_cat_evolution_requirements;
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
        school_tip = "The Arrows Express is the official DLSU shuttle service available from Mondays to Saturdays. It is free and anyone can get a ride provided they reserve a seat here: https://bit.ly/SSUShuttleSeatReservationF";
        this.type = CatType.Type.arrows_cat;
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
