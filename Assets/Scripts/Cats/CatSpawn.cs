using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawn : MonoBehaviour
{

    [SerializeField] private float spawnRate = 0.10f;
    [SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 0;


	private void Start()
	{
        DontDestroyOnLoad(this);
	}
	public float SpawnRate
	{
		get { return spawnRate; }
	}
    public float CatchRate
    {
        get { return catchRate; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int HP
    {
        get { return hp; }
    }


	private void OnMouseDown()
	{
        Debug.Log("cat click");
        AnimeowSceneManager[] managers = FindObjectsOfType<AnimeowSceneManager>();
		foreach (AnimeowSceneManager animeowSceneManager in managers)
		{
			if (animeowSceneManager.gameObject.activeSelf)
			{
                animeowSceneManager.catTapped(this.gameObject);
			}
		}

	}
}
