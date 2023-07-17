using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefCat : Cat
{



    //private Dictionary<CatType.Type, EvolutionMaterialInventory> USG_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    //{

    //};

    //public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    //{
    //    get
    //    {
    //        return USG_cat_evolution_requirements;
    //    }
    //}

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
        this.type = CatType.Type.chef_cat;
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
