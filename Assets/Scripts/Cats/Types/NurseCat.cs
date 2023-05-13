using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class NurseCat : Cat
{



    private Dictionary<CatType.Type, EvolutionMaterialInventory> nurse_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {

    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return nurse_cat_evolution_requirements;
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
        school_tip = "The HSO has a list of accredited clinics! You can find them here: https://www.dlsu.edu.ph/wp-content/uploads/pdf/hso/list-of-accredited-clinics.pdf";
        this.type = CatType.Type.nurse_cat;
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
