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

    protected override void InitializeCatType()
    {
        school_tip = "Books can be borrowed from the Library";
        type = cat_type.library_cat;
    }

    protected override void InitializeCatFavors()
    {
        InitializeFoodFavors(10, 20, 5);
        InitializeToyFavors(10, 5, 20);
        
    }

    protected override void InitializeEvolutionPath()
    {
        
    }



}