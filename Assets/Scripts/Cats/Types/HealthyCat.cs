using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class HealthyCat : Cat
{
    private static EvolutionMaterialInventory doctorCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.stethoscope, 3 },
    };
    private static EvolutionMaterialInventory nurseCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.syringe, 3 },
    };
    private static EvolutionMaterialInventory dentistCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.dental_probe, 3 },
    };


    private Dictionary<CatType.Type, EvolutionMaterialInventory> healthy_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.doctor_cat, doctorCatEvolutionInventory },
        {CatType.Type.nurse_cat, nurseCatEvolutionInventory },
        {CatType.Type.dentist_cat, dentistCatEvolutionInventory },
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return healthy_cat_evolution_requirements;
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
        school_tip = "Want to look for COB students? They usually hold their classes at St. La Salle Hall!";
        this.type = CatType.Type.healthy_cat;
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
