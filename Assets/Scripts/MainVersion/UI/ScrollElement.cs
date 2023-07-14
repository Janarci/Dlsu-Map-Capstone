using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollElement : MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler, IScrollHandler
{
    public ScrollRect MainScroll;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        MainScroll.OnBeginDrag(eventData);
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on drag");
        MainScroll.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        MainScroll.OnEndDrag(eventData);
    }


    public void OnScroll(PointerEventData data)
    {
        Debug.Log("on scroll");
        MainScroll.OnScroll(data);
    }


}
