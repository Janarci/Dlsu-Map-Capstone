using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HQUI : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject materialTxtTemplate;
    [SerializeField] GameObject evolutionButtonTemplate;
    [SerializeField] GameObject availableEvolutionsObj;
    [SerializeField] GameObject availableMaterialsObj;
    [SerializeField] GameObject availableEvolutionsContent;
    [SerializeField] GameObject availableMaterialsContent;

    public GameObject catTooltipUI = null;



    private Cat focusedCat;
    private float zoomTvalue = 0;
    private Vector3 cameraInitialPos;
    private bool isZoomingOnCat;

    // Start is called before the first frame update
    void Start()
    {
        cameraInitialPos = camera.transform.position;
        EventManager.OnCatClick += OnCatSelect;
    }

    // Update is called once per frame
    void Update()
    {
        if(isZoomingOnCat)
        {
            CameraFocusOnCat(focusedCat.gameObject);
        }

        if(focusedCat != null)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                UnselectCat();
            }
        }
    }

    private void OnCatSelect(Cat selectedCat)
    {
        focusedCat = selectedCat;
        isZoomingOnCat = true;
        focusedCat.StopRoaming();
        UpdateCatAvailableEvolutions();
        UpdateCatMaterialsList();
        
    }

    private void UnselectCat()
    {
        focusedCat.StartRoam();
        focusedCat = null;
        ResetCamera();
        UpdateCatAvailableEvolutions();
        UpdateCatMaterialsList();
    }

    public void GiveCatBook()
    {
        focusedCat?.GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.homework);
    }

    public void GiveCatHomework()
    {
        focusedCat?.GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.homework);
        UpdateCatAvailableEvolutions();
        UpdateCatMaterialsList();
        //Debug.Log(focusedCat?.GetMaterialCount(CatEvolutionitem.cat_evolution_item_type.homework));
        //Debug.Log(focusedCat?.GetAvailableEvolutions().Contains(cat_type.student_cat));
    }

    public void GiveCatPaycheck()
    {
        focusedCat?.GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.paycheck);
        UpdateCatAvailableEvolutions();
        UpdateCatMaterialsList();
    }

    public void UpdateCatMaterialsList()
    {
        for (int i = 0; i < availableMaterialsContent.transform.childCount; i++)
        {
            Destroy(availableMaterialsContent.transform.GetChild(i).gameObject);
        }

        if (focusedCat)
        {

            for (int i = 0; i < Enum.GetNames(typeof(CatEvolutionItem.cat_evolution_item_type)).Count(); i++)
            {
                GameObject newTextObj = Instantiate(materialTxtTemplate, availableMaterialsContent.transform);
                Text textComp = newTextObj.GetComponent<Text>();
                CatEvolutionItem.cat_evolution_item_type type = (CatEvolutionItem.cat_evolution_item_type)i;
                textComp.GetComponent<Text>().text = type.ToString() + ": " + focusedCat.GetMaterialCount(type).ToString();
            }
        }
        
    }
    public void UpdateCatAvailableEvolutions()
    {
        for (int i = 0; i < availableEvolutionsContent.transform.childCount; i++)
        {
            Destroy(availableEvolutionsContent.transform.GetChild(i).gameObject);
        }

        if (focusedCat)
        {
            foreach (cat_type type in focusedCat.GetAvailableEvolutions())
            {
                GameObject newButtonObj = Instantiate(evolutionButtonTemplate, availableEvolutionsContent.transform);
                Button buttonComp = newButtonObj.GetComponent<Button>();
                GameObject textComp = newButtonObj.transform.GetChild(0).gameObject;
                textComp.GetComponent<Text>().text = type.ToString();
                buttonComp.onClick.AddListener(delegate { focusedCat = focusedCat.EvolveTo(type); HideCatAvailableEvolutions(); DisplayCatToolTip(); });

            }
        }
        
        
        
    }
    public void HideCatAvailableEvolutions()
    {

        List<GameObject> evolutionsButtonList = new List<GameObject>();
        Debug.Log(availableEvolutionsContent.transform.childCount);
        for (int i = 0; i < availableEvolutionsContent.transform.childCount; i++)
        {
            evolutionsButtonList.Add(availableEvolutionsContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject buttonObj in evolutionsButtonList)
        {
            Destroy(buttonObj);
        }

        evolutionsButtonList.Clear();
        availableEvolutionsObj?.SetActive(false);

    }
    private void CameraFocusOnCat(GameObject selectedCat)
    {
        //Debug.Log(Time.deltaTime);
        zoomTvalue += Time.deltaTime * 0.8f;

        Vector3 catFrontPosition = new Vector3(selectedCat.transform.position.x, selectedCat.transform.position.y + 5, selectedCat.transform.position.z - 5);
        Vector3 cameraNewPosition = Vector3.Lerp(camera.transform.position, catFrontPosition, zoomTvalue);
        camera.transform.position = cameraNewPosition;
        
        if (zoomTvalue >= 1.0f)
        {
            zoomTvalue = 0.0f;
            isZoomingOnCat = false;
            focusedCat.transform.LookAt(new Vector3(camera.transform.position.x, focusedCat.transform.position.y, camera.transform.position.z));
        }

    }

    public void DisplayCatToolTip()
    {
        if(catTooltipUI)
        {
            if (!(Values.collected_cat_types.Contains(focusedCat.GetCatType())))
            {
                Values.collected_cat_types.Add(focusedCat.GetCatType());
                GameObject tooltipObj = GameObject.Instantiate(catTooltipUI, canvas.transform);
                Text tooltipTxt = tooltipObj.transform.GetChild(0).gameObject.GetComponent<Text>();
                Button tooltipCloseBtn = tooltipObj.transform.GetChild(1).gameObject.GetComponent<Button>();
                tooltipCloseBtn.onClick.AddListener(delegate { GameObject.Destroy(tooltipObj); });
                tooltipTxt.text = focusedCat.GetCatTooltip();
                Debug.Log(tooltipTxt.text);
            }

        }
    }



    private void ResetCamera()
    {
        camera.transform.position = cameraInitialPos;
        isZoomingOnCat = false;
        


        Debug.Log("Restting camera");
        Debug.Log(camera.transform.position);


    }
    private void OnDestroy()
    {
        EventManager.OnCatClick -= OnCatSelect;
    }
}
