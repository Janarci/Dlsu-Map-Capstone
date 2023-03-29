using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCats : MonoBehaviour
{
    static UpdateCats Instance;
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
        if(Values.befriended_cats.Count > 0)
        {
            foreach(GameObject cat in Values.befriended_cats)
            {
                Cat catComp = cat.GetComponent<Cat>();
                catComp.UpdateAilmentStatus();
                for(int i = 0; i < 4; i++) 
                {
                    if (Values.selected_cats[i] == cat)
                    {
                        if((catComp.GetSadnessPercentage() + catComp.GetHungerPercentage() + catComp.GetBoredomPercentage() + catComp.GetDirtPercentage()) > 2.0f)
                        {
                            Debug.LogError("Removing selected cat index " + i);
                            Values.selected_cats[i] = null;
                        }
                        break;
                    }
                }
            }
        }
    }
}
