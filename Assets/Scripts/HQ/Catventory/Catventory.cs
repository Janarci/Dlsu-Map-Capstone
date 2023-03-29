using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catventory : MonoBehaviour
{
    private List<CatventoryItem> cats = new List<CatventoryItem>();
    [SerializeField] GameObject catventoryItemTemplate;
    [SerializeField] Transform catventoryContent;
    // Start is called before the first frame update
    void Start()
    {
        AddCats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCats()
    {
        if(!(Values.befriended_cats.Count == 0))
        {
            foreach(GameObject befriended_cat in Values.befriended_cats)
            {
                GameObject catventory_item = Instantiate(catventoryItemTemplate, catventoryContent);
                Cat catComp = befriended_cat.GetComponent<Cat>();
                CatventoryItem itemComp = catventory_item.GetComponent<CatventoryItem>();
                if(catComp && itemComp)
                {
                    itemComp.SetValues(catComp.GetCatType(), catComp.GetSadnessPercentage(), catComp.GetHungerPercentage(), catComp.GetBoredomPercentage(), catComp.GetDirtPercentage());
                }

            }
        }
    }
}
