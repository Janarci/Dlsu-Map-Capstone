using System.Collections;
using System.Collections.Generic;
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
        Assert.IsNotNull(availableDroids);
        Assert.IsNotNull(player);//player
	}

	// Start is called before the first frame update
	void Start()
    {
        EventManager.OnCatClick += OnCatClicked;
    }

    
    public CatSpawnerUpdated getInstance()
    {
        return Instance;
    }

    public void InstantiateDroid(Transform sectorTransform, Rect AreaRect)
	{
        //GameObject newDroid = Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity);
        int index = Random.Range(0, availableDroids.Length);
        GameObject newDroid = Instantiate(availableDroids[index]);

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

    public void OnCatClicked(Cat clickedCat)
    {
        Debug.Log(Vector3.Distance(player.transform.position, clickedCat.transform.position));
        if(IsWithinRangeOfCats(player.transform.position).Contains(clickedCat.gameObject))
        {
            Values.approached_cat = GameObject.Instantiate(clickedCat.gameObject);
            Values.approached_cat.SetActive(false);
            GameObject.DontDestroyOnLoad(Values.approached_cat);
            LoadScene.LoadCatBefriendingScene();
        }    
    }

    public void OnDestroy()
    {
        EventManager.OnCatClick -= OnCatClicked;
    }




}
