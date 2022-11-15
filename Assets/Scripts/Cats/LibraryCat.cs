using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryCat : Cat
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void InitializeCat()
    {
        InitializeFoodFavors(10, 20, 5);
        InitializeToyFavors(10, 5, 20);
    }    
        


}
