using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private List<Parts> buildingParts;
    [SerializeField] private GameObject InfoUITemplate;

    [SerializeField] private string buildingName;

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 1; i < gameObject.transform.childCount; i++)
        //{
        //    Parts childPartsComp = gameObject.transform.GetChild(i).AddComponent<Parts>();
        //    childPartsComp.SetBuilding(this);
        //}

        Collider[] childrenCollider = gameObject.GetComponentsInChildren<Collider>();

        foreach(Collider c in childrenCollider)
        {
            if(c.gameObject != transform.GetChild(0).gameObject)
            {
                Parts childPartsComp = c.gameObject.AddComponent<Parts>();
                childPartsComp.SetBuilding(this);
                buildingParts.Add(childPartsComp);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPartsClick()
    {
        Debug.Log(buildingName + " was clicked");
        DisplayBuildingInfo();
    }

    public void DisplayBuildingInfo()
    {
        GameObject mainCanvas = GameObject.Find("InfoCanvas");
        GameObject infoUI = Instantiate(InfoUITemplate, mainCanvas.transform);
        infoUI.transform.GetChild(0).GetComponent<Text>().text = "Name: " +  buildingName + "\n" + "About:";
        infoUI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoUI); });

    }
}
