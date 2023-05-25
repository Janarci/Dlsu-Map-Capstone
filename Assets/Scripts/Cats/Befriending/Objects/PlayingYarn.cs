using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingYarn : BefriendingObjects
{
    bool isInAir = false;
    public Vector3 direction = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(isInAir)
        {
            Debug.Log("Moving: " + direction *Time.deltaTime * 100);
            transform.position += direction * Time.deltaTime * 100;
        }
    }

    public bool IsInAir()
    {
        return isInAir;
    }

    public void Throw()
    {
        isInAir = true;
        direction = (transform.position - initialPos).normalized;
        isSelected = false;
        Debug.Log("Launching");
    }

    public bool CanLaunch()
    {
        float distance = Vector3.Distance(initialPos, direction);
        Debug.Log(distance);
        return distance >= 5.5f;
    }

    public override void UnselectObject()
    {
        base.UnselectObject();
        isInAir = false;
    }
}
