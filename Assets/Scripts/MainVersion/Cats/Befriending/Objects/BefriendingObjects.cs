using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendingObjects : MonoBehaviour
{
    public enum BefriendingType
    {
        None,
        Petting,
        Feeding,
        Playing,
        Cleaning
    }
    public bool isSelected { get; protected set; }
    public Vector3 initialPos { get; private set; }
    public BefriendingType type = BefriendingType.None;
    // Start is called before the first frame update
    protected void Start()
    {
        isSelected = false;
        initialPos = transform.position;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(isSelected)
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //            isSelected = false;
        //    }

        //    else if (Input.mousePresent != false)
        //    {
        //        if (Input.GetMouseButtonUp(0))
        //            isSelected = false;
        //    }
        //}
    }

    public void SelectObject()
    {
        Debug.Log("Selected");
        isSelected = true;
    }

    public virtual void UnselectObject()
    {
        isSelected = false;
        transform.position = initialPos;
    }
}
