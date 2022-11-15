using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDebug : MonoBehaviour
{
    public GameObject currentCat = null;
    public List<GameObject> catObjList;
    public CatUI ui;

    // Start is called before the first frame update
    void Start()
    {
        if (Values.befriended_cats == null)
            Values.befriended_cats = new List<GameObject>();

        EventManager.Instance.OnCatBefriendSuccess += RemoveCat;
        EventManager.Instance.OnCatBefriendFail += RemoveCat;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCheetaMesh()
    {
        if(currentCat == null)
        {
            currentCat = GameObject.Instantiate(catObjList[0], new Vector3(0, 0, 0), Quaternion.Euler(-90, 180, 0), null);
            currentCat.SetActive(true);
        }
    }
        

    public void SetBlackPanterMesh()
    {
        if (currentCat == null)
        {
            currentCat = GameObject.Instantiate(catObjList[1], new Vector3(0, 0, 0), Quaternion.Euler(-90, 180, 0), null);
            currentCat.SetActive(true);
        }

    }

    public void SetTigerMesh()
    {
        if (currentCat == null)
        {
            currentCat = GameObject.Instantiate(catObjList[2], new Vector3(0, 0, 0), Quaternion.Euler(-90, 180, 0), null);
            currentCat.SetActive(true);
        }
    }

    public void SetWhiteBengalMesh()
    {
        if (currentCat == null)
        {
            currentCat = GameObject.Instantiate(catObjList[3], new Vector3(0, 0, 0), Quaternion.Euler(-90, 180, 0), null);
            currentCat.SetActive(true);
        }
    }

    public void FeedCatnip()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.FeedCatNip();
        }
        
    }

    public void FeedCatFood()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.FeedCatFood();
        }
    }

    public void FeedFish()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.FeedFish();
        }
    }

    public void PlayWithYarn()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.PlayWithYarn();
        }
    }

    public void PlayWithLaser()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.PlayWithLaser();
        }
    }

    public void PlayWithBox()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.PlayWithBox();
        }
    }

    public void RemoveCat(Cat befriendedCat)
    {
        if(befriendedCat = currentCat.GetComponent<Cat>())
        {
            currentCat.SetActive(false);
            currentCat = null;
            Debug.Log("remove");
        }
    }

    public void GoToHQ()
    {
        foreach (GameObject go in Values.befriended_cats)
        {
            
        }
        LoadScene.LoadHQScene();
    }

    public void OnDestroy()
    {
        EventManager.Instance.OnCatBefriendSuccess -= RemoveCat;
        EventManager.Instance.OnCatBefriendFail -= RemoveCat;
    }

    public void GoToMenu()
    {
        LoadScene.LoadMenuScene();
    }
}
