using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQCatsPanelIndex : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] Image icon;
    public GameObject replacementCat = null;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnHQCatReplace += OnReplaceCat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceHQCat()
    {
        if (replacementCat != null)
        {
            icon.sprite = CatDatabase.Instance.GetCatData(replacementCat.GetComponent<Cat>().GetCatType()).icon;
            EventManager.HQCatReplaced(replacementCat.GetComponent<Cat>(), id);
        }
    }

    public void OnReplaceCat(Cat _replacementCat, int _index)
    {
        replacementCat = null;
    }

    public void OnDestroy()
    {
        EventManager.OnHQCatReplace -= OnReplaceCat;
    }
}
