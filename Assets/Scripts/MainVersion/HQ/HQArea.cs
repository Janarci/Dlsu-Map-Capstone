using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class HQArea : MonoBehaviour
{
    [Serializable]
    public enum Area
    {
        none,
        floor,
        rug,
        couch,
    }

    public Area area;
    public Collider collider;
    public bool isOccupied = false;
    public catAgent occupant = null;

    public bool isInArea(Vector3 point)
    {
        //Debug.Log(collider.bounds.Contains(point));
        return (collider.bounds.Contains(point));
    }
}
