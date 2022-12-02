using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymCat : Cat
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

    protected override void InitializeCatFavors()
    {
        InitializeFoodFavors(5, 5, 10);
        InitializeToyFavors(20, 20, 20);
    }
}
