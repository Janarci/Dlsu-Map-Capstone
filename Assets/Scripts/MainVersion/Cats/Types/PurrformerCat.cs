using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class PurrformerCat : Cat
{

    private static EvolutionMaterialInventory bandCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.trombone, 3 },
    };

    private static EvolutionMaterialInventory theaterCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.theater_mask, 3 },
    };

    private static EvolutionMaterialInventory singerCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.microphone, 3 },
    };

    private static EvolutionMaterialInventory dancerCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.boombox, 3 },
    };

    private Dictionary<CatType.Type, EvolutionMaterialInventory> purrformer_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.band_cat, bandCatEvolutionInventory },
        {CatType.Type.theater_cat, theaterCatEvolutionInventory },
        {CatType.Type.singer_cat, singerCatEvolutionInventory },
        {CatType.Type.dancer_cat, dancerCatEvolutionInventory },
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return purrformer_cat_evolution_requirements;
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
        school_tip = "The Culture and Arts Office houses the performing arts organizations of DLSU! These include the DLSU Chorale, Lasallian Youth Orchestra, De La Salle Innersoul, the Harlequin Theatre Guild, and the La Salle Dance Companies (Contemporary/Street/Folk)";
        this.type = CatType.Type.purrformer_cat;
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
