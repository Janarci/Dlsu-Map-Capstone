using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HQBehaviour : MonoBehaviour
{
    public List<GameObject> catList;
    public static HQBehaviour Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnCatEvolve += OnCatEvolve;
        catList = new List<GameObject>();

        int j = 0;
        int i = 0;

        if(Values.befriended_cats != null)
        foreach (GameObject go in Values.befriended_cats)
        {
            Debug.Log(i);
            go.transform.SetPositionAndRotation(new Vector3(-12 + (i * 3), 0, -20 + (j * 3)), Quaternion.Euler(new Vector3(0, 180, 0)));
            go.SetActive(true);
            if(go.GetComponent<Animator>().isActiveAndEnabled)
                go.GetComponent<Cat>().StartRoam();
            i++;
            if(i >= 9)
            {
                j++;
                i = 0;
            }

            catList.Add(go);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void GiveCatHomework(Cat cat)
    {
        if (catList.Contains(cat.gameObject))
        {
            cat.GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.homework);
            //catList[cat.gameObject].gameObject.GetComponent<Cat>().GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.paycheck);

        }
    }
    public void GiveCatPaycheck(Cat cat)
    {
        if (catList.Contains(cat.gameObject))
        {
            cat.GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.paycheck);
            //catList[cat.gameObject].gameObject.GetComponent<Cat>().GiveEvolutionMaterial(CatEvolutionItem.cat_evolution_item_type.paycheck);

        }
    }
    public void OnCatEvolve(Cat oldCat, Cat newCat, CatType.Type type)
    {
        
    }

    public void GoToCatCatching()
    {
        LoadScene.LoadSectorUnlockingScene();
    }

    public void OnDestroy()
    {
        //foreach(KeyValuePair<GameObject, GameObject> pair in catList)
        //{
        //    Destroy(pair.Value.GetComponent<Cat>());
        //    bool copied = ComponentUtility.CopyComponent(pair.Key.GetComponent<Cat>());
        //    bool pasted = ComponentUtility.PasteComponentAsNew(pair.Value);
        //    pair.Value.GetComponent<Cat>().InheritCatAttributes(pair.Key.GetComponent<Cat>());
        //    Debug.Log(copied + ", " + pasted);
        //}

        foreach(GameObject cat in catList)
        {
            cat.GetComponent<Cat>().StopRoaming();
            cat.GetComponent<Cat>().ui.ShowAll(false);
            cat.GetComponent<Cat>().ui.ShowInteractUI(false);
            cat.GetComponent<Cat>().ui.ShowEvolve(false);

            cat.SetActive(false);
        }
        EventManager.OnCatEvolve -= OnCatEvolve;
    }
}
