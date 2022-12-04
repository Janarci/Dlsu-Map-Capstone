using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class CatSpawnerUpdated : Singleton<CatSpawnerUpdated>
{

    [SerializeField] private GameObject[] availableDroids;
    [SerializeField] private GameObject player;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int catsPerSpawn = 3;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;

	private void Awake()
	{
        Assert.IsNotNull(availableDroids);
        Assert.IsNotNull(player);//player
	}

	// Start is called before the first frame update
	void Start()
    {
		
    }

    
    public CatSpawnerUpdated getInstance()
    {
        return Instance;
    }

    public void InstantiateDroid(Vector3 spawnLoc)
	{
        
        int index = Random.Range(0, availableDroids.Length);
        float x = spawnLoc.x + GenerateRange();
        float z = spawnLoc.z + GenerateRange();
        float y = spawnLoc.y;

        Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity);

	}

    private float GenerateRange()
	{
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
	}


}
