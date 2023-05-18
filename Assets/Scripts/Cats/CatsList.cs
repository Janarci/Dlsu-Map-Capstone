using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatsList : MonoBehaviour, IDataPersistence
{
    public static CatsList instance { get; private set; }
    public List<GameObject> befriended_cats;
    public GameObject[] selected_cats;
    public List<GameObject> stashed_cat_spawns;
    public List<KeyValuePair<int, int>> queuedSpawns;
    public bool spawn_cats;
    public int num_base_cats;

    public int num_sectors;

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
    CatsList()
    {
        befriended_cats= new List<GameObject>();
        selected_cats = new GameObject[4];
        stashed_cat_spawns = new List<GameObject>();
        queuedSpawns = new List<KeyValuePair<int, int>>();
        spawn_cats = true;
        num_base_cats = -1;
        num_sectors = -1;
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

    public void LoadGameData(GameData gameData)
    {
        foreach(GameData.CatData loadedCatData in gameData.befriended_cats)
        {
            GameObject loadedCatObj = Instantiate(base_cat_template);
            Destroy(loadedCatObj.GetComponent<Cat>());
            CatType.Type loadedCatType = loadedCatData.type;
            Cat newCat = CatDatabase.Instance.GetCatData(loadedCatType).script;
            Cat loadedCatComp = (Cat)loadedCatObj.AddComponent(Type.GetType(newCat.GetType().ToString()));
            Debug.Log("Loaded Cat of type " + newCat + " from file");
            Destroy(loadedCatObj.transform.GetChild(0).gameObject);
            GameObject newBody = Instantiate(CatDatabase.Instance?.GetCatData(loadedCatData.type).model, loadedCatObj.transform);
            newBody.transform.SetSiblingIndex(0);
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
    }

    public void SaveGameData(ref GameData gameData)
    {
        foreach(GameObject catObj in befriended_cats)
        {
            Cat catComp = catObj.GetComponent<Cat>();
            GameData.CatData catData = new GameData.CatData();
            catData.id = catObj.GetInstanceID();
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
            gameData.befriended_cats.Add(catData);
        }
    }
}
