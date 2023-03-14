using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class VarsityCat : Cat
{



    private Dictionary<CatType.Type, EvolutionMaterialInventory> varsity_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {

    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return varsity_cat_evolution_requirements;
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
        school_tip = "Athletes and varsity members may enjoy full or partial scholarship or tuition discounts!";
        this.type = CatType.Type.IT_cat;
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
