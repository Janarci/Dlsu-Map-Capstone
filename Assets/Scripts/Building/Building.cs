using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public enum Type
    {
        br_andrew_gonzalez_hall,
        br_bloemen_hall,
        br_connon_hall,
        br_william_hall,
        enrique_razon_sports_center,
        faculty_center,
        gokongwei_hall,
        henry_sy_sr_hall,
        sci_tech_research_center,
        st_joseph_hall,
        st_lasalle_hall,
        st_miguel_hall,
        velasco_hall,
        yuchengco_hall
    }

    [SerializeField] private List<Parts> buildingParts;
    [SerializeField] private GameObject InfoUITemplate;
    [SerializeField] public string buildingName;

    [TextAreaAttribute]
    [SerializeField] public string buildingAbout;
    [SerializeField] public Sprite buildingPic;
    [SerializeField] private GameObject buttonPopup;

    [SerializeField] public Type type;

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

        MakeTransparent();
    }

    public void MakeTransparent()
    {
        if(buildingParts != null)
        {
            if(!(buildingParts.Count == 0))
            {
                foreach (Parts part in buildingParts)
                {
                    List<Material> mats = GetMaterialsList(part);

                    foreach (Material mat in mats)
                    {
                        Debug.Log("setting transparent" + mat.name);
                        Color transpColor = new Color(0.75f, 0.75f, 0.75f);
                        transpColor.a = 0.275f;
                        mat.SetColor("_Color", transpColor);
                        mat.SetOverrideTag("RenderType", "Transparent");
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 0);
                        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    }
                }
            }
        }
    }

    public void MakeOpaque()
    {
        if (buildingParts != null)
        {
            if (!(buildingParts.Count == 0))
            {
                foreach (Parts part in buildingParts)
                {
                    List<Material> mats = GetMaterialsList(part);

                    foreach (Material mat in mats)
                    {
                        Debug.Log("setting transparent" + mat.name);
                        Color transpColor = new Color(1.0f, 1.0f, 1.0f);
                        transpColor.a = 1.0f;
                        mat.SetColor("_Color", transpColor);
                        mat.SetOverrideTag("RenderType", "Opaque");
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                        mat.SetInt("_ZWrite", 1);
                        mat.renderQueue = -1;

                    }
                }
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
        buttonPopup.SetActive(true);
        //DisplayBuildingInfo();
    }

    public void DisplayBuildingInfo()
    {
        PopupGenerator.Instance?.GenerateCloseablePopup(
            "Name: " + buildingName + "\n" + "About: " + buildingAbout
            );

    }

    public List<Material> GetMaterialsList(Parts p)
    {
        GameObject partsObject = p.gameObject;
        List<Renderer> renderers = new List<Renderer>();
        List<Material> partMats = new List<Material>();
        List<Material> partMatsTemp = new List<Material>();
        p.gameObject.GetComponentsInChildren<Renderer>(renderers);

        foreach(Renderer r in renderers)
        {
            partMatsTemp = new List<Material>();
            r.GetMaterials(partMatsTemp);
            partMats.AddRange(partMatsTemp);
        }


        return partMats;
    }

    public Type SetData()
    {
        return type;
    }
}
