using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : AnimeowSceneManager
{
	public CatSpawner catspawner;
	public override void catTapped(GameObject cat)
	{
		//SceneManager.LoadScene("0", LoadSceneMode.Additive);
		Debug.Log("wala");
		catspawner.CatWasSelected(cat.GetComponent<CatSpawn>());
		cat.Destroy();
	}

	public override void playerTapped(GameObject player)
	{

	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
