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
    [SerializeField] GameObject interactUI;


    Cat cat;

    public void SetValues(Cat _cat)
    {
        cat = _cat;
        CatType.Type type = _cat.GetCatType();
        if(CatDatabase.Instance)
        {
            typeTxt.text = CatDatabase.Instance.GetCatData(type).catTypeLabel;
            catIcon.sprite = CatDatabase.Instance.GetCatData(type).icon;
        }    
        
        sadnessBar.fillAmount= cat.GetSadnessPercentage();
        hungerBar.fillAmount= cat.GetHungerPercentage();
        boredomBar.fillAmount= cat.GetBoredomPercentage();
        dirtBar.fillAmount= cat.GetDirtPercentage();
    }

    public void InteractByPetting()
    {
        cat?.InteractWithCat(CatInteraction.Type.pet);
        sadnessBar.fillAmount = cat.GetSadnessPercentage();

    }

    public void InteractByFeeding()
    {
        cat?.InteractWithCat(CatInteraction.Type.feed);
        hungerBar.fillAmount = cat.GetHungerPercentage();

    }
    public void InteractByPlaying()
    {
        cat?.InteractWithCat(CatInteraction.Type.play);
        boredomBar.fillAmount = cat.GetBoredomPercentage();

    }
    public void InteractByCleaning()
    {
        cat?.InteractWithCat(CatInteraction.Type.clean);
        dirtBar.fillAmount = cat.GetDirtPercentage();

    }

    public void ShowInteractUI(bool isShow)
    {
        interactUI.SetActive(isShow);
    }
}
