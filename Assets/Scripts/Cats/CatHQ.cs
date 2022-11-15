using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHQ : MonoBehaviour
{
    public List<GameObject> catList;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnCatBefriendSuccess += AddCat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCat(Cat targetCat)
    {
        catList.Add(targetCat.gameObject);
    }

    public void OnDestroy()
    {
        EventManager.Instance.OnCatBefriendSuccess -= AddCat;
    }
}
