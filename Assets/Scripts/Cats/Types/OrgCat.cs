using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class OrgCat : Cat
{

    private static EvolutionMaterialInventory purrformerCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.costume, 3 },
    };

    private static EvolutionMaterialInventory pressCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.newspaper, 1 },
        { CatEvolutionItem.cat_evolution_item_type.video_camera, 1 },

    };

    private static EvolutionMaterialInventory leaderCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.cso_flag, 1 },
        { CatEvolutionItem.cat_evolution_item_type.usg_flag, 1 },

    };

    private static EvolutionMaterialInventory athleteCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.basketball, 3 },
    };

    private static EvolutionMaterialInventory disciplinarianCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.scales_of_justice, 3 },
    };


    private Dictionary<CatType.Type, EvolutionMaterialInventory> org_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.purrformer_cat, purrformerCatEvolutionInventory },
        {CatType.Type.press_cat, pressCatEvolutionInventory },
        {CatType.Type.leader_cat, leaderCatEvolutionInventory },
        {CatType.Type.athlete_cat, athleteCatEvolutionInventory },
        {CatType.Type.disciplinarian_cat, disciplinarianCatEvolutionInventory },
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return org_cat_evolution_requirements;
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
        school_tip = "There are many student organizations in DLSU! They each have their own respective offices but overall are managed by the Office of Student Affairs.";
        this.type = CatType.Type.org_cat;

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
        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();

    }
}
