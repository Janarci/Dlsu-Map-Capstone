using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CatSpawner : Singleton<CatSpawner>
{

    [SerializeField] private GameObject[] availableDroids;
    [SerializeField] private GameObject player;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int startingCats = 5;
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
		for (int i = 0; i < startingCats; i++)
		{
            InstantiateDroid();
            
		}
        StartCoroutine(GenerateDroids());
    }

    private IEnumerator GenerateDroids()
	{
		while (true)
		{
            InstantiateDroid();
            yield return new WaitForSeconds(waitTime);
		}
	}
    

    private void InstantiateDroid()
	{
        int index = Random.Range(0, availableDroids.Length);
        float x = player.transform.position.x + GenerateRange();
        float z = player.transform.position.z + GenerateRange();
        float y = player.transform.position.y;

        Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity);

	}

    private float GenerateRange()
	{
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
	}


}
