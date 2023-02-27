//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;


//public class StaffCat : Cat
//{

//    private static EvolutionMaterialInventory libraryCatEvolutionInventory = new EvolutionMaterialInventory()
//    {
//        { CatEvolutionItem.cat_evolution_item_type.book, 3 },
//    };

//    private Dictionary<cat_type, EvolutionMaterialInventory> library_cat_evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>()
//    {
//        {cat_type.library_cat, libraryCatEvolutionInventory }
//    };

//    public override IDictionary<cat_type, EvolutionMaterialInventory> evolution_requirements
//    {
//        get
//        {
//            return library_cat_evolution_requirements;
//        }
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        base.Start();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    protected override void InitializeCatType()
//    {
//        school_tip = "There is a DLSU staff directory online. www.dlsu.edu.ph/staff-directory/";
//        type = cat_type.staff_cat;
//    }

//    //protected override void InitializeCatFavors()
//    //{
//    //    InitializeFoodFavors(10, 20, 5);
//    //    InitializeToyFavors(10, 5, 20);

//    //}

//    protected override void InitializeEvolutionPath()
//    {
//        //EvolutionMaterialInventory libraryCatEvolutionInventory = new EvolutionMaterialInventory();
//        //libraryCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.book, 3);

//        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
//        //evolution_requirements.Add(cat_type.library_cat, libraryCatEvolutionInventory);
//    }
//}
