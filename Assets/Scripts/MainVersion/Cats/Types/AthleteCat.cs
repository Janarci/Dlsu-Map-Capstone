using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class AthleteCat : Cat
{

    private static EvolutionMaterialInventory varsityCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.basketball, 3 },
    };

    private Dictionary<CatType.Type, EvolutionMaterialInventory> athlete_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.varisty_cat, varsityCatEvolutionInventory }
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return athlete_cat_evolution_requirements;
        }
    }

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
        school_tip = "You can buy UAAP tickets at the Office of Sports Development!";
        this.type = CatType.Type.athlete_cat;
    }

    //protected override void InitializeCatFavors()
    //{
    //    InitializeFoodFavors(10, 20, 5);
    //    InitializeToyFavors(10, 5, 20);

    //}

    protected override void InitializeEvolutionPath()
    {
        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();

    }
}
