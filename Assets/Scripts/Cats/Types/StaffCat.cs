using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class StaffCat : Cat
{
    private static EvolutionMaterialInventory professorCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.pen, 3 },
    };

    private static EvolutionMaterialInventory securityCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.baton, 3 },
    };

    private static EvolutionMaterialInventory arrowsCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.steering_wheel, 3 },
    };

    private static EvolutionMaterialInventory healthyCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.stethoscope, 1 },
        { CatEvolutionItem.cat_evolution_item_type.syringe, 1 },
        { CatEvolutionItem.cat_evolution_item_type.dental_probe, 1 },

    };

    private static EvolutionMaterialInventory ITCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.laptop, 3 },
    };

    private static EvolutionMaterialInventory libraryCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.book, 3 },
    };

    private Dictionary<CatType.Type, EvolutionMaterialInventory> library_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.professor_cat, professorCatEvolutionInventory },
        {CatType.Type.security_cat, securityCatEvolutionInventory },
        {CatType.Type.arrows_cat, arrowsCatEvolutionInventory },
        {CatType.Type.healthy_cat, healthyCatEvolutionInventory },
        {CatType.Type.IT_cat, ITCatEvolutionInventory },
        {CatType.Type.library_cat, libraryCatEvolutionInventory }
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return library_cat_evolution_requirements;
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
        school_tip = "There is a DLSU staff directory online. www.dlsu.edu.ph/staff-directory/";
        type = CatType.Type.staff_cat;

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
        //EvolutionMaterialInventory libraryCatEvolutionInventory = new EvolutionMaterialInventory();
        //libraryCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.book, 3);

        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        //evolution_requirements.Add(cat_type.library_cat, libraryCatEvolutionInventory);
    }
}
