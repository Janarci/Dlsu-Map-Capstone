using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class CatSpawnerUpdated : Singleton<CatSpawnerUpdated>
{
    [SerializeField] private GameObject[] availableDroids;
    [SerializeField] List<CatType.Type> BasicCatSpawnPool;
    [SerializeField] List<CatType.Type> AdvancedCatSpawnPool;
    [SerializeField] GameObject baseCatTemplate;
    [SerializeField] private GameObject player;
    [SerializeField] private int catsPerSpawn = 3;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private List<GameObject> spawnedCats;

	private void Awake()
	{
        //Assert.IsNotNull(availableDroids);
        //Assert.IsNotNull(player);//player
	}

	// Start is called before the first frame update
	void Start()
    {
        EventManager.OnCatClick += OnCatClicked;
        CatsManager.instance.num_base_cats = availableDroids.Length;
        SpawnStashedCats();
    }


    public CatSpawnerUpdated getInstance()
    {
        return Instance;
    }

    public GameObject InstantiateDroid()
	{
		if (spawnedCats.Count > 10)
		{
            return null;
		}
        //GameObject newDroid = Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity);


        //int index = UnityEngine.Random.Range(0, availableDroids.Length);
        //GameObject newDroid = Instantiate(availableDroids[index]);

        GameObject newDroid = null;
        float rng = UnityEngine.Random.Range(0.0f, 1.0f);
        if (rng >= 0.75)
        {
            int advSpawnIndex = UnityEngine.Random.Range(0, AdvancedCatSpawnPool.Count);
            newDroid = CreateCat(AdvancedCatSpawnPool[advSpawnIndex]);
        }

        else
        {
            int basicSpawnindex = UnityEngine.Random.Range(0, BasicCatSpawnPool.Count);
            newDroid = CreateCat(BasicCatSpawnPool[basicSpawnindex]);
        }

        //Transform CubeBoundsObj = sectorTransform.GetChild(0);

        //int spawnAttempts = 0;

        //Vector3 spawnLoc = CubeBoundsObj.position;
        //do
        //{
        //    spawnAttempts++;
        //    float x = spawnLoc.x + GenerateRange((AreaRect.width * 0.85f)/2);
        //    float z = spawnLoc.z + GenerateRange((AreaRect.height * 0.85f)/2);
        //    float y = 0;

        //    Vector3 newSpawnLoc = Quaternion.LookRotation(CubeBoundsObj.forward, CubeBoundsObj.up) * (new Vector3(x, y, z) - CubeBoundsObj.position) + CubeBoundsObj.position;
        //    newDroid.transform.position = newSpawnLoc;
        //} while (Physics.OverlapBox(newDroid.transform.position, newDroid.GetComponent<BoxCollider>().size / 2).Length > 0 && spawnAttempts < 5);

        spawnedCats.Add(newDroid);
        DontDestroyOnLoad(newDroid);

        if(newDroid.TryGetComponent<Cat>(out Cat cat))
        {
            Timers.Instance?.StartCatDurationCountdown(cat);
        }

        TutorialManager.instance.UnlockTutorial(TutorialManager.Type.cat_spawn);
        return newDroid;

    }

    GameObject CreateCat(CatType.Type _type)
    {
        GameObject loadedCatObj = Instantiate(baseCatTemplate);
        Destroy(loadedCatObj.GetComponent<Cat>());
        CatType.Type loadedCatType = _type;
        Cat newCat = CatDatabase.Instance.GetCatData(loadedCatType).script;
        Cat loadedCatComp = (Cat)loadedCatObj.AddComponent(Type.GetType(newCat.GetType().ToString()));
        Debug.Log("Loaded Cat of type " + newCat + " from file");

        if (CatDatabase.Instance.GetCatData(loadedCatType).accessories.Length != 0)
        {
            foreach (Transform t in loadedCatObj.transform.GetChild(1))
            {
                t.gameObject.Destroy();
            }

            foreach (GameObject a in CatDatabase.Instance.GetCatData(loadedCatType).accessories)
            {
                GameObject.Instantiate(a, loadedCatObj.transform.GetChild(1));
            }            
        }

        else
        {
            Destroy(loadedCatObj.transform.GetChild(0).gameObject);
            GameObject newBody = Instantiate(CatDatabase.Instance?.GetCatData(_type).model, loadedCatObj.transform);
            newBody.transform.SetSiblingIndex(0);
        }

        

        return loadedCatObj;
    }
    

    List<GameObject> IsWithinRangeOfCats(Vector3 pointOfComparison)
    {
        List<GameObject> catsWithinDetectionRange = new List<GameObject>();

        foreach(GameObject spawnedCatObj in spawnedCats)
        {
            if(Vector3.Distance(pointOfComparison, spawnedCatObj.transform.position) <= 30.0f)
            {
                catsWithinDetectionRange.Add(spawnedCatObj);
            }
        }

        return catsWithinDetectionRange;
    }

    public void RemoveCatFromSpawnList(GameObject cat)
    {
        if(spawnedCats.Contains(cat))
        {
            spawnedCats.Remove(cat);
        }
    }

    public void SpawnStashedCats()
    {
        for(int i = 0; i < CatsManager.instance.stashed_cat_spawns.Count; i++)
        {
            CatsManager.instance.stashed_cat_spawns[i].SetActive(true);
            spawnedCats.Add(CatsManager.instance.stashed_cat_spawns[i]);
        }
    }
    public void OnCatClicked(Cat clickedCat)
    {
        Debug.Log(Vector3.Distance(player.transform.position, clickedCat.transform.position));
        if(IsWithinRangeOfCats(player.transform.position).Contains(clickedCat.gameObject))
        {
            Debug.Log("animeowing time");

            //Values.approached_cat = GameObject.Instantiate(clickedCat.gameObject);
            DataPersistenceManager.instance.gameData.approached_cat = clickedCat.gameObject;
            DataPersistenceManager.instance.gameData.approached_cat.SetActive(false);
            GameObject.DontDestroyOnLoad(DataPersistenceManager.instance.gameData.approached_cat);
            Timers.Instance?.EndCatDurationCountdown(clickedCat);
            spawnedCats.Remove(clickedCat.gameObject);
            LoadScene.LoadCatBefriendingScene();
        }    
    }   

    public void OnDestroy()
    {
        EventManager.OnCatClick -= OnCatClicked;
        for(int i = 0; i < spawnedCats.Count; i++)
        {
            if (spawnedCats[i])
            {
                spawnedCats[i].SetActive(false);

                if (!(CatsManager.instance.stashed_cat_spawns.Contains(spawnedCats[i])))
                    CatsManager.instance.stashed_cat_spawns.Add(spawnedCats[i]);
            }
            

        }
    }




}
