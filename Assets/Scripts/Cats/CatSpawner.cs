using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CatSpawner : Singleton<CatSpawner>
{

    [SerializeField] private CatSpawn[] availableCatTypes; // put differenrt cats here to spawn ?
    [SerializeField] private GameObject player;
    [SerializeField] private float waitTime = 10.0f;
    [SerializeField] private int startingCats = 5;
    [SerializeField] private int MaxCats = 10;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;


    [SerializeField] private List<CatSpawn> liveCats = new List<CatSpawn>(); // cats currently in game
    private CatSpawn selectedCat;


    public List<CatSpawn> LiveCats
	{
		get { return liveCats; }
	}

    public CatSpawn SelectedCat
    {
        get { return selectedCat; }
    }


    private void Awake()
	{
        Assert.IsNotNull(availableCatTypes);
        Assert.IsNotNull(player);//player
	}

	// Start is called before the first frame update
	void Start()
    {
		for (int i = 0; i < startingCats; i++)
		{
            InstantiateCats();
        }
        //
        
        StartCoroutine(GenerateCats());
    }

    public void CatWasSelected(CatSpawn cat)
	{
        selectedCat = cat;
        liveCats.Remove(cat);

	}
    private IEnumerator GenerateCats()
	{
		while (LiveCats.Count != MaxCats)
		{
            InstantiateCats();
            yield return new WaitForSeconds(waitTime);
        }
	}
    

    private void InstantiateCats()
	{
        //put a check here if they are in an unlocked zone

        int index = Random.Range(0, availableCatTypes.Length);
        float x = player.transform.position.x + GenerateRange();
        float z = player.transform.position.z + GenerateRange();
        float y = player.transform.position.y;


        

        // Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity);

        liveCats.Add(Instantiate(availableCatTypes[index], new Vector3(x, y, z), Quaternion.identity));
    }

    private float GenerateRange()
	{
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
	}


}
