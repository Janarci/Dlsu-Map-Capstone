using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<evolution_material, int>;

public enum cat_type
{
    basic_cat,

    student_cat,

    staff_cat,
    library_cat
}
public enum cat_food
{
    cat_nip,
    cat_food,
    fish
}

public enum cat_toy
{
    yarn,
    laser,
    box
}

public enum evolution_material
{
    homework,
    paycheck
}

public class Cat : MonoBehaviour
{
   

    

    
    Dictionary<cat_food, int> food_Favor;
    Dictionary<cat_toy, int> toy_Favor;

    List<evolution_material> material_list;
    EvolutionMaterialInventory material_inventory;
    protected Dictionary<cat_type, EvolutionMaterialInventory> evolution_requirements;

    [SerializeField] protected string school_tip = "Welcome to DLSU";
    private int friendship_value = 0;
    protected int befriendAttempts = 0;
    protected cat_type type = cat_type.basic_cat;

    private CatUI ui;
    // Start is called before the first frame update
    protected void Start()
    {
        InitializeCat();
        if (gameObject.TryGetComponent(out CatUI cat_ui))
        {
            ui = cat_ui;
            ui.SetFriendshipBarValue(getFriendshipPercentage());
            ui.cat = this;
        }

        else
        {
            ui = gameObject.AddComponent<CatUI>();
            ui.SetFriendshipBarValue(getFriendshipPercentage());
            ui.cat = this;
            Debug.Log("cat ui added");
        }

        InitializeCat();

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeCat()
    {
        InitializeCatType();
        InitializeCatFavors();
        InitializeInventory();
        InitializeEvolutionPath();
        
    }

    protected virtual void InitializeCatType()
    {
        type = cat_type.basic_cat;
        school_tip = "Welcome to DLSU";
    }

    protected virtual void InitializeCatFavors()
    {
        InitializeFoodFavors(10, 10, 10);
        InitializeToyFavors(10, 10, 10);
    }

    private void InitializeInventory()
    {
        //initialize material inventory by setting all items to 0
        material_inventory = new EvolutionMaterialInventory();
        for (int i = 0; i < Enum.GetNames(typeof(evolution_material)).Count(); i++)
        {
            evolution_material evo_mat = (evolution_material)i;
            material_inventory[evo_mat] = 0;
        }
    }

    protected virtual void InitializeEvolutionPath()
    {
        EvolutionMaterialInventory studentCatEvolutionInventory = new EvolutionMaterialInventory();
        studentCatEvolutionInventory.Add(evolution_material.homework, 3);

        EvolutionMaterialInventory staffCatEvolutionInventory = new EvolutionMaterialInventory();
        staffCatEvolutionInventory.Add(evolution_material.paycheck, 3);

        evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        evolution_requirements[cat_type.student_cat] = studentCatEvolutionInventory;
        evolution_requirements[cat_type.staff_cat] = staffCatEvolutionInventory;
    }

    protected void InitializeFoodFavors(int cat_nip_favor, int cat_food_favor, int fish_favor)
    {
        food_Favor = new Dictionary<cat_food, int>();
        food_Favor.Add(global::cat_food.cat_nip, cat_nip_favor);
        food_Favor.Add(global::cat_food.cat_food, cat_food_favor);
        food_Favor.Add(global::cat_food.fish, fish_favor);

    }

    protected void InitializeToyFavors(int yarn_favor, int laser_favor, int box_favor)
    {
        
        toy_Favor = new Dictionary<cat_toy, int>();
        toy_Favor.Add(cat_toy.yarn, yarn_favor);
        toy_Favor.Add(cat_toy.laser, laser_favor);
        toy_Favor.Add(cat_toy.box, box_favor);

    }


    protected void AttemptBefriendCat(cat_food given_food)
    {
        if (befriendAttempts >= 5)
        {
            Debug.Log("The cat prefers to be left alone");
            EventManager.CatBefriend(this, false);
            return;
        }

        if (food_Favor[given_food] >= 0)
        {
            friendship_value = Mathf.Min(friendship_value + food_Favor[given_food], 100);
        }

        else
        {
            friendship_value = Mathf.Max(friendship_value + food_Favor[given_food], 0);
        }

        Debug.Log("Food Favor Received: " + food_Favor[given_food]);

        befriendAttempts++;

        if (getFriendshipPercentage() >= 1.0f)
        {
            EventManager.CatBefriend(this, true);
            Debug.Log("The cat has chosen to be your friend");
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    public void FeedCat(cat_food toEatFood)
    {
        AttemptBefriendCat(toEatFood);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    protected void AttemptBefriendCat(cat_toy given_toy)
    {
        if(befriendAttempts >= 5)
        {
            Debug.Log("The cat prefers to be left alone");
            EventManager.CatBefriend(this, false);
            return;
        }

        if (toy_Favor[given_toy] >= 0)
        {
            friendship_value = Mathf.Min(friendship_value + toy_Favor[given_toy], 100);
        }

        else
        {
            friendship_value = Mathf.Max(friendship_value + toy_Favor[given_toy], 0);
        }

        Debug.Log("Toy Favor Received: " + toy_Favor[given_toy]);

        befriendAttempts++;

        if(getFriendshipPercentage() >= 1.0f)
        {
            EventManager.CatBefriend(this, true);
            Debug.Log("The cat has chosen to be your friend");
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayWithCat(cat_toy toPlayToy)
    {
        AttemptBefriendCat(toPlayToy);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public void GiveEvolutionMaterial(evolution_material mat)
    {
        material_inventory[mat] += 1;
    }

    public int GetMaterialCount(evolution_material mat)
    {
        return material_inventory[mat];
    }

    public List<cat_type> GetPossibleEvolutions()
    {
        List<cat_type> possibleEvolutions = new List<cat_type>();
        foreach(KeyValuePair<cat_type, EvolutionMaterialInventory> pair in evolution_requirements)
        {
            possibleEvolutions.Add(pair.Key);
        }

        return possibleEvolutions;
    }

    public List<cat_type> GetAvailableEvolutions()
    {
        List<cat_type> availableEvolutions = new List<cat_type>();
        foreach (KeyValuePair<cat_type, EvolutionMaterialInventory> evolution_requirements_map in evolution_requirements)
        {
            bool canEvolveToType = true;

            foreach(KeyValuePair<evolution_material, int> required_inventory in evolution_requirements_map.Value)
            {
                if (material_inventory[required_inventory.Key] < required_inventory.Value)
                {
                    canEvolveToType = false;
                    break;
                }
            }

            if(canEvolveToType)
                availableEvolutions.Add(evolution_requirements_map.Key);
        }

        return availableEvolutions;
    }

    public bool CanEvolveTo(cat_type evolve_type)
    {
        return GetAvailableEvolutions().Contains(evolve_type);
    }

    public void Evolve(cat_type evolve_type)
    {
        if(CanEvolveTo(evolve_type))
        {

        }
    }

    public int getFriendshipValue()
    {
        return friendship_value;
    }

    public float getFriendshipPercentage()
    {
        return friendship_value/60.0f;
    }

    public void OnMouseUp()
    {
        Debug.Log("clicked on cat");
        EventManager.CatClick(this);
    }
}
