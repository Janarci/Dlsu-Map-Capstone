using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGenerator : MonoBehaviour
{
    private Queue<GameObject> popupQueue;
    [SerializeField] private GameObject infoUICloseable;
    [SerializeField] private GameObject infoUINextable;
    [SerializeField] private GameObject infoUIConfirmable;
    [SerializeField] private GameObject infoUIAchievement;

    [SerializeField] private GameObject infoUIObj;
    [SerializeField] public static PopupGenerator Instance;

    public bool dontOverwrite = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            popupQueue = new Queue<GameObject>();
        }
            

        else
            Destroy(this.gameObject);

    }

    private void Update()
    {
        if(popupQueue.Count != 0 && infoUIObj == null)
        {
            GameObject newPopup = popupQueue.Dequeue();
            infoUIObj = newPopup;
            infoUIObj.SetActive(true);
            Debug.Log("Displaying Popup");
        }

    }

    public void GenerateCloseablePopup(string popupMessage)
    {
        GameObject mainCanvas = GameObject.Find("InfoCanvas");

        if (mainCanvas)
        {
            GameObject newInfoUIObj = Instantiate(infoUICloseable, mainCanvas.transform);
            newInfoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
            newInfoUIObj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Destroy(newInfoUIObj); infoUIObj = null; });
            dontOverwrite = false;
            newInfoUIObj.SetActive(false);


            popupQueue.Enqueue(newInfoUIObj);
        }
        Debug.Log(popupQueue.Count);

        //else
        //{
        //    if(!dontOverwrite)
        //    {
        //        infoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
        //        dontOverwrite = false;
        //    }

        //}
    }

    public Button AssignCloseableFunction(string message)
    {

        Destroy(infoUIObj);
        GameObject mainCanvas = GameObject.Find("InfoCanvas");


        infoUIObj = Instantiate(infoUINextable, mainCanvas.transform);
        infoUIObj.transform.GetChild(0).GetComponent<Text>().text = message;
        Debug.Log(popupQueue.Count);

        return transform.GetChild(1).GetComponent<Button>();
        
    }

    public ConfirmablePopupBehaviour GenerateConfirmablePopup(string popupMessage)
    {
        GameObject mainCanvas = GameObject.Find("InfoCanvas");

        if (mainCanvas)
        {
            GameObject newInfoUIObj = Instantiate(infoUIConfirmable, mainCanvas.transform);
            newInfoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
            newInfoUIObj.SetActive(false);
            dontOverwrite = false;

            popupQueue.Enqueue(newInfoUIObj);
            ConfirmablePopupBehaviour cpb = newInfoUIObj.GetComponent<ConfirmablePopupBehaviour>();
            newInfoUIObj.SetActive(false);
            cpb.onCancel += DestroyPopup;
            cpb.onConfirm += DestroyPopup;

            Debug.Log(popupQueue.Count);

            return cpb;
        }

        else
        {
            return null;
        }

        //else
        //{
        //    if (!dontOverwrite)
        //    {
        //        infoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
        //        dontOverwrite = false;
        //        return infoUIObj.GetComponent<ConfirmablePopupBehaviour>();
        //    }
        //    return null;
        //}

        void DestroyPopup()
        {
            Destroy(infoUIObj);
            infoUIObj = null;
        }
    }

    public void GenerateAchievementPopup(Achievement.AchievementCode _type)
    {

        GameObject mainCanvas = GameObject.Find("InfoCanvas");


        GameObject newInfoUIObj = Instantiate(infoUIAchievement, mainCanvas.transform);
        newInfoUIObj.GetComponent<AchievementPopupBehaviour>().SetInfo(_type);
        newInfoUIObj.SetActive(false);

        popupQueue.Enqueue(newInfoUIObj);
        Debug.Log(popupQueue.Count);
    }
}
