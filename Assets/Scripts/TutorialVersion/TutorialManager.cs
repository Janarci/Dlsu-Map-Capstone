using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [Serializable] public enum Type
    {
        none,
        intro,
        ar_camera,
        qr_scanning,
        cat_spawn,
        cat_befriend,
        cat_befriend_fail,
        cat_befriend_success,
        cat_relationship_up,
        cat_evolve,
        open_hq,
        open_catalog,
        open_inventory,
        open_quest,

    }
    [Serializable]
    class TutorialPhase
    {
        public Type type = Type.none;
        public string instruction;
        GameObject popup = null;
        public GameObject[] pointers;

        bool isActive = false;
        public IEnumerator Activate()
        {
            popup = PopupGenerator.Instance?.GenerateTutorialPopup(instruction);
            Debug.Log("active");
            if(pointers.Length != 0)
            {
                for(int i = 0; i < pointers.Length; i++)
                {
                    pointers[i].SetActive(true);
                }
            }

            isActive = true;

            while (isActive)
            {
                if (!(Time.timeScale == 0))
                {
                    Time.timeScale = 0;
                }


                if (Input.GetMouseButton(0))
                {
                    Deactivate();
                }
                yield return null;
            }

            Time.timeScale = 1;
        }

        public void Deactivate()
        {
            popup?.Destroy();
            if (pointers.Length != 0)
            {
                for (int i = 0; i < pointers.Length; i++)
                {
                    pointers[i].SetActive(false);
                }
            }

            isActive = false;
            AudioManager.Instance?.Play("Popup Close", "sfx", false);
        }

        //IEnumerator pause()
        //{
            

        //}



    }
    [SerializeField] TutorialPhase[] tutorials;
    public int currentTutorialIndex { get; private set; }

    List<Type> unlockedTutorials;
    Dictionary<Type, TutorialPhase> mappedDatabase;
    public static TutorialManager instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            unlockedTutorials = new List<Type>();
            mappedDatabase = new Dictionary<Type, TutorialPhase>();
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach(TutorialPhase t in tutorials)
        {
            if(!(mappedDatabase.ContainsKey(t.type)))
            {
                mappedDatabase[t.type] = t;
            }
        }
    }

    //public void NextTutorial()
    //{
    //    tutorials[currentTutorialIndex].SetActive(false);
    //    currentTutorialIndex++;

    //    if (currentTutorialIndex >= tutorials.Length)
    //    {
    //        currentTutorialIndex = 0;
    //    }

    //    tutorials[currentTutorialIndex].SetActive(true);
    //}

    //public void PreviousTutorial()
    //{
    //    tutorials[currentTutorialIndex].SetActive(false);
    //    currentTutorialIndex--;

    //    if (currentTutorialIndex < 0)
    //    {
    //        currentTutorialIndex = tutorials.Length - 1;
    //    }

    //    tutorials[currentTutorialIndex].SetActive(true);
    //}

    //public void EndTutorial()
    //{
    //    if(currentTutorialIndex == tutorials.Length - 1)
    //    {

    //    }
    //}    

    public void UnlockTutorial(Type _type)
    {
        if (!(unlockedTutorials.Contains(_type)))
        {
            if (mappedDatabase.ContainsKey(_type))
            {
                StartCoroutine(mappedDatabase[_type].Activate());
                unlockedTutorials.Add(_type);
            }
                
        }

        else
            Debug.Log("tutorial " + _type + " already finished");
    }


}
