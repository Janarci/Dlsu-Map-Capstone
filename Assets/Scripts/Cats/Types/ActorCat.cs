using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class ActorCat : Cat
{
    private static EvolutionMaterialInventory purrformerCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.costume, 3 },
    };

    

    private Dictionary<cat_type, EvolutionMaterialInventory> actor_cat_evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>()
    {
        {cat_type.purrformer_cat, purrformerCatEvolutionInventory }
    };

    public override IDictionary<cat_type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return actor_cat_evolution_requirements;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void InitializeCatType()
    {
        school_tip = "HTG holds plays at either the Auditorium in Yuchengco Hall, or in Bro. William Hall! Come and support them if you can.";
        type = cat_type.actor_cat;
    }

    //protected override void InitializeCatFavors()
    //{
    //    InitializeFoodFavors(10, 20, 5);
    //    InitializeToyFavors(10, 5, 20);

    //}

    protected override void InitializeEvolutionPath()
    {
        //EvolutionMaterialInventory purrformerCatEvolutionInventory = new EvolutionMaterialInventory();
        //purrformerCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.costume, 3);

        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        //evolution_requirements.Add(cat_type.purrformer_cat, purrformerCatEvolutionInventory);


    }
}
