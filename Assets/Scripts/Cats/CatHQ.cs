using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHQ : MonoBehaviour
{
    public List<GameObject> catList;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnCatBefriend += AddCat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCat(Cat targetCat, bool isBefriended)
    {
        if(isBefriended)
            catList.Add(targetCat.gameObject);
    }

    public void OnDestroy()
    {
        EventManager.OnCatBefriend -= AddCat;
    }
}
