using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using System.Linq;

public class UpdateCats : MonoBehaviour
{
    public static UpdateCats Instance;
    bool isReplacing = false;
    private GameObject replacementCat = null;
    private int replacementIndex = -1;
    [SerializeField] GameObject HQCatsPanelTemplate;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

        else
            Destroy(gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CatsManager.instance.befriended_cats.Count > 0)
        {
            
            Timers.Instance?.StartCatShuffleTimer();
            Timers.Instance?.StartReplaceCatTimer();
            
            int availableCats = 0;

            foreach (GameObject cat in CatsManager.instance.befriended_cats)
            {
                if (Array.Exists<GameObject>(CatsManager.instance.selected_cats, selectedCat => selectedCat == cat))
                {
                    Cat catComp = cat.GetComponent<Cat>();

                    catComp.Ail();
                    if ((catComp.GetSadnessPercentage() + catComp.GetHungerPercentage() + catComp.GetBoredomPercentage() + catComp.GetDirtPercentage()) > 2.0f)
                    {
                        Debug.LogError("Cat has reached ail limit");

                        for (int i = 0; i < 4; i++)
                        {
                            if (CatsManager.instance.selected_cats[i] == cat)
                            {
                                Debug.LogError("Removing selected cat index " + i);
                                CatsManager.instance.selected_cats[i] = null;

                                PopupGenerator.Instance?.GenerateCloseablePopup(CatDatabase.Instance.GetCatData(catComp.GetCatType()).catTypeLabel + " has left the HQ");
                                break;
                            }
                        }

                    }
                }

                else
                {
                    //Debug.Log("RECOVERING CAT");
                    Cat catComp = cat.GetComponent<Cat>();
                    catComp.Recover();
                    if ((catComp.GetSadnessPercentage() + catComp.GetHungerPercentage() + catComp.GetBoredomPercentage() + catComp.GetDirtPercentage()) < 0.75f)
                    {
                        availableCats++;
                    }
                }

                //CatHQStayRequest();

            }
        }
    }

    public void CatHQStayRequest()
    {
        Debug.Log("Staeting HQ cat stay request");
        if (CatsManager.instance.befriended_cats.Count > 4 && !isReplacing)
        {
            Cat catComp = null;

            Debug.Log("Found available cat slot");
            ConfirmablePopupBehaviour cpb = null;
            int counter = 0;
            do
            {
                GameObject _replacementCat = CatsManager.instance.befriended_cats[UnityEngine.Random.Range(0, CatsManager.instance.befriended_cats.Count)];
                if (!(Array.Exists<GameObject>(CatsManager.instance.selected_cats, _cat => _cat == _replacementCat)))
                {
                    replacementCat = _replacementCat;
                    catComp = replacementCat.GetComponent<Cat>();
                }

                counter++;
            } while (catComp != null && (catComp.GetSadnessPercentage() + catComp.GetHungerPercentage() + catComp.GetBoredomPercentage() + catComp.GetDirtPercentage()) < 0.75f && counter <= 12);


            if (catComp && replacementCat)
            {
                cpb = PopupGenerator.Instance?.GenerateConfirmablePopup(CatDatabase.Instance.GetCatData(catComp.GetCatType()).catTypeLabel + " wants to spend time in the HQ. Do you approve?");

                if (cpb)
                {
                    isReplacing = true;
                    cpb.onConfirm += OnAddCatToHQ;
                    cpb.onCancel += ResetValues;
                }

                else
                {
                    ResetValues();
                }


            }
        }
    }

    public void GenerateItemsPassive()
    {
        foreach (GameObject cat in CatsManager.instance.befriended_cats)
        {
            Cat catComp = cat.GetComponent<Cat>();
            if(catComp.getRelationshipLevel() == 10 && CatDatabase.Instance.GetCatData(catComp.GetCatType()).evolutions.Count == 0)
            {
                bool isCatExistInEvolutionPath = false;
                foreach (CatDatabase.CatData _data in CatDatabase.Instance.data)
                {
                    foreach(CatEvolutionRequirement _req in _data.evolutions)
                    {
                        if (_req.catType == catComp.GetCatType())
                        {
                            foreach(CatEvolutionRequirement.EvolutionRequirement _item in _req.requirement)
                            {
                                Inventory.Instance.AddToInventory(_item.item, _item.amount);
                            }
                            isCatExistInEvolutionPath = true;
                            break;

                        }

                        
                    }
                    if (isCatExistInEvolutionPath)
                        break;
                }
                
            }

            else
            {
                
            }

            //CatHQStayRequest();

        }
    }

    void OnAddCatToHQ()
    {
        if(replacementCat != null)
        {
            Debug.Log("Cat entered HQ");
            //Values.selected_cats[replacementIndex] = replacementCat;
            //EventManager.HQCatReplaced(replacementCat.GetComponent<Cat>(), replacementIndex);
            GameObject HQCatsPanelObj = Instantiate(HQCatsPanelTemplate, GameObject.Find("InfoCanvas").transform);
            for(int i = 0; i < HQCatsPanelObj.transform.childCount; i++)
            {
                HQCatsPanelObj.transform.GetChild(i).gameObject.GetComponent<HQCatsPanelIndex>().replacementCat = replacementCat;
            }
        }

        ResetValues();
    }

    void ResetValues()
    {
        replacementCat = null;
        replacementIndex = -1;
        isReplacing= false;
    }
}
