using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class CatSpawnerUpdated : Singleton<CatSpawnerUpdated>
{

    [SerializeField] private GameObject[] availableDroids;
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
        CatsList.num_base_cats = availableDroids.Length;
        SpawnStashedCats();
    }


    public CatSpawnerUpdated getInstance()
    {
        return Instance;
    }

    public void InstantiateDroid(int catSpawnTemplateIndex, Transform sectorTransform, Rect AreaRect)
	{
        //GameObject newDroid = Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity);
        //int index = Random.Range(0, availableDroids.Length);
        GameObject newDroid = Instantiate(availableDroids[catSpawnTemplateIndex]);

        Transform CubeBoundsObj = sectorTransform.GetChild(0);

        int spawnAttempts = 0;

        Vector3 spawnLoc = CubeBoundsObj.position;
        do
        {
            spawnAttempts++;
            float x = spawnLoc.x + GenerateRange((AreaRect.width * 0.85f)/2);
            float z = spawnLoc.z + GenerateRange((AreaRect.height * 0.85f)/2);
            float y = 0;

            Vector3 newSpawnLoc = Quaternion.LookRotation(CubeBoundsObj.forward, CubeBoundsObj.up) * (new Vector3(x, y, z) - CubeBoundsObj.position) + CubeBoundsObj.position;
            newDroid.transform.position = newSpawnLoc;
        } while (Physics.OverlapBox(newDroid.transform.position, newDroid.GetComponent<BoxCollider>().size / 2).Length > 0 && spawnAttempts < 5);

        spawnedCats.Add(newDroid);
        DontDestroyOnLoad(newDroid);

        if(newDroid.TryGetComponent<Cat>(out Cat cat))
        {
            Timers.Instance.StartCatDurationCountdown(cat);

        }

    }

    private float GenerateRange(float maxRange)
	{
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
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
        for(int i = 0; i < CatsList.stashed_cat_spawns.Count; i++)
        {
            CatsList.stashed_cat_spawns[i].SetActive(true);
            spawnedCats.Add(CatsList.stashed_cat_spawns[i]);
        }
    }
    public void OnCatClicked(Cat clickedCat)
    {
        Debug.Log(Vector3.Distance(player.transform.position, clickedCat.transform.position));
        if(IsWithinRangeOfCats(player.transform.position).Contains(clickedCat.gameObject))
        {
            //Values.approached_cat = GameObject.Instantiate(clickedCat.gameObject);
            Values.approached_cat = clickedCat.gameObject;
            Values.approached_cat.SetActive(false);
            GameObject.DontDestroyOnLoad(Values.approached_cat);
            Timers.Instance.EndCatDurationCountdown(clickedCat);
            spawnedCats.Remove(clickedCat.gameObject);
            LoadScene.LoadCatBefriendingScene();
        }    
    }

    public void OnDestroy()
    {
        EventManager.OnCatClick -= OnCatClicked;
        for(int i = 0; i < spawnedCats.Count; i++)
        {
            spawnedCats[i].SetActive(false);

            if (!(CatsList.stashed_cat_spawns.Contains(spawnedCats[i])))
                CatsList.stashed_cat_spawns.Add(spawnedCats[i]);

        }
    }




}
