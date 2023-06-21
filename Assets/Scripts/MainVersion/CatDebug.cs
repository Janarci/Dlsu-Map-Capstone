using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using static CatTrait;

public class CatDebug : MonoBehaviour
{
    public GameObject canvas;
    public GameObject currentCat = null;
    public GameObject catTooltipUI = null;
    public GameObject befriendSuccess;
    public GameObject befriendFail;

    public List<GameObject> catObjList;
    public CatUI ui;
    public GameObject cam;

    public Vector3 CameraPos;
    Vector3 InitialPos;
    // Start is called before the first frame update
    void Start()
    {
        TutorialManager.instance.UnlockTutorial(TutorialManager.Type.cat_befriend);
        
        if (CameraManager.Instance)
        {
            CameraManager.Instance.gameObject.transform.position = Vector3.zero;
            CameraManager.Instance.EnableARCamera();
            InitialPos = CameraManager.Instance.ARCamera.transform.localPosition;
            CameraManager.Instance.ARCamera.transform.localPosition = CameraPos;
            Destroy(cam);
        }

        else
        {
            cam.SetActive(true);
        }
        EventManager.OnCatBefriend += RemoveCat;
        currentCat = DataPersistenceManager.instance.gameData.approached_cat;
        currentCat.transform.position = new Vector3(0, -5, 2);
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
            CatTrait.Favor favor = cat.AttempBefriendCat(CatInteraction.Type.pet);

            if(cat.GetComponent<CatAnimsTest>() != null)
            {
                CatAnimsTest c = cat.GetComponent<CatAnimsTest>();
                switch(favor)
                {
                    case CatTrait.Favor.bad:
                        c.cat_react_bad();
                        break;

                    case CatTrait.Favor.good:
                        c.cat_react_good();
                        break;

                    case CatTrait.Favor.great:
                        c.cat_react_great();
                        break;
                }
            }
        }
    }

    public void Feed()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            CatTrait.Favor favor = cat.AttempBefriendCat(CatInteraction.Type.feed);

            if (cat.GetComponent<CatAnimsTest>() != null)
            {
                CatAnimsTest c = cat.GetComponent<CatAnimsTest>();
                switch (favor)
                {
                    case CatTrait.Favor.bad:
                        c.cat_react_bad();
                        break;

                    case CatTrait.Favor.good:
                        c.cat_react_good();
                        break;

                    case CatTrait.Favor.great:
                        c.cat_react_great();
                        break;
                }
            }
        }
    }

    public void Play()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            CatTrait.Favor favor = cat.AttempBefriendCat(CatInteraction.Type.play);

            if (cat.GetComponent<CatAnimsTest>() != null)
            {
                CatAnimsTest c = cat.GetComponent<CatAnimsTest>();
                switch (favor)
                {
                    case CatTrait.Favor.bad:
                        c.cat_react_bad();
                        break;

                    case CatTrait.Favor.good:
                        c.cat_react_good();
                        break;

                    case CatTrait.Favor.great:
                        c.cat_react_great();
                        break;
                }
            }
        }
    }

    public void Clean()
    {
        if (currentCat != null)
        {
            Cat cat = currentCat.GetComponent<Cat>();
            CatTrait.Favor favor = cat.AttempBefriendCat(CatInteraction.Type.clean);

            if (cat.GetComponent<CatAnimsTest>() != null)
            {
                CatAnimsTest c = cat.GetComponent<CatAnimsTest>();
                switch (favor)
                {
                    case CatTrait.Favor.bad:
                        c.cat_react_bad();
                        break;

                    case CatTrait.Favor.good:
                        c.cat_react_good();
                        break;

                    case CatTrait.Favor.great:
                        c.cat_react_great();
                        break;
                }
            }
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
            CatsManager.instance.befriended_cats.Add(befriendedCat.gameObject);
            GameObject.DontDestroyOnLoad(befriendedCat);
            if (!(CatsManager.instance.collected_cat_types.Contains(befriendedCat.GetCatType())))
            {
                CatsManager.instance.UnlockCatType(befriendedCat.GetCatType());
                //GameObject tooltipObj = GameObject.Instantiate(catTooltipUI, canvas.transform);
                //Text tooltipTxt = tooltipObj.transform.GetChild(0).gameObject.GetComponent<Text>();
                //Button tooltipCloseBtn = tooltipObj.transform.GetChild(1).gameObject.GetComponent<Button>();
                //tooltipCloseBtn.onClick.AddListener(delegate { GameObject.Destroy(tooltipObj); });
                //tooltipTxt.text = befriendedCat.GetCatTooltip();

                PopupGenerator.Instance?.GenerateCloseablePopup(
                    "You have befriended a " + CatDatabase.Instance?.GetCatData(befriendedCat.GetCatType()).catTypeLabel +
                    "\n" +
                    befriendedCat.GetCatTooltip()
                    );
            }

            befriendedCat.ui.ShowAffinity(false);
            befriendSuccess.SetActive(true);

            
                

            if(CatsManager.instance.befriended_cats.Count <= 4)
            {
                CatsManager.instance.selected_cats[CatsManager.instance.befriended_cats.Count - 1] = befriendedCat.gameObject;
            }
        }

        else
        {
            Destroy(befriendedCat.gameObject);
            befriendFail.SetActive(true);

        }

    }

    public void GoToHQ()
    {
        foreach (GameObject go in CatsManager.instance.befriended_cats)
        {
            
        }
        LoadScene.LoadHQScene();
    }

    public void OnDestroy()
    {
        CameraManager.Instance.ARCamera.transform.localPosition = InitialPos;
        EventManager.OnCatBefriend -= RemoveCat;
        Destroy(currentCat);
    }

    public void GoToMenu()
    {
        LoadScene.LoadSectorUnlockingScene();
    }
}
