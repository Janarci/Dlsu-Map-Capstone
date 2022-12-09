using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;

public enum cat_type
{
    basic_cat,

    actor_cat,
    COB_cat,
    history_cat,
    library_cat,
    purrformer_cat,
    staff_cat,
    student_cat
}
//public enum CatBefriendingItem.cat_befriending_food
//{
//    cat_nip,
//    CatBefriendingItem.cat_befriending_food,
//    fish
//}

//public enum CatBefriendingItem.cat_befriending_toy
//{
//    yarn,
//    laser,
//    box
//}



public class Cat : MonoBehaviour
{
    private bool isWalking = false;
    private float walkingTick = 0.0f;
    private float changeDirectionInterval = 6.0f;
    private float changeDirectionIntervalMin = 4.4f;
    private float changeDirectionIntervalMax = 7.7f;

    private Vector2 walkingDirection = Vector2.zero;


    Dictionary<CatBefriendingItem.cat_befriending_food, int> food_Favor;
    Dictionary<CatBefriendingItem.cat_befriending_toy, int> toy_Favor;

    //List<CatEvolutionItem.cat_evolution_item_type> material_list;
    EvolutionMaterialInventory material_inventory;
    protected Dictionary<cat_type, EvolutionMaterialInventory> evolution_requirements;

    [SerializeField] protected string school_tip = "Welcome to DLSU";
    private float friendship_value = 0;
    private float relationship_level_value = 0;
    protected int befriendAttempts = 0;
    protected cat_type type = cat_type.basic_cat;

    private CatUI ui;

    public bool isWalkingTemp = false;
    // Start is called before the first frame update
    private void Awake()
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
    protected void Start()
    {
        
        //Roam();
        

    }



    // Update is called once per frame
    void Update()
    {
        if(isWalking)
        {
            if (walkingTick >= changeDirectionInterval)
            {
                walkingTick = 0.0f;
                RandomizeWalkingDirection();
                changeDirectionInterval = UnityEngine.Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
            }

            Vector2 direction = walkingDirection.normalized;
            gameObject.transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
            gameObject.transform.LookAt(gameObject.transform.position + new Vector3(direction.x, gameObject.transform.position.y, direction.y));

            walkingTick += Time.deltaTime;
        }

        GetComponent<Animator>()?.SetBool("isWalking", isWalkingTemp);
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
        int catnip_food_favor = UnityEngine.Random.Range(5, 20);
        int catfood_food_favor = UnityEngine.Random.Range(5, 20);
        int fish_food_favor = UnityEngine.Random.Range(5, 20);

        int yarn_toy_favor = UnityEngine.Random.Range(5, 20);
        int laser_toy_favor = UnityEngine.Random.Range(5, 20);
        int box_toy_favor = UnityEngine.Random.Range(5, 20);

        InitializeFoodFavors(catnip_food_favor, catfood_food_favor, fish_food_favor);
        InitializeToyFavors(yarn_toy_favor, laser_toy_favor, box_toy_favor);
    }

    private void InitializeInventory()
    {
        //initialize material inventory by setting all items to 0
        material_inventory = new EvolutionMaterialInventory();
        for (int i = 0; i < Enum.GetNames(typeof(CatEvolutionItem.cat_evolution_item_type)).Count(); i++)
        {
            CatEvolutionItem.cat_evolution_item_type evo_mat = (CatEvolutionItem.cat_evolution_item_type)i;
            material_inventory[evo_mat] = 0;
        }
    }

    protected virtual void InitializeEvolutionPath()
    {
        EvolutionMaterialInventory studentCatEvolutionInventory = new EvolutionMaterialInventory();
        studentCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.homework, 3);

        EvolutionMaterialInventory staffCatEvolutionInventory = new EvolutionMaterialInventory();
        staffCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.paycheck, 3);

        evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        evolution_requirements[cat_type.student_cat] = studentCatEvolutionInventory;
        evolution_requirements[cat_type.staff_cat] = staffCatEvolutionInventory;
    }

    protected void InitializeFoodFavors(int cat_nip_favor, int cat_food_favor, int fish_favor)
    {
        food_Favor = new Dictionary<CatBefriendingItem.cat_befriending_food, int>();
        food_Favor.Add(global::CatBefriendingItem.cat_befriending_food.cat_nip, cat_nip_favor);
        food_Favor.Add(global::CatBefriendingItem.cat_befriending_food.cat_food, cat_food_favor);
        food_Favor.Add(global::CatBefriendingItem.cat_befriending_food.fish, fish_favor);

    }

    protected void InitializeToyFavors(int yarn_favor, int laser_favor, int box_favor)
    {
        
        toy_Favor = new Dictionary<CatBefriendingItem.cat_befriending_toy, int>();
        toy_Favor.Add(CatBefriendingItem.cat_befriending_toy.yarn, yarn_favor);
        toy_Favor.Add(CatBefriendingItem.cat_befriending_toy.laser, laser_favor);
        toy_Favor.Add(CatBefriendingItem.cat_befriending_toy.box, box_favor);

    }

    public void InheritCatAttributes(Cat copy_cat)
    {
 
        //this.material_inventory.Clear();
        foreach (CatEvolutionItem.cat_evolution_item_type evolution_item in copy_cat.material_inventory.Keys)
        {
            this.material_inventory[evolution_item] += copy_cat.material_inventory[evolution_item];
        }

        this.befriendAttempts = copy_cat.befriendAttempts;
        this.relationship_level_value = copy_cat.relationship_level_value;
        this.friendship_value = copy_cat.friendship_value;

    }


    protected void AttemptBefriendCat(CatBefriendingItem.cat_befriending_food given_food)
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
            //GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    public void FeedCat(CatBefriendingItem.cat_befriending_food toEatFood)
    {
        AttemptBefriendCat(toEatFood);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    protected void AttemptBefriendCat(CatBefriendingItem.cat_befriending_toy given_toy)
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
            //GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayWithCat(CatBefriendingItem.cat_befriending_toy toPlayToy)
    {
        AttemptBefriendCat(toPlayToy);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public void GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type mat)
    {
        material_inventory[mat] += 1;
        Debug.Log("giving");
    }

    public int GetMaterialCount(CatEvolutionItem.cat_evolution_item_type mat)
    {
        return material_inventory[mat];
    }

    public void DisplayCatToolTip()
    {
        if(ui)
        {

        }
    }

    public string GetCatTooltip()
    {
        return this.school_tip;
    }
    public cat_type GetCatType()
    {
        return this.type;
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

            foreach(KeyValuePair<CatEvolutionItem.cat_evolution_item_type, int> required_inventory in evolution_requirements_map.Value)
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

    public Cat EvolveTo(cat_type evolve_type)
    {
        Debug.Log("evolving");
        if(CanEvolveTo(evolve_type))
        {
            foreach (CatEvolutionItem.cat_evolution_item_type evolution_item in evolution_requirements[evolve_type].Keys)
            {
                EvolutionMaterialInventory inv = evolution_requirements[evolve_type];
                material_inventory[evolution_item] -= inv[evolution_item];


            
            }

            Cat evolvedCat = null;

            //switch(evolve_type)
            //{
            //    case cat_type.student_cat:
            //        {
            //            evolvedCat = gameObject.AddComponent(typeof(StudentCat)) as StudentCat;
            //            evolvedCat.InheritCatAttributes(this);
            //            Debug.Log(evolvedCat.GetCatType());
            //            Debug.Log("student");
            //            Destroy(this);
            //        }
            //        break;

            //    case cat_type.staff_cat:
            //        {
            //            evolvedCat = gameObject.AddComponent<StaffCat>();
            //            evolvedCat.InheritCatAttributes(this);
            //            Debug.Log("staff");
            //            Destroy(this);
            //        }
            //        break;
            //    case cat_type.library_cat:
            //        {
            //            evolvedCat = gameObject.AddComponent<LibraryCat>();
            //            evolvedCat.InheritCatAttributes(this);
            //            Debug.Log("library");
                        
            //        }break;
            //    default:
            //        {
            //            evolvedCat = this;
            //            Debug.Log("default");
            //        }
            //        break;
            //}

            Cat newCat = CatDatabase.Instance.GetCatData(evolve_type).script;
            //ComponentUtility.CopyComponent(newCat);
            //ComponentUtility.PasteComponentAsNew(gameObject);
                
            string catType = newCat.GetType().ToString();
            gameObject.AddComponent(Type.GetType(catType));
            foreach(Cat catComp in gameObject.GetComponents<Cat>())
            {
                if(catComp != this)
                {
                    evolvedCat = catComp;
                }
            }

            evolvedCat.InheritCatAttributes(this);
            evolvedCat.enabled = true;

            Debug.Log(evolvedCat.GetCatType());
            EventManager.CatEvolve(this, evolvedCat, evolve_type);


            Destroy(this);

            Destroy(gameObject.transform.GetChild(0).gameObject);
            GameObject newBody = Instantiate(CatDatabase.Instance?.GetCatData(evolve_type).model, gameObject.transform);
            newBody.transform.SetAsFirstSibling();

            return evolvedCat;
        }

        return this;
    }

    public float getFriendshipValue()
    {
        return friendship_value;
    }

    public float getFriendshipPercentage()
    {
        return friendship_value/60.0f;
    }

    public void StartRoam()
    {
        isWalking = true;
        gameObject.GetComponent<Animator>()?.SetBool("isWalking", isWalking);

        RandomizeWalkingDirection();
        changeDirectionInterval = UnityEngine.Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
    }

    public void StopRoaming()
    {
        isWalking = false;
        gameObject.GetComponent<Animator>()?.SetBool("isWalking", isWalking);

        walkingDirection = Vector2.zero;
        changeDirectionInterval = UnityEngine.Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
    }

    public void RandomizeWalkingDirection()
    {
        walkingDirection.x = UnityEngine.Random.Range(-10, 10);
        walkingDirection.y = UnityEngine.Random.Range(-10, 10);
    }
    public void OnMouseUp()
    {
        Debug.Log("clicked on cat");
        EventManager.CatClick(this);
    }
}
