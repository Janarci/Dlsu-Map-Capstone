using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatUI : MonoBehaviour
{
    [SerializeField] private Image friendship_bar;
    [SerializeField] private Image relationship_bar;
    [SerializeField] private Image sadness_bar;
    [SerializeField] private Image hunger_bar;
    [SerializeField] private Image boredom_bar;
    [SerializeField] private Image dirt_bar;
    [SerializeField] TextMeshProUGUI name_txt;
    [SerializeField] TextMeshProUGUI level_txt;


    [SerializeField] private GameObject tooltip_popup;
    [SerializeField] private GameObject evolve_btn;
    [SerializeField] private GameObject interact_ui;

    public Cat cat;
    // Start is called before the first frame update
    void Start()
    {
        //friendship_bar = gameObject.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFriendshipBarValue(float value)
    {
        friendship_bar.transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }

    public void SetRelationshipBarValue(float value)
    {
        relationship_bar.transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }

    public void SetSadnessBarValue(float value)
    {
        sadness_bar.transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }

    public void SetHungerValue(float value)
    {
        hunger_bar.transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }

    public void SetBoredomBarValue(float value)
    {
        boredom_bar.transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }

    public void SetDirtBarValue(float value)
    {
        dirt_bar.transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }

    public void SetLevel(int level)
    {
        level_txt.text = "lvl " + level.ToString();
    }

    public void SetCat()
    {

    }
    public void SetCat(Cat cat)
    {
        this.cat = cat;
    }

    public void ShowAll(bool isShow)
    {
        friendship_bar.gameObject.SetActive(isShow);
        relationship_bar.gameObject.SetActive(isShow);
        sadness_bar.gameObject.SetActive(isShow);
        hunger_bar.gameObject.SetActive(isShow);
        boredom_bar.gameObject.SetActive(isShow);
        dirt_bar.gameObject.SetActive(isShow);
        name_txt.gameObject.SetActive(isShow);
        level_txt.gameObject.SetActive(isShow);
        //evolve_btn.SetActive(isShow);
        //interact_ui.SetActive(isShow);
    }

    
    public void ShowAffinity(bool isShow)
    {
        friendship_bar.gameObject.SetActive(isShow);
        relationship_bar.gameObject.SetActive(isShow);
    }

    public void ShowAilment(bool isShow)
    {
        sadness_bar.gameObject.SetActive(isShow);
        hunger_bar.gameObject.SetActive(isShow);
        boredom_bar.gameObject.SetActive(isShow);
        dirt_bar.gameObject.SetActive(isShow);
    }
    public void ShowInfo(bool isShow)
    {
        name_txt.gameObject.SetActive(isShow);
        level_txt.gameObject.SetActive(isShow);
    }

    public void ShowEvolve(bool isShow)
    {
        evolve_btn.SetActive(isShow);
    }

    public void ShowInteractUI(bool isShow)
    {
        interact_ui.SetActive(isShow);
    }

    public void InteractByPetting()
    {
        cat?.InteractWithCat(CatInteraction.Type.pet);
    }

    public void InteractByFeeding()
    {
        cat?.InteractWithCat(CatInteraction.Type.feed);
    }
    public void InteractByPlaying()
    {
        cat?.InteractWithCat(CatInteraction.Type.play);
    }
    public void InteractByCleaning()
    {
        cat?.InteractWithCat(CatInteraction.Type.clean);
    }

}
