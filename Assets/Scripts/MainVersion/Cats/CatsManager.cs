using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;

public class CatsManager : MonoBehaviour, IDataPersistence
{
    public static CatsManager instance { get; private set; }
    public List<GameObject> befriended_cats;
    public List<CatType.Type> collected_cat_types;
    public GameObject[] selected_cats;
    public List<GameObject> stashed_cat_spawns;
    public List<KeyValuePair<int, int>> queuedSpawns;
    public Dictionary<CatType.Type, bool[]> unlocked_tooltips;
    public bool spawn_cats;
    public int num_base_cats;

    public int num_sectors;

    public int spawn_index { get; private set; }
    [SerializeField] private GameObject base_cat_template;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    CatsManager()
    {
        befriended_cats= new List<GameObject>();
        selected_cats = new GameObject[4];
        stashed_cat_spawns = new List<GameObject>();
        queuedSpawns = new List<KeyValuePair<int, int>>();
        unlocked_tooltips = new Dictionary<CatType.Type, bool[]>();
        spawn_cats = true;
        num_base_cats = -1;
        num_sectors = -1;
        spawn_index = 0;
    }

    public void AddNewCatToQueue()
    {
        if(num_base_cats != -1 && num_sectors != -1)
        {
            int catIndex = UnityEngine.Random.Range(0, num_base_cats);
            int sectorIndex = UnityEngine.Random.Range(0, num_sectors);
            KeyValuePair<int, int> cat_spawn = new KeyValuePair<int, int>(catIndex, sectorIndex);
            queuedSpawns.Add(cat_spawn);

        }

        else
        {
            Debug.Log("undefined num cats or num sectors");
        }
    }

    public void AddExistingCatToStash(int catIndex, int sectorIndex)
    {
        KeyValuePair<int, int> cat_spawn = new KeyValuePair<int, int>(catIndex, sectorIndex);
        queuedSpawns.Add(cat_spawn);
    }

    public void SpawnCat(Transform areaTransform, Rect areaRect)
    {
        CatSpawnerUpdated csu = FindObjectOfType<CatSpawnerUpdated>();
        if (csu)
        {
            GameObject cat_spawn = csu.InstantiateDroid();
            Transform CubeBoundsObj = areaTransform.GetChild(0);

            int spawnAttempts = 0;

            LayerMask mask = 1 << 6;

            Vector3 spawnLoc = CubeBoundsObj.position;
            do
            {
                spawnAttempts++;
                float x = spawnLoc.x + GenerateRange((areaRect.width * 0.9f) / 2);
                float z = spawnLoc.z + GenerateRange((areaRect.height * 0.9f) / 2);
                float y = 0;

                Vector3 newSpawnLoc = Quaternion.LookRotation(CubeBoundsObj.forward, CubeBoundsObj.up) * (new Vector3(x, y, z) - CubeBoundsObj.position) + CubeBoundsObj.position;
                cat_spawn.transform.position = newSpawnLoc;
                Debug.Log("Attempts at positioning cat to avoid overlap: " + spawnAttempts + " || " + "Randomized location: " + newSpawnLoc);
            } while (Physics.OverlapBox(cat_spawn.transform.position, cat_spawn.GetComponent<BoxCollider>().size / 2, cat_spawn.transform.rotation, ~mask).Length > 0 && spawnAttempts < 8);

            spawn_index++;


            float GenerateRange(float maxRange)
            {
                float randomNum = UnityEngine.Random.Range(3.0f, maxRange);
                bool isPositive = UnityEngine.Random.Range(0, 10) < 5;
                return randomNum * (isPositive ? 1 : -1);
            }
        }
        
    }

    public void UnlockCatType(CatType.Type catType)
    {
        collected_cat_types.Add(catType);
        unlocked_tooltips.Add(catType, new bool[5] { true, false, false, false, false });
        AchievementsManager.instance?.ProgressSideQuest(SideQuest.QuestCode.discover_cat_types, 1);
    }

    public void LoadGameData(GameData gameData)
    {
        spawn_index = gameData.spawn_index;
        foreach(GameData.CatData loadedCatData in gameData.befriended_cats)
        {
            GameObject loadedCatObj = Instantiate(base_cat_template);
            Destroy(loadedCatObj.GetComponent<Cat>());
            CatType.Type loadedCatType = loadedCatData.type;
            Cat newCat = CatDatabase.Instance.GetCatData(loadedCatType).script;
            Cat loadedCatComp = (Cat)loadedCatObj.AddComponent(Type.GetType(newCat.GetType().ToString()));
            loadedCatComp.InitializeCat(loadedCatData.id, loadedCatData.friendship_value, loadedCatData.relationship_value, loadedCatData.relationship_level, loadedCatData.befriend_attempts, loadedCatData.trait, loadedCatData.sadnessAilmentValue, loadedCatData.boredomAilmentValue, loadedCatData.dirtAilmentValue, loadedCatData.hungerAilmentValue, loadedCatData.mat);
            Debug.Log("Loaded Cat of type " + newCat + " from file");

            if(CatDatabase.Instance.GetCatData(loadedCatType).accessories.Length != 0 )
            {
                foreach(Transform t in loadedCatObj.transform.GetChild(1))
                {
                    t.gameObject.Destroy();
                }

                foreach(GameObject a in CatDatabase.Instance.GetCatData(loadedCatType).accessories)
                {
                    GameObject.Instantiate(a, loadedCatObj.transform.GetChild(1));
                }
            }
            else
            {
                Destroy(loadedCatObj.transform.GetChild(0).gameObject);
                GameObject newBody = Instantiate(CatDatabase.Instance?.GetCatData(loadedCatData.type).model, loadedCatObj.transform);
                newBody.transform.SetSiblingIndex(0);
            }
              
            befriended_cats.Add(loadedCatObj);
            DontDestroyOnLoad(loadedCatObj);
            loadedCatObj.SetActive(false);

            for(int i = 0; i < 4; i++)
            {
                if (gameData.befriended_cats[i] == loadedCatData)
                {
                    selected_cats[i] = loadedCatObj;
                    break;
                }
            }
        }

        foreach (CatType.Type _type in gameData.collected_cat_types)
        {
            collected_cat_types.Add(_type);
        }

        //foreach (KeyValuePair<CatType.Type, bool[]> _pair in gameData.unlocked_tooltips)
        //{
        //    unlocked_tooltips.Add(_pair.Key, _pair.Value);
        //}


        //JsonUtility.FromJson<Dictionary<CatType.Type, bool[]>>(gameData.unlocked_tooltips);
        if(gameData.unlocked_tooltips != string.Empty)
        {
            unlocked_tooltips = JsonConvert.DeserializeObject<Dictionary<CatType.Type, bool[]>>(gameData.unlocked_tooltips);
        }

    }

    public void SaveGameData(ref GameData gameData)
    {
        gameData.spawn_index = spawn_index;
        foreach(GameObject catObj in befriended_cats)
        {
            bool isNewCat = true;
            Cat catComp = catObj.GetComponent<Cat>();

            foreach (GameData.CatData _c in gameData.befriended_cats)
            {

                if(_c.id == catObj.GetComponent<Cat>().id)
                {
                    isNewCat = false;

                    _c.type = catComp.GetCatType();
                    _c.friendship_value = catComp.getFriendshipValue();
                    _c.relationship_value = catComp.getRelationshipValue();
                    _c.relationship_level = catComp.getRelationshipLevel();
                    _c.befriend_attempts = catComp.getBefriendAttempts();
                    _c.trait = catComp.getTrait();
                    _c.sadnessAilmentValue = catComp.getAilmentValue(Ailment.Type.sadness);
                    _c.boredomAilmentValue = catComp.getAilmentValue(Ailment.Type.boredom);
                    _c.dirtAilmentValue = catComp.getAilmentValue(Ailment.Type.dirt);
                    _c.hungerAilmentValue = catComp.getAilmentValue(Ailment.Type.hunger);

                    break;
                }
            }

            if(isNewCat)
            {
                GameData.CatData catData = new GameData.CatData();
                catData.id = catObj.GetComponent<Cat>().id;
                catData.type = catComp.GetCatType();
                catData.friendship_value = catComp.getFriendshipValue();
                catData.relationship_value = catComp.getRelationshipValue();
                catData.relationship_level = catComp.getRelationshipLevel();
                catData.befriend_attempts = catComp.getBefriendAttempts();
                catData.trait = catComp.getTrait();
                catData.sadnessAilmentValue = catComp.getAilmentValue(Ailment.Type.sadness);
                catData.boredomAilmentValue = catComp.getAilmentValue(Ailment.Type.boredom);
                catData.dirtAilmentValue = catComp.getAilmentValue(Ailment.Type.dirt);
                catData.hungerAilmentValue = catComp.getAilmentValue(Ailment.Type.hunger);
                catData.mat = catComp.furMat;
                gameData.befriended_cats.Add(catData);
            }
            
        }
        
        foreach(CatType.Type _type in collected_cat_types)
        {
            if(!gameData.collected_cat_types.Contains(_type))
                gameData.collected_cat_types.Add(_type);
        }

        //foreach (CatType.Type _type in unlocked_tooltips.Keys)
        //{
        //    gameData.unlocked_tooltips.Add(new KeyValuePair<CatType.Type, bool[]>(_type, unlocked_tooltips[_type]));
        //    Debug.Log("saving cat tooltip");
        //}
        foreach(CatType.Type _type in unlocked_tooltips.Keys)
        {
            for(int i = 0; i < 5; i++)
            {
                Debug.Log(unlocked_tooltips[_type][i]);

            }
        }

        gameData.unlocked_tooltips = JsonConvert.SerializeObject(unlocked_tooltips, Formatting.None);

    }
}
