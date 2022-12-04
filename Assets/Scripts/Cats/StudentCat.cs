using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentCat : Cat
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Debug.Log(this.GetCatType());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    protected override void InitializeCatType()
    {
        school_tip = "Students can enter DLSU by scanning the tag found on either their ID or EAF";
        this.type = cat_type.student_cat;
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
