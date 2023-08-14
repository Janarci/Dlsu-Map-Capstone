using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveTest : MonoBehaviour
{
    public Cat cat;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        CatDatabase.Instance.InitializeCatDatabase();

        while (CatDatabase.Instance.isInitialized ==false)
        {
            yield return null;
        }

        Inventory.Instance.InitializeInventory();

        while (Inventory.Instance.isInitialized ==false) { yield return null; }

        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUpCat()
    {
        cat.InteractWithCat();
    }
}
