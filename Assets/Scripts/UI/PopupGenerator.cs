using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGenerator : MonoBehaviour
{
    [SerializeField] private GameObject infoUITemplate;
    [SerializeField] private GameObject infoUIObj;
    [SerializeField] public static PopupGenerator Instance;

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

    public void GeneratePopup(string popupMessage)
    {
        if (infoUIObj == null)
        {
            GameObject mainCanvas = GameObject.Find("InfoCanvas");
            

            infoUIObj = Instantiate(infoUITemplate, mainCanvas.transform);
            infoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;
            infoUIObj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoUIObj); });
        }

        else
        {
            infoUIObj.transform.GetChild(0).GetComponent<Text>().text = popupMessage;

        }
    }
}
