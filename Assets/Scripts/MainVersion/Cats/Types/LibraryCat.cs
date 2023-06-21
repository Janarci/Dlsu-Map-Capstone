using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


public class LibraryCat : Cat
{

    private static EvolutionMaterialInventory conferenceCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.book, 3 },
    };

    private Dictionary<CatType.Type, EvolutionMaterialInventory> library_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.conference_cat, conferenceCatEvolutionInventory }
    };

    public override IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return library_cat_evolution_requirements;
        }
    }
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
        school_tip = "There are self-service printers available in the Library";
        type = CatType.Type.library_cat;
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
