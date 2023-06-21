using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewModelEvolutionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnCatClick+= OnCatClick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCatClick(Cat cat)
    {
        Debug.Log("clicked cat test");
        cat.GetComponent<Cat>().ui.ShowEvolve(true);

    }

}
