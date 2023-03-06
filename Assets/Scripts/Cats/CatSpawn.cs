using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatSpawn : MonoBehaviour
{

    [SerializeField] private float spawnRate = 0.10f;
    [SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 0;
    [SerializeField] private int lifespan = 0;


	private void Start()
	{
        DontDestroyOnLoad(this);
        CatDialogueGenerator.Instance?.GenerateCloseablePopup(
            "THIS IS A CAT SCRIPT WORKING !! " + "\n" + " CATERINOOOO",
            this.transform
            );
        Debug.Log("cat spawn");

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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.Log("cat click");
        AnimeowSceneManager[] managers = FindObjectsOfType<AnimeowSceneManager>();
		foreach (AnimeowSceneManager animeowSceneManager in managers)
		{
			if (animeowSceneManager.gameObject.activeSelf)
			{
                animeowSceneManager.catTapped(this.gameObject);
				Values.approached_cat = GameObject.Instantiate(this.gameObject);
				Values.approached_cat.SetActive(false);
				GameObject.DontDestroyOnLoad(Values.approached_cat);
				LoadScene.LoadCatBefriendingScene();
			}
		}

	}
}
