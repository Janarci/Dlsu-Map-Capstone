using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatDebug : MonoBehaviour
{
    public GameObject canvas;
    public GameObject currentCat = null;
    public GameObject catTooltipUI = null;
    public GameObject befriendSuccess;
    public GameObject befriendFail;

    public List<GameObject> catObjList;
    public CatUI ui;

    // Start is called before the first frame update
    void Start()
    {

        EventManager.OnCatBefriend += RemoveCat;
        currentCat = Values.approached_cat;
        currentCat.transform.position = new Vector3(0, -5, 25);
        currentCat.transform.rotation = Quaternion.Euler(0, 180, 0);
        currentCat.SetActive(true);
        Debug.Log(currentCat);
        if(currentCat)
        {
            currentCat.GetComponent<Cat>().ui.ShowAffinity(true);
            currentCat.GetComponent<Cat>().ui.gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetCheetaMesh()
    {
        if (currentCat == null)
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
            cat.FeedCat(CatBefriendingItem.cat_befriending_food.cat_nip);
        }

    }

    public void FeedCatFood()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.FeedCat(CatBefriendingItem.cat_befriending_food.cat_food);
        }
    }

    public void FeedFish()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.FeedCat(CatBefriendingItem.cat_befriending_food.fish);
        }
    }

    public void PlayWithYarn()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.PlayWithCat(CatBefriendingItem.cat_befriending_toy.yarn);
        }
    }

    public void PlayWithLaser()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.PlayWithCat(CatBefriendingItem.cat_befriending_toy.laser);
        }
    }

    public void PlayWithBox()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.PlayWithCat(CatBefriendingItem.cat_befriending_toy.box);
        }
    }

    public void Pet()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.AttempBefriendCat(CatInteraction.Type.pet);
        }
    }

    public void Feed()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.AttempBefriendCat(CatInteraction.Type.feed);
        }
    }

    public void Play()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.AttempBefriendCat(CatInteraction.Type.play);
        }
    }

    public void Clean()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            cat.AttempBefriendCat(CatInteraction.Type.clean);
        }
    }

    public void RemoveCat(Cat befriendedCat, bool isCatBefriended)
    {
        if (befriendedCat == currentCat.GetComponent<Cat>())
        {
            currentCat.SetActive(false);
            currentCat = null;
            Debug.Log("remove");
        }

        if (isCatBefriended)
        {
            befriendedCat.gameObject.SetActive(false);
            Values.befriended_cats.Add(befriendedCat.gameObject);
            GameObject.DontDestroyOnLoad(befriendedCat);
            if (!(Values.collected_cat_types.Contains(befriendedCat.GetCatType())))
            {
                Values.collected_cat_types.Add(befriendedCat.GetCatType());
                //GameObject tooltipObj = GameObject.Instantiate(catTooltipUI, canvas.transform);
                //Text tooltipTxt = tooltipObj.transform.GetChild(0).gameObject.GetComponent<Text>();
                //Button tooltipCloseBtn = tooltipObj.transform.GetChild(1).gameObject.GetComponent<Button>();
                //tooltipCloseBtn.onClick.AddListener(delegate { GameObject.Destroy(tooltipObj); });
                //tooltipTxt.text = befriendedCat.GetCatTooltip();

                PopupGenerator.Instance?.GenerateCloseablePopup(
                    befriendedCat.GetCatTooltip()
                    );
            }

            befriendedCat.ui.ShowAffinity(false);
            befriendSuccess.SetActive(true);
        }

        else
        {
            Destroy(befriendedCat.gameObject);
            befriendSuccess.SetActive(false);

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
        EventManager.OnCatBefriend -= RemoveCat;
        Destroy(currentCat);
    }

    public void GoToMenu()
    {
        LoadScene.LoadSectorUnlockingScene();
    }
}
