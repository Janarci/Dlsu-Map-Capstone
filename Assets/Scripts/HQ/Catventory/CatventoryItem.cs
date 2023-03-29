using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI typeTxt;
    [SerializeField] Image catIcon;
    [SerializeField] Image sadnessBar;
    [SerializeField] Image hungerBar;
    [SerializeField] Image boredomBar;
    [SerializeField] Image dirtBar;


    public void SetValues(CatType.Type type, float sadnessPercentage, float hungerPercentage, float boredomPercentage, float dirtPercentage)
    {
        if(CatDatabase.Instance)
        {
            typeTxt.text = CatDatabase.Instance.GetCatData(type).catTypeLabel;
            catIcon.sprite = CatDatabase.Instance.GetCatData(type).icon;
        }    
        
        sadnessBar.fillAmount= sadnessPercentage;
        hungerBar.fillAmount= hungerPercentage;
        boredomBar.fillAmount= boredomPercentage;
        dirtBar.fillAmount= dirtPercentage;
    }
}
