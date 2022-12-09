using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGenerator : MonoBehaviour
{
    [SerializeField] private GameObject infoUICloseable;
    [SerializeField] private GameObject infoUINextable;

    [SerializeField] private GameObject infoUIObj;
    [SerializeField] public static PopupGenerator Instance;

    public bool dontOverwrite = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
            

        else
            Destroy(this.gameObject);

    }

    public void GenerateCloseablePopup(string popupMessage)
    {
        if (infoUIObj == null)
        {
            GameObject mainCanvas = GameObject.Find("InfoCanvas");
            

            infoUIObj = Instantiate(infoUICloseable, mainCanvas.transform);
            infoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
            infoUIObj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoUIObj); });
            dontOverwrite = false;
        }

        else
        {
            if(!dontOverwrite)
            {
                infoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
                dontOverwrite = false;
            }
                
        }
    }

    public Button AssignCloseableFunction(string message)
    {
        Destroy(infoUIObj);
        GameObject mainCanvas = GameObject.Find("InfoCanvas");


        infoUIObj = Instantiate(infoUINextable, mainCanvas.transform);
        infoUIObj.transform.GetChild(0).GetComponent<Text>().text = message;
        return transform.GetChild(1).GetComponent<Button>();
        
    }
}
