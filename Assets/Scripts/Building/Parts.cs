using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    private Building ownerBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseUp()
    {
        ownerBuilding?.OnPartsClick();
    }

    public void SetBuilding(Building building)
    {
        ownerBuilding = building;
    }


}
