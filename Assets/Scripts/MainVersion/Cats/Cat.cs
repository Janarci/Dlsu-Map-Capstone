using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


using EvolutionMaterialInventory = System.Collections.Generic.Dictionary<CatEvolutionItem.cat_evolution_item_type, int>;

public class CatType
{
    public enum Type
    {
        basic_cat,

        actor_cat,
        COB_cat,
        history_cat,
        library_cat,
        purrformer_cat,
        staff_cat,
        student_cat,

        arrows_cat,
        athlete_cat,
        band_cat,
        broadcaster_cat,
        conference_cat,
        CSO_cat,
        dancer_cat,
        disciplinarian_cat,
        dean_cat,
        dentist_cat,
        doctor_cat,
        healthy_cat,
        IT_cat,
        journalist_cat,
        LAMBs_cat,
        leader_cat,
        nurse_cat,
        org_cat,
        press_cat,
        professor_cat,
        security_cat,
        singer_cat,
        theater_cat,
        USG_cat,
        varisty_cat,
        scholar_cat,
        BAGCED_cat,
        RVRCOB_cat,
        CLA_cat,
        COS_cat,
        CCS_cat,
        GCOE_cat,
        SOE_cat,
        chef_cat
    }

    public Type type = Type.basic_cat;
}

public class CatInteraction
{
    public enum Type
    {
        pet,
        feed,
        play,
        clean
    }
}

public class CatTrait
{
    public enum Type
    {
        none,
        friendly,
        fat,
        playful,
        clean
    }

    public enum Favor
    {
        bad = 10,
        good = 15,
        great = 25
    }

    static Dictionary<CatInteraction.Type, Favor> none_cat_interaction_favor = new Dictionary<CatInteraction.Type, Favor>()
    {
        { CatInteraction.Type.pet, Favor.good},
        { CatInteraction.Type.play, Favor.good },
        { CatInteraction.Type.feed, Favor.good},
        { CatInteraction.Type.clean, Favor.good}
    };

    static Dictionary<CatInteraction.Type, Favor> friendly_cat_interaction_favor = new Dictionary<CatInteraction.Type, Favor>()
    {
        { CatInteraction.Type.pet, Favor.great},
        { CatInteraction.Type.play, Favor.bad },
        { CatInteraction.Type.feed, Favor.good},
        { CatInteraction.Type.clean, Favor.good}
    };

    static Dictionary<CatInteraction.Type, Favor> fat_cat_interaction_favor = new Dictionary<CatInteraction.Type, Favor>()
    {
        { CatInteraction.Type.pet, Favor.good},
        { CatInteraction.Type.play, Favor.good },
        { CatInteraction.Type.feed, Favor.great},
        { CatInteraction.Type.clean, Favor.bad},
    };

    static Dictionary<CatInteraction.Type, Favor> playful_cat_interaction_favor = new Dictionary<CatInteraction.Type, Favor>()
    {
        { CatInteraction.Type.pet, Favor.good },
        { CatInteraction.Type.play, Favor.great },
        { CatInteraction.Type.feed, Favor.bad},
        { CatInteraction.Type.clean, Favor.good},
    };

    static Dictionary<CatInteraction.Type, Favor> clean_cat_interaction_favor = new Dictionary<CatInteraction.Type, Favor>()
    {
        { CatInteraction.Type.pet, Favor.bad},
        { CatInteraction.Type.play, Favor.good },
        { CatInteraction.Type.feed, Favor.good},
        { CatInteraction.Type.clean, Favor.great}
    };
    private Dictionary<CatInteraction.Type, int> interaction_favor;
    public static Dictionary<CatInteraction.Type, Favor> GetInteractionFavorFromTrait(Type type)
    {
        //interaction_favor = Dictionary<CatInteraction.Type, int>();

        switch (type)
        {
            case Type.friendly:
                {


                    return friendly_cat_interaction_favor;

                }
                break;
            case Type.fat:
                {


                    return fat_cat_interaction_favor;

                }
                break;
            case Type.playful:
                {
                    return playful_cat_interaction_favor;
                }
                break;
            case Type.clean:
                {
                    return clean_cat_interaction_favor;

                }
                break;
            default:
                {
                    return none_cat_interaction_favor;
                }
                break;
        }


        return none_cat_interaction_favor;
    }




}

public class Ailment
{
    public enum Type
    {
        sadness,
        hunger,
        boredom,
        dirt
    }

    public Type type { get; private set; }
    public Ailment(Type type)
    {
        this.type = type;

        increaseFrequency = UnityEngine.Random.Range(22, 28);
        currentTimer = 0.0f;
    }

    public void Ail(float deltaTime)
    {
        //Debug.Log(deltaTime);
        currentTimer += deltaTime;
        if (currentTimer >= increaseFrequency && !isMaxedOut())
        {

            currentValue = Math.Min(currentValue + 4, maxValue);
            //Debug.Log("[AILING] " +type.ToString() + " | " + currentValue);
            currentTimer = 0.0f;
            increaseFrequency = UnityEngine.Random.Range(22, 28);

        }
    }

    public void Recover(float deltaTime)
    {
        //Debug.Log(deltaTime);
        currentTimer += deltaTime;
        if (currentTimer >= increaseFrequency && !isZero())
        {

            currentValue = Math.Max(currentValue - 2.5f, 0);
            //Debug.Log("[RECOVERING] " + type.ToString() + " | " + currentValue);
            currentTimer = 0.0f;
            increaseFrequency = 15;

        }
    }

    public bool isMaxedOut()
    {
        return currentValue == maxValue;
    }

    public bool isZero()
    {
        return currentValue == 0;
    }

    public float currentValue = 0;
    public float maxValue = 100;
    private float increaseFrequency;
    private float currentTimer;
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
    public int id { get; private set; }
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

    private static EvolutionMaterialInventory studentCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.homework, 3 },
    };

    private static EvolutionMaterialInventory staffCatEvolutionInventory = new EvolutionMaterialInventory()
    {
        { CatEvolutionItem.cat_evolution_item_type.paycheck, 3 },
    };

    private Dictionary<CatType.Type, EvolutionMaterialInventory> basic_cat_evolution_requirements = new Dictionary<CatType.Type, EvolutionMaterialInventory>()
    {
        {CatType.Type.student_cat, studentCatEvolutionInventory   },
        {CatType.Type.staff_cat, staffCatEvolutionInventory   }

    };

    virtual public IDictionary<CatType.Type, EvolutionMaterialInventory> evolution_requirements
    {
        get
        {
            return basic_cat_evolution_requirements;
        }
    }

    [SerializeField] protected string school_tip = "Welcome to DLSU";
    private float friendship_value = 0;
    private float relationship_value = 0;
    protected int relationship_level = 0;
    protected int befriendAttempts = 0;
    protected CatType.Type type = CatType.Type.basic_cat;
    protected CatTrait.Type trait = CatTrait.Type.none;

    private Ailment sadnessAilment;
    private Ailment hungerAilmennt;
    private Ailment boredomAilment;
    private Ailment dirtAilment;

    public int furMat { get; private set; }
    public CatUI ui;

    public bool isWalkingTemp = false;
    // Start is called before the first frame update
    private void Awake()
    {
        id = -1;
        furMat = -1;
        //InitializeCat();
        ui = GetComponentInChildren<CatUI>();

        if (ui)
        {
            ui.cat = this;
            ui.SetFriendshipBarValue(getFriendshipPercentage());
        }

        //if (gameObject.TryGetComponent(out CatUI cat_ui))
        //{
        //    ui = cat_ui;
        //    ui.SetFriendshipBarValue(getFriendshipPercentage());
        //    ui.cat = this;
        //}

        //else
        //{
        //    ui = gameObject.AddComponent<CatUI>();
        //    ui.SetFriendshipBarValue(getFriendshipPercentage());
        //    ui.cat = this;
        //    Debug.Log("cat ui added");
        //}

        InitializeCat();


    }
    protected void Start()
    {

        //Roam();


    }



    // Update is called once per frame
    void Update()
    {
        //UpdateAilmentStatus();
        if (isWalking)
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

        //GetComponent<Animator>()?.SetBool("isWalking", isWalkingTemp);
    }

    private void InitializeCat()
    {

        if (id == -1 && CatsManager.instance)
            id = CatsManager.instance.spawn_index;

        InitializeCatType();
        InitializeCatFavors();
        InitializeInventory();
        InitializeEvolutionPath();
        InitializeAilments();

        if (furMat == -1 && transform.childCount > 0)
            InitializeFurType();
    }

    private void InitializeFurType()
    {
        if (CatDatabase.Instance.GetCatData(type).accessories.Length != 0)
        {
            if (CatDatabase.Instance.furPatternPool.Length > 0)
            {
                int matIndex = UnityEngine.Random.Range(0, CatDatabase.Instance.furPatternPool.Length);
                transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = CatDatabase.Instance.furPatternPool[matIndex];
                furMat = matIndex;
            }
        }
    }

    public void InitializeCat(int _id, float _friendshipValue, float _relationshipValue, int _relationshipLevel, int _befriendAttempts, CatTrait.Type _trait, float _sadnessValue, float _boredomValue, float _dirtValue, float _hungerValue, int _mat)
    {
        id = _id;
        friendship_value = _friendshipValue;
        relationship_value = _relationshipValue;
        relationship_level= _relationshipLevel;
        befriendAttempts= _befriendAttempts;
        trait = _trait;
        sadnessAilment.currentValue = _sadnessValue;
        boredomAilment.currentValue = _boredomValue;
        dirtAilment.currentValue = _dirtValue;
        hungerAilmennt.currentValue = _hungerValue;

        if(_mat >= 0)
        {
            furMat = _mat;

            if(CatDatabase.Instance.furPatternPool.Length-1 <= _mat)
                transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = CatDatabase.Instance.furPatternPool[_mat];
        }
    }

    protected virtual void InitializeCatType()
    {
        type = CatType.Type.basic_cat;
        school_tip = "Welcome to DLSU";

        int catTraitType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(CatTrait.Type)).Length);
        trait = (CatTrait.Type)catTraitType;
        Debug.Log(trait);
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

    private void InitializeAilments()
    {
        sadnessAilment = new Ailment(Ailment.Type.sadness);
        hungerAilmennt = new Ailment(Ailment.Type.hunger);
        boredomAilment = new Ailment(Ailment.Type.boredom);
        dirtAilment = new Ailment(Ailment.Type.dirt);

    }

    protected virtual void InitializeEvolutionPath()
    {
        //EvolutionMaterialInventory studentCatEvolutionInventory = new EvolutionMaterialInventory();
        //studentCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.homework, 3);

        //EvolutionMaterialInventory staffCatEvolutionInventory = new EvolutionMaterialInventory();
        //staffCatEvolutionInventory.Add(CatEvolutionItem.cat_evolution_item_type.paycheck, 3);

        //evolution_requirements = new Dictionary<cat_type, EvolutionMaterialInventory>();
        //evolution_requirements[cat_type.student_cat] = studentCatEvolutionInventory;
        //evolution_requirements[cat_type.staff_cat] = staffCatEvolutionInventory;
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

    public void Ail()
    {
        sadnessAilment.Ail(Time.deltaTime);
        hungerAilmennt.Ail(Time.deltaTime);
        boredomAilment.Ail(Time.deltaTime);
        dirtAilment.Ail(Time.deltaTime);
        UpdateAilmentBars();

    }

    public void Recover()
    {
        sadnessAilment.Recover(Time.deltaTime);
        hungerAilmennt.Recover(Time.deltaTime);
        boredomAilment.Recover(Time.deltaTime);
        dirtAilment.Recover(Time.deltaTime);
        UpdateAilmentBars();
    }

    private void UpdateAilmentBars()
    {
        ui?.SetSadnessBarValue(sadnessAilment.currentValue / sadnessAilment.maxValue);
        ui?.SetHungerValue(hungerAilmennt.currentValue / hungerAilmennt.maxValue);
        ui?.SetBoredomBarValue(boredomAilment.currentValue / boredomAilment.maxValue);
        ui?.SetDirtBarValue(dirtAilment.currentValue / dirtAilment.maxValue);
    }

    private void UpdateRelationshipBar()
    {
        ui?.SetRelationshipBarValue(getRelationshipPercentage());
    }

    private void UpdateFriendshipBar()
    {
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    private void UpdateLevelTxt()
    {
        ui?.SetLevel(relationship_level);
    }


    public void InheritCatAttributes(Cat copy_cat)
    {

        //this.material_inventory.Clear();

        this.befriendAttempts = copy_cat.befriendAttempts;
        this.relationship_value = 0;
        this.friendship_value = copy_cat.friendship_value;
        this.id = copy_cat.id;
        this.trait = copy_cat.trait;
        this.furMat = copy_cat.furMat;
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
        if (befriendAttempts >= 5)
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

        if (getFriendshipPercentage() >= 1.0f)
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

    public CatTrait.Favor AttempBefriendCat(CatInteraction.Type interaction)
    {
        Debug.Log(CatTrait.GetInteractionFavorFromTrait(trait)[interaction]);
        CatTrait.Favor favor = CatTrait.GetInteractionFavorFromTrait(trait)[interaction];
        friendship_value = Mathf.Min(friendship_value + (int)favor, 60);
        befriendAttempts++;

        if (getFriendshipPercentage() >= 1.0f)
        {
            relationship_level = 1;
            EventManager.CatBefriend(this, true);
            Debug.Log("The cat has chosen to be your friend");
            //GameObject.DontDestroyOnLoad(this.gameObject);
            AchievementsManager.instance?.ProgressSideQuest(SideQuest.QuestCode.befriend_cats, 1);
            TutorialManager.instance.UnlockTutorial(TutorialManager.Type.cat_befriend_success);
            AchievementsManager.instance.PerformMainQuest(MainQuest.QuestCode.befriend_cat);
        }

        else if (befriendAttempts >= 5)
        {
            EventManager.CatBefriend(this, false);
            TutorialManager.instance.UnlockTutorial(TutorialManager.Type.cat_befriend_fail);
        }
        ui?.SetFriendshipBarValue(getFriendshipPercentage());

        Debug.Log(getFriendshipValue());
        return favor;
    }
    public CatTrait.Favor InteractWithCat(CatInteraction.Type interaction)
    {
        TutorialManager.instance.UnlockTutorial(TutorialManager.Type.cat_relationship_up);
        //switch (interaction)
        //{
        //    case CatInteraction.Type.pet:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]) * (sadnessAilment.currentValue/sadnessAilment.maxValue), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.feed:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]) * (hungerAilmennt.currentValue / hungerAilmennt.maxValue), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.play:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]) * (boredomAilment.currentValue / boredomAilment.maxValue), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.clean:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //}
        CatTrait.Favor favor = CatTrait.GetInteractionFavorFromTrait(trait)[interaction];
        switch (interaction)
        {
            case CatInteraction.Type.pet:
                {
                    relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
                    sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
                }
                break;
            case CatInteraction.Type.feed:
                {
                    relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
                    hungerAilmennt.currentValue = Mathf.Max(0, hungerAilmennt.currentValue - 10);
                }
                break;
            case CatInteraction.Type.play:
                {
                    relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
                    boredomAilment.currentValue = Mathf.Max(0, boredomAilment.currentValue - 10);
                }
                break;
            case CatInteraction.Type.clean:
                {
                    relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
                    dirtAilment.currentValue = Mathf.Max(0, dirtAilment.currentValue - 10);
                }
                break;
        }

        Debug.Log(relationship_value);
        if (relationship_value >= 100 && relationship_level < 10)
        {
            relationship_level++;
            relationship_value = 0;
            ui?.SetLevel(relationship_level);
            Debug.Log(relationship_level);

            if (relationship_level % 2 == 0)
            {
                if (CatDatabase.Instance.GetCatData(type).tooltips.Count() >= (relationship_level/2) && !(CatsManager.instance.unlocked_tooltips[type][(relationship_level/2)]))
                {
                    CatsManager.instance.unlocked_tooltips[type][(relationship_level / 2)] = true;
                    if(PopupGenerator.Instance && CatDatabase.Instance)
                    {
                        PopupGenerator.Instance?.GenerateCloseablePopup(
                            CatDatabase.Instance?.GetCatData(type).catTypeLabel + "says: " +
                            "\n" +
                            CatDatabase.Instance?.GetCatData(type).tooltips[relationship_level/2]
                            );
                    }
                    
                }
            }
        }

        ui?.SetRelationshipBarValue(getRelationshipPercentage());
        UpdateAilmentBars();
        //if (CanEvolve())
        //{
        //    ui?.ShowInteractUI(false);
        //    ui?.ShowEvolve(true);

        //}

        if (relationship_level == 10)
        {
            ui?.ShowInteractUI(false);
            ui?.ShowEvolve(true);

        }

        return favor;
    }

    public void InteractWithCat()
    {
        TutorialManager.instance?.UnlockTutorial(TutorialManager.Type.cat_relationship_up);
        //switch (interaction)
        //{
        //    case CatInteraction.Type.pet:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]) * (sadnessAilment.currentValue/sadnessAilment.maxValue), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.feed:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]) * (hungerAilmennt.currentValue / hungerAilmennt.maxValue), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.play:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]) * (boredomAilment.currentValue / boredomAilment.maxValue), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.clean:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + CatTrait.GetInteractionFavorFromTrait(trait)[interaction]), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //}
        //CatTrait.Favor favor = CatTrait.GetInteractionFavorFromTrait(trait)[interaction];
        //switch (interaction)
        //{
        //    case CatInteraction.Type.pet:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
        //            sadnessAilment.currentValue = Mathf.Max(0, sadnessAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.feed:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
        //            hungerAilmennt.currentValue = Mathf.Max(0, hungerAilmennt.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.play:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
        //            boredomAilment.currentValue = Mathf.Max(0, boredomAilment.currentValue - 10);
        //        }
        //        break;
        //    case CatInteraction.Type.clean:
        //        {
        //            relationship_value = Mathf.Min((relationship_value + (int)favor), 100);
        //            dirtAilment.currentValue = Mathf.Max(0, dirtAilment.currentValue - 10);
        //        }
        //        break;
        //}

        Debug.Log(relationship_level);
        if (relationship_level < 10)
        {
            relationship_level++;
            relationship_value = 0;
            Debug.Log(relationship_level);

            if (relationship_level % 2 == 0)
            {
                if (CatDatabase.Instance.GetCatData(type).tooltips.Count() >= (relationship_level / 2) && !(CatsManager.instance.unlocked_tooltips[type][(relationship_level / 2)]))
                {
                    CatsManager.instance.unlocked_tooltips[type][(relationship_level / 2)] = true;
                    if (PopupGenerator.Instance && CatDatabase.Instance)
                    {
                        PopupGenerator.Instance?.GenerateCloseablePopup(
                            CatDatabase.Instance?.GetCatData(type).catTypeLabel + "says: " +
                            "\n" +
                            CatDatabase.Instance?.GetCatData(type).tooltips[relationship_level / 2]
                            );
                    }

                }
            }
        }
        ui?.SetLevel(relationship_level);
        ui?.SetRelationshipBarValue(getRelationshipPercentage());
        UpdateAilmentBars();
        if (CanEvolve())
        {
            ui?.ShowInteractUI(false);
            ui?.ShowEvolve(true);

        }
    }

    public void TreatAilment(CatInteraction.Type interaction)
    {

    }

    public float GetSadnessPercentage()
    {
        return sadnessAilment.currentValue / sadnessAilment.maxValue;
    }

    public float GetHungerPercentage()
    {
        return hungerAilmennt.currentValue / hungerAilmennt.maxValue;
    }

    public float GetBoredomPercentage()
    {
        return boredomAilment.currentValue / boredomAilment.maxValue;
    }

    public float GetDirtPercentage()
    {
        return dirtAilment.currentValue / dirtAilment.maxValue;
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
        if (ui)
        {

        }
    }

    public string GetCatTooltip()
    {
        return this.school_tip;
    }
    public CatType.Type GetCatType()
    {
        return this.type;
    }

    public List<CatType.Type> GetPossibleEvolutions()
    {
        List<CatType.Type> possibleEvolutions = new List<CatType.Type>();
        foreach (CatEvolutionRequirement evolutions in CatDatabase.Instance.GetCatData(type).evolutions)
        {
            possibleEvolutions.Add(evolutions.catType);
        }

        return possibleEvolutions;
    }

    public List<CatType.Type> GetAvailableEvolutions()
    {
        List<CatType.Type> availableEvolutions = new List<CatType.Type>();
        //foreach (KeyValuePair<CatType.Type, EvolutionMaterialInventory> evolution_requirements_map in evolution_requirements)
        //{
        //    bool canEvolveToType = true;

        //    foreach (KeyValuePair<CatEvolutionItem.cat_evolution_item_type, int> required_inventory in evolution_requirements_map.Value)
        //    {
        //        if (Inventory.Instance.Has(required_inventory.Key) < required_inventory.Value)
        //        {
        //            canEvolveToType = false;
        //            break;
        //        }
        //    }

        //    if (canEvolveToType)
        //        availableEvolutions.Add(evolution_requirements_map.Key);
        //}

        foreach (CatEvolutionRequirement evolutions in CatDatabase.Instance.GetCatData(type).evolutions)
        {
            bool canEvolveToType = true;


            foreach (CatEvolutionRequirement.EvolutionRequirement item_req in evolutions.requirement)
            {
                if (Inventory.Instance.Has(item_req.item) < item_req.amount)
                {
                    canEvolveToType = false;
                    break;
                }
            }

            if (canEvolveToType)
                availableEvolutions.Add(evolutions.catType);
        }

        return availableEvolutions;
    }

    public bool CanEvolveTo(CatType.Type evolve_type)
    {
        return (GetAvailableEvolutions().Contains(evolve_type) && relationship_level == 10);
    }

    public bool CanEvolve()
    {
        return (GetAvailableEvolutions().Count != 0 && relationship_level == 10);
    }
    public void Evolve()
    {
        List<CatType.Type> availableEvolutions = GetAvailableEvolutions();
        if (!(availableEvolutions.Count == 0))
        {
            int evolutionIndex = UnityEngine.Random.Range(0, availableEvolutions.Count);
            EvolveTo(availableEvolutions[evolutionIndex]);
        }

    }

    public Cat EvolveTo(CatType.Type evolve_type)
    {
        Debug.Log("evolving");
        if (CanEvolveTo(evolve_type))
        {
            foreach (CatEvolutionRequirement.EvolutionRequirement req in (CatDatabase.Instance.GetCatData(type).evolutions.Find(_evo => _evo.catType == evolve_type)).requirement)
            {
                //CatEvolutionItem.cat_evolution_item_type evolution_item = req.item;
                //EvolutionMaterialInventory inv = evolution_requirements[evolve_type];
                //material_inventory[evolution_item] -= inv[evolution_item];
                Inventory.Instance.RemoveFromInventory(req.item, req.amount);
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
            foreach (Cat catComp in gameObject.GetComponents<Cat>())
            {
                if (catComp != this)
                {
                    evolvedCat = catComp;
                    break;
                }
            }

            evolvedCat.InheritCatAttributes(this);
            evolvedCat.UpdateAilmentBars();
            evolvedCat.UpdateFriendshipBar();
            evolvedCat.UpdateRelationshipBar();
            evolvedCat.UpdateLevelTxt();
            evolvedCat.ui.ShowEvolve(false);
            evolvedCat.ui.ShowInteractUI(true);
            evolvedCat.enabled = true;


            Debug.Log(evolvedCat.GetCatType());
            EventManager.CatEvolve(this, evolvedCat, evolve_type);

            if(CatDatabase.Instance.GetCatData(evolve_type).accessories.Length == 0)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
                GameObject newBody = Instantiate(CatDatabase.Instance?.GetCatData(evolve_type).model, gameObject.transform);
                newBody.transform.SetAsFirstSibling();
            }

            else
            {
                foreach(Transform t in evolvedCat.transform.GetChild(1))
                {
                    Destroy(t.gameObject);
                }

                foreach(GameObject a in CatDatabase.Instance.GetCatData(evolve_type).accessories)
                {
                    Instantiate(a, evolvedCat.transform.GetChild(1));
                }
            }


            TutorialManager.instance.UnlockTutorial(TutorialManager.Type.cat_evolve);
            Destroy(this);



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
        return friendship_value / 60.0f;
    }

    public float getRelationshipValue()
    {
        return relationship_value;
    }

    public float getRelationshipPercentage()
    {
        return relationship_value / 100.0f;
    }

    public int getRelationshipLevel()
    {
        return relationship_level;
    }

    public int getBefriendAttempts()
    {
        return befriendAttempts;
    }

    public CatTrait.Type getTrait()
    {
        return trait;
    }

    public float getAilmentValue(Ailment.Type ailmentType)
    {
        float currentAilmentValue = 0;
        switch(ailmentType)
        {
            case Ailment.Type.sadness: currentAilmentValue = sadnessAilment.currentValue; break;
            case Ailment.Type.boredom: currentAilmentValue = boredomAilment.currentValue; break;
            case Ailment.Type.dirt: currentAilmentValue = dirtAilment.currentValue; break;
            case Ailment.Type.hunger: currentAilmentValue = hungerAilmennt.currentValue; break;
        }

        return currentAilmentValue;
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
    public void OnMouseDown()
    {
        Debug.Log("clicked on cat");
        EventManager.CatClick(this);
    }
}
