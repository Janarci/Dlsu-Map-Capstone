using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandCloth : BefriendingObjects
{
    [SerializeField] private GameObject dirt;
    Transform[] dirtObjects;
    List<GameObject> intersectedDirt;
    private void Start()
    {
        base.Start();
        intersectedDirt = new List<GameObject>();
        dirtObjects = dirt.GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public bool isCleaningUpDirt()
    {
        return (intersectedDirt.Count != 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hand cloth collision 2d");
        if(collision.gameObject.activeInHierarchy && ArrayUtility.Contains(dirtObjects, collision.transform))
        {
            if(!(intersectedDirt.Contains(collision.gameObject)))
            {
                intersectedDirt.Add(collision.gameObject);
                Debug.Log("intersecting dirt piece");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Hand cloth collision 2d");
        if (collision.gameObject.activeInHierarchy && ArrayUtility.Contains(dirtObjects, collision.transform))
        {
            if (intersectedDirt.Contains(collision.gameObject))
            {
                intersectedDirt.Remove(collision.gameObject);
                Debug.Log("removing unintersected dirt piece");
            }
        }
    }

    public List<GameObject> GetCoveredDirt()
    {
        return intersectedDirt;
    }

    public void ResetIntersectedDirt()
    {
        intersectedDirt.Clear();
    }

    
}
