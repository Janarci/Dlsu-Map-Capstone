using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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
        AudioManager.Instance?.Play("BGM HQ", "bgm");
        EventManager.OnCatEvolve += OnCatEvolve;
        catList = new List<GameObject>();

        int j = 0;
        int i = 0;

        //if(Values.befriended_cats != null)
        //foreach (GameObject go in Values.befriended_cats)
        //{
        //    //Debug.Log(i);
        //    go.transform.SetPositionAndRotation(new Vector3(-12 + (i * 3), 0, -20 + (j * 3)), Quaternion.Euler(new Vector3(0, 180, 0)));
        //    go.SetActive(true);
        //    if(go.GetComponent<Animator>().isActiveAndEnabled)
        //        go.GetComponent<Cat>().StartRoam();
        //    i++;
        //    if(i >= 9)
        //    {
        //        j++;
        //        i = 0;
        //    }

        //    catList.Add(go);
        //}

        if(CatsManager.instance.selected_cats.Length > 0)
        for(int n = 0; n < 4; n ++)
        {
            GameObject go = CatsManager.instance.selected_cats[n];
            if(go)
            {
                go.transform.SetPositionAndRotation(new Vector3(-15 + (i * 15), 0, -20 + (j * 15)), Quaternion.Euler(new Vector3(0, 180, 0)));
                go.SetActive(true);
                //if (go.GetComponent<Animator>().isActiveAndEnabled)
                //    go.GetComponent<Cat>().StartRoam();
                i++;
                if (i >= 9)
                {
                    j++;
                    i = 0;
                }

                catList.Add(go);

                NavMeshAgent nma = go.AddComponent<NavMeshAgent>();
                nma.agentTypeID = 0;
                nma.baseOffset = 0;
                nma.speed = 5.0f;
                nma.angularSpeed = 120.0f;
                nma.acceleration = 8.0f;
                nma.stoppingDistance = 0.0f;
                nma.autoBraking = true;
                nma.radius = 2.5f;
                nma.height = 3.5f;
                nma.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                nma.avoidancePriority = 50;
                nma.autoTraverseOffMeshLink = false;
                nma.autoRepath = true;

                CharacterController cc = go.AddComponent<CharacterController>();
                cc.slopeLimit = 45.0f;
                cc.stepOffset = 0.3f;
                cc.skinWidth = 0.08f;
                cc.minMoveDistance = 0.001f;
                cc.center = new Vector3(0.0f, 2.5f, 0.0f);
                cc.radius = 2.5f;
                cc.height = 3.5f;

                catAgent ca = go.AddComponent<catAgent>();


            }
            
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
        AudioManager.Instance?.Stop("BGM HQ", "bgm");


        foreach (GameObject cat in catList)
        {
            if(cat)
            {
                //cat.GetComponent<Cat>().StopRoaming();
                cat.GetComponent<CatAnimsTest>().ResetAnimator();
                cat.GetComponent<Cat>().ui.ShowAll(false);
                cat.GetComponent<Cat>().ui.ShowInteractUI(false);
                cat.GetComponent<Cat>().ui.ShowEvolve(false);
                cat.GetComponent<NavMeshAgent>()?.Destroy();
                cat.SetActive(false);
            }
            
        }
        EventManager.OnCatEvolve -= OnCatEvolve;
    }
}
