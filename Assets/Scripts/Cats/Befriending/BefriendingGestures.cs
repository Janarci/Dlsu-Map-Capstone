using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class BefriendingGestures : MonoBehaviour
{
    [SerializeField] Cat cat;
    enum BefriendingMode
    {
        None,
        Petting,
        Feeding,
        Playing,
        Cleaning
    }

    float currentProgress = 0f;
    float pettingProgress = 0.0f;
    float feedingProgress = 0.0f;
    float playingProgress = 0.0f;
    float cleaningProgress = 0.0f;

    float maxProgress = 100.0f;

    public Image progressBar;
    BefriendingMode currentMode;

    [Header("Befriending Objects")]
    [SerializeField] GameObject foodBowl;
    [SerializeField] GameObject foodPack;
    [SerializeField] GameObject pettingHand;
    [SerializeField] GameObject playinggObj;
    [SerializeField] GameObject cleaningObj;
    [SerializeField] GameObject playingYarn;
    [SerializeField] GameObject dirt;
    [SerializeField] GameObject showerHead;

    bool isToyInAir = false;
    // Start is called before the first frame update

    void Start()
    {
        cat = DataPersistenceManager.instance.gameData.approached_cat.GetComponent<Cat>();
    }

    // Update is called once per frame
    void Update()
    {

        //pettingProgress += 0.25f;
        //feedingProgress += 1.1f;
        //playingProgress += 2.5f;
        //cleaningProgress += 0.8f;

        if (Input.touchCount > 0)
        {
            RegisterGesture();
        }

        RegisterGesture();
        switch(currentMode)
        {
            case BefriendingMode.None:
                currentProgress = 0f;
                break;
            case BefriendingMode.Petting:
                currentProgress = pettingProgress;
                if (currentProgress >= maxProgress)
                {
                    cat.AttempBefriendCat(CatInteraction.Type.pet);
                    pettingProgress = 0.0f;
                }
                break;
            case BefriendingMode.Feeding:
                currentProgress = feedingProgress;
                if (currentProgress >= maxProgress)
                {
                    cat.AttempBefriendCat(CatInteraction.Type.feed);
                    feedingProgress = 0.0f;
                }
                break;
            case BefriendingMode.Playing:
                currentProgress = playingProgress;
                if (currentProgress >= maxProgress)
                {
                    cat.AttempBefriendCat(CatInteraction.Type.play);
                    playingProgress = 0.0f;
                }
                break;
            case BefriendingMode.Cleaning:
                currentProgress = cleaningProgress;
                if (currentProgress >= maxProgress)
                {
                    cat.AttempBefriendCat(CatInteraction.Type.clean);
                    cleaningProgress = 0.0f;
                }
                break;
                
        }

        if(currentProgress >= maxProgress)
        {
            ResetProgressMode();
            ResetBefriendingObjectsPosition();
            currentProgress = 0.0f;
        }
        progressBar.fillAmount = currentProgress / maxProgress;
    }

    public void DisplayPettingProgress()
    {
        currentMode = BefriendingMode.Petting;
        if (progressBar)
        {
            progressBar.color = Color.yellow;
        }

        pettingHand.SetActive(true);
        foodBowl.SetActive(false);
        foodPack.SetActive(false);
        playingYarn.SetActive(false);
        showerHead.SetActive(false);
        dirt.SetActive(false);
    }
    public void DisplayFeedingProgress()
    {
        currentMode = BefriendingMode.Feeding;

        if (progressBar)
        {
            progressBar.color = Color.green;
        }

        pettingHand.SetActive(false);
        foodBowl.SetActive(true);
        foodPack.SetActive(true);
        playingYarn.SetActive(false);
        showerHead.SetActive(false);
        dirt.SetActive(false);
    }
    public void DisplayPlayingProgress()
    {
        currentMode = BefriendingMode.Playing;

        if (progressBar)
        {
            progressBar.color = Color.red;
        }

        pettingHand.SetActive(false);
        foodBowl.SetActive(false);
        foodPack.SetActive(false);
        playingYarn.SetActive(true);
        showerHead.SetActive(false);
        dirt.SetActive(false);
    }
    public void DisplayCleaningProgress()
    {
        currentMode = BefriendingMode.Cleaning;

        if (progressBar)
        {
            progressBar.color = Color.blue;
        }

        pettingHand.SetActive(false);
        foodBowl.SetActive(false);
        foodPack.SetActive(false);
        playingYarn.SetActive(false);
        showerHead.SetActive(true);
        dirt.SetActive(true);
    }

    public void ResetProgressMode()
    {
        currentMode = BefriendingMode.None;

        if (progressBar)
        {
            progressBar.color = Color.white;
        }

        pettingHand.SetActive(false);
        foodBowl.SetActive(false);
        foodPack.SetActive(false);
        playingYarn.SetActive(false);
        showerHead.SetActive(false);
        dirt.SetActive(false);

        for(int i = 0; i < dirt.transform.childCount; i++)
        {
            dirt.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ResetBefriendingObjectsPosition()
    {
        BefriendingObjects[] boList = GameObject.FindObjectsOfType<BefriendingObjects>(true);
        foreach(BefriendingObjects bo in boList)
        {
            bo.UnselectObject();
        }
    }

    private void RegisterGesture()
    {
        //Touch t = Input.GetTouch(0);

        //Vector2 initialPos = Vector2.zero;
        //Vector2 touchCurrentPos = t.position;
        //Vector2 touchPrevPos = t.position - t.deltaPosition;
        //Vector2 touchdeltaPos = t.deltaPosition;

        //if(t.phase == TouchPhase.Began)
        //{
        //    //initialPos = t.position;
        //}

        switch (currentMode)
        {
            case BefriendingMode.None:
                {
                    
                }
                break;

            case BefriendingMode.Petting:
                {
                    if (pettingHand.GetComponent<BefriendingObjects>().isSelected)
                    {
                        Vector2 localPos;
                        Vector2 pettingHandPos1 = pettingHand.transform.position;

                        if (Input.touchCount > 0)
                        {
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0), null, out localPos);
                            pettingHand.transform.localPosition = localPos;

                            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                                pettingHand.GetComponent<BefriendingObjects>().UnselectObject();
                        }

                        else if (Input.mousePresent != false)
                        {
                            Debug.Log("mouse");
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), null, out localPos);
                            pettingHand.transform.localPosition = localPos;

                            if (Input.GetMouseButtonUp(0))
                            {
                                Debug.Log("Restting petting hand");
                                pettingHand.GetComponent<BefriendingObjects>().UnselectObject();

                            }

                        }

                        else
                            localPos = Vector3.zero;

                        Vector2 pettingHandPos2 = pettingHand.transform.position;
                        Bounds b1 = new Bounds(pettingHand.transform.position, pettingHand.GetComponent<RectTransform>().rect.size);
                        Bounds b2 = cat.gameObject.GetComponent<Collider>().bounds;


                        Bounds b3 = new Bounds(Vector3.zero, Vector3.zero);
                        b3.center = Camera.main.WorldToScreenPoint(b2.center);
                        b3.min = Camera.main.WorldToScreenPoint(b2.min);
                        b3.max = Camera.main.WorldToScreenPoint(b2.max);
                        b3.center = new Vector3(b3.center.x, b3.center.y, b1.center.z);

                        DrawBounds(b1);

                        //DrawBounds(b2);

                        DrawBounds(b3);

                        Debug.Log(b1.center + " || " + b3.center);
                        if (b1.Intersects(b3))
                        {
                            //feedingProgress += 30.0f * Time.deltaTime;
                            Debug.Log("Petting");
                            pettingProgress += Vector3.Distance(pettingHandPos1, pettingHandPos2) * 0.15f;
                        }

                    
                    //if(minP || maxP)
                    //{
                    //    Debug.Log("Petting");
                    //}
                    
                    //b2.center = new Vector3 (b2.center.x, b2.center.y, 0.0f);
                    
                    }
                }
                break;

            case BefriendingMode.Feeding:
                {
                    //convert touch position to localposition reletive to canvas

                    FoodPack foodPackComp = foodPack.GetComponent<FoodPack>();
                    if(foodPackComp.isSelected)
                    {
                        Vector2 localPos;
                        if (Input.touchCount > 0)
                        {
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0), null, out localPos);
                            foodPack.transform.localPosition = localPos;

                            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                                foodPackComp.UnselectObject();
                        }

                        else if (Input.mousePresent != false)
                        {
                            Debug.Log("mouse");
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), null, out localPos);
                            foodPack.transform.localPosition = localPos;

                            if (Input.GetMouseButtonUp(0))
                            {
                                Debug.Log("Restting food");
                                foodPackComp.UnselectObject();

                            }

                        }

                        else
                            localPos = Vector3.zero;
                        //set position on canvas


                        //Debug.Log(FoodBowl.transform.position + "| | " + FoodPack.transform.position);
                        if (foodPackComp.isInBowl())
                        {
                            Debug.Log("feeding");
                            feedingProgress += 30.0f * Time.deltaTime;
                        }

                        else
                        {
                            feedingProgress = 0.0f;
                        }
                    }
                    
                }
                break;

            case BefriendingMode.Playing:
                {
                    PlayingYarn playingYarnComp = playingYarn.GetComponent<PlayingYarn>();
                    if (playingYarn.GetComponent<BefriendingObjects>().isSelected)
                    {
                        Vector2 playingYarnPos1 = playingYarn.transform.position;
                        Vector2 localPos;
                        if (Input.touchCount > 0)
                        {
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0), null, out localPos);
                            playingYarn.transform.localPosition = localPos;

                            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                            {
                                if(playingYarnComp.CanLaunch())
                                {
                                    playingYarnComp.Throw();
                                }

                                else
                                {
                                    playingYarnComp.UnselectObject();
                                }
                            }
                        }

                        else if (Input.mousePresent != false)
                        {
                            //Debug.Log("mouse");
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), null, out localPos);
                            playingYarn.transform.localPosition = localPos;

                            if (Input.GetMouseButtonUp(0))
                            {
                                if (playingYarnComp.CanLaunch())
                                {
                                    playingYarnComp.Throw();
                                }

                                else
                                {
                                    playingYarnComp.UnselectObject();
                                }
                            }

                        }

                        else
                            localPos = Vector3.zero;
                        //set position on canvas


                        Vector2 playingYarnPos2 = playingYarn.transform.position;

                        //float distance = Vector2.Distance(playingYarnPos1, playingYarnPos2) * Time.deltaTime * 20;
                        //if (distance > 0.0f)
                        //    Debug.Log(distance);

                        
                    }

                    else if(playingYarnComp.IsInAir())
                    {
                        Bounds b1 = new Bounds(playingYarn.transform.position, pettingHand.GetComponent<RectTransform>().rect.size);
                        Bounds b2 = cat.gameObject.GetComponent<Collider>().bounds;
                        Bounds b3 = new Bounds(Vector3.zero, Vector3.zero);
                        b3.center = Camera.main.WorldToScreenPoint(b2.center);
                        b3.min = Camera.main.WorldToScreenPoint(b2.min);
                        b3.max = Camera.main.WorldToScreenPoint(b2.max);
                        b3.center = new Vector3(b3.center.x, b3.center.y, b1.center.z);

                        if (b1.Intersects(b3))
                        {
                            playingProgress += maxProgress / 2;
                            playingYarnComp.UnselectObject();
                        }
                    }

                    //else if(playingYarn.transform.position != playingYarn.GetComponent<BefriendingObjects>().initialPos)
                    //{
                    //    if(isToyInAir)
                    //    {

                    //    }
                    //}

                    //if (isToyInAir)
                    //{
                    //    //Debug.Log(FoodBowl.transform.position + "| | " + FoodPack.transform.position);
                    //    Bounds b1 = new Bounds(foodBowl.transform.position, foodBowl.GetComponent<RectTransform>().rect.size);
                    //    Bounds b2 = new Bounds(foodPack.transform.position, foodPack.GetComponent<RectTransform>().rect.size);
                    //    DrawBounds(b1);
                    //    DrawBounds(b2);

                    //    if (b1.Intersects(b2))
                    //    {
                    //        feedingProgress += 30.0f * Time.deltaTime;
                    //        Debug.Log("feeding");
                    //    }

                    //    else
                    //    {
                    //        feedingProgress = 0.0f;
                    //    }
                    //}
                }
                break;

            case BefriendingMode.Cleaning:
                {
                    if (showerHead.GetComponent<BefriendingObjects>().isSelected)
                    {
                        Vector2 localPos;
                        if (Input.touchCount > 0)
                        {
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0), null, out localPos);
                            showerHead.transform.localPosition = localPos;

                            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                                showerHead.GetComponent<BefriendingObjects>().UnselectObject();
                        }

                        else if (Input.mousePresent != false)
                        {
                            Debug.Log("mouse");
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("InfoCanvas").GetComponent<RectTransform>(), new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), null, out localPos);
                            showerHead.transform.localPosition = localPos;
                            if (Input.GetMouseButtonUp(0))
                            {
                                Debug.Log("Restting showerhead");
                                showerHead.GetComponent<BefriendingObjects>().UnselectObject();

                            }
                        }

                        else
                            localPos = Vector3.zero;
                        //set position on canvas

                        //Debug.Log(FoodBowl.transform.position + "| | " + FoodPack.transform.position);
                        Bounds b1 = new Bounds(showerHead.transform.position, foodBowl.GetComponent<RectTransform>().rect.size);
                        DrawBounds(b1);

                        int cleanedUpDirt = 0;
                        for (int i = 0; i < dirt.transform.childCount; i++)
                        {
                            if(dirt.transform.GetChild(i).gameObject.activeInHierarchy == false)
                            {
                                cleanedUpDirt++;
                            }

                            else if (b1.Intersects(dirt.transform.GetChild(i).GetComponent<Collider2D>().bounds))
                            {
                                dirt.transform.GetChild(i).gameObject.SetActive(false);
                                Debug.Log("Cleaning");
                                cleanedUpDirt++;
                            }

                            cleaningProgress = maxProgress * (cleanedUpDirt/(float)dirt.transform.childCount);
                        }

                        

                        

                    }
                }
                break;
        }
    }

    void DrawBounds(Bounds b, float delay = 0)
    {
        // bottom
        var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

        Debug.DrawLine(p1, p2, Color.blue, delay);
        Debug.DrawLine(p2, p3, Color.red, delay);
        Debug.DrawLine(p3, p4, Color.yellow, delay);
        Debug.DrawLine(p4, p1, Color.magenta, delay);

        // top
        var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        Debug.DrawLine(p5, p6, Color.blue, delay);
        Debug.DrawLine(p6, p7, Color.red, delay);
        Debug.DrawLine(p7, p8, Color.yellow, delay);
        Debug.DrawLine(p8, p5, Color.magenta, delay);

        // sides
        Debug.DrawLine(p1, p5, Color.white, delay);
        Debug.DrawLine(p2, p6, Color.gray, delay);
        Debug.DrawLine(p3, p7, Color.green, delay);
        Debug.DrawLine(p4, p8, Color.cyan, delay);
    }

}
