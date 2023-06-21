using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class DancerCat : Cat
{



    private Dictionary<CatType.Type, EvolutionMaterialInventory> dancer_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {

    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return dancer_cat_evolution_requirements;
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
        school_tip = "The La Salle Dance Company (LSDC) actually comprises 3 independent organizations according to the style of dance! These are Contemporary, Street, and Folk!";
        this.type = CatType.Type.dancer_cat;
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
