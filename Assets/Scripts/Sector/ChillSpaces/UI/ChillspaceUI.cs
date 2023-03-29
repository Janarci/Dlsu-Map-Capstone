using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChillspaceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI areaName;
    [SerializeField] private Text areaInfo;
    [SerializeField] private Text areaContactNumber;
    [SerializeField] private Text areaEmail;
    [SerializeField] private Text areaItems;




    [SerializeField] private Image image;
    [SerializeField] TextMeshProUGUI cdTxt;
    [SerializeField] TextMeshProUGUI btnText;


    public Action OnChillspaceUIButtonPress;
    public Action OnChillspaceUIPicturePress;

    // Start is called before the first frame update
    void Start()
    {
        SetBtnText("Locked");
        if(image.GetComponent<EventTrigger>() != null)
        {
            EventTrigger.Entry click = new EventTrigger.Entry();
            click.eventID = EventTriggerType.PointerDown;
            click.callback = new EventTrigger.TriggerEvent();
            click.callback.AddListener(delegate { OnChillspaceUIPicturePress(); });
            image.GetComponent<EventTrigger>().triggers.Add(click);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(areaName.text);
    }

    public void SetName(string name)
    {
        areaName.text = name;
        
        //Debug.Log("Changing name");
        //Debug.Log(areaName.text);
    }

    public void SetInfo(string info)
    {
        this.areaInfo.text = info;
    }

    public void SetNumber(string number)
    {
        this.areaContactNumber.text = number;
    }

    public void SetEmail(string email)
    {
        this.areaEmail.text = email;
    }

    public void SetItems(List<CatEvolutionItem.cat_evolution_item_type> items)
    {
        foreach(CatEvolutionItem.cat_evolution_item_type item in items)
        {
            areaItems.text += (item.ToString() + ",\n");
        }
    }

    public void SetPicture(Sprite picture)
    {
        image.sprite = picture;
    }

    

    public void SetBtnText(string text)
    {
        btnText.text = text;
    }

    public void SetCD(string text)
    {
        if (cdTxt == null) Debug.Log("cdText null");
        cdTxt.text = text;
    }

    public void ChillspaceUIButtonPress()
    {
        if (OnChillspaceUIButtonPress != null)
        {
            OnChillspaceUIButtonPress();
        }
    }

    public void ChillspaceUIOnPicturePress()
    {
        if(OnChillspaceUIPicturePress != null)
        {
            OnChillspaceUIPicturePress();

        }
    }
}
