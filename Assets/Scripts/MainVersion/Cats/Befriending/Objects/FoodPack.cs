using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPack : BefriendingObjects
{
    [SerializeField] private GameObject bowl;

    // Update is called once per frame
    void Update()
    {

    }

    public bool isInBowl()
    {
        Bounds b1 = new Bounds(bowl.transform.position, bowl.GetComponent<RectTransform>().rect.size);
        Bounds b2 = new Bounds(transform.position, gameObject.GetComponent<RectTransform>().rect.size);


        if (b1.Intersects(b2))
        {
            Debug.Log("in bowl");
            return true;
        }

        else
        {
            return false;
        }
    }
}

