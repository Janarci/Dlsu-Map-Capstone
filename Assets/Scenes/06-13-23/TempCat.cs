using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        //GetComponent<Cat>().,(true);

        //GetComponentInChildren<CatUI>().ShowAll(true);
        //GetComponentInChildren<CatUI>().ShowEvolve(true);
        GetComponent<CatAnimsTest>().cat_walk_tail_start();

    }
}
