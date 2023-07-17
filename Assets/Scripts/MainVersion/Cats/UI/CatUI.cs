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

    [SerializeField] private Canvas evolutionChoiceUI;
    [SerializeField] private GameObject evolutionChoiceItem;
    [SerializeField] private Transform evolutionChoiceContent;
    [SerializeField] private GameObject itemRequirementItem;
    //[SerializeField] private Transform itemRequirementContent;


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

    public void ShowEvolutionUI(bool isShow)
    {
        evolutionChoiceUI.gameObject.SetActive(isShow);
    }

    public void AddEvolutions()
    {
        for(int i = 0; i < evolutionChoiceContent.childCount; i++)
        {
            Destroy(evolutionChoiceContent.GetChild(i).gameObject);
        }

        foreach(CatEvolutionRequirement cer in CatDatabase.Instance.GetCatData(cat.GetCatType()).evolutions)
        {
            CatType.Type type = cer.catType;
            GameObject evolutionChoiceObj = Instantiate(evolutionChoiceItem, evolutionChoiceContent);
            GameObject evolutionChoiceBtnObj = evolutionChoiceObj.transform.GetChild(0).gameObject;
            Button evolutionChoiceBtnComp = evolutionChoiceBtnObj.GetComponent<Button>();
            Image evolutionChoiceImgComp = evolutionChoiceBtnObj.GetComponent<Image>();
            Text evolutionChoiceTxtComp = evolutionChoiceBtnObj.transform.GetChild(0).gameObject.GetComponent<Text>();

            Transform itemRequirementContent = evolutionChoiceBtnObj.transform.GetChild(1).GetChild(0).gameObject.transform;

            evolutionChoiceTxtComp.text = CatDatabase.Instance?.GetCatData(type).catTypeLabel;


            if (cat.CanEvolveTo(type))
            {
                evolutionChoiceBtnComp.onClick.AddListener(delegate { cat.EvolveTo(type); ShowEvolutionUI(false); });
                evolutionChoiceImgComp.color = Color.green;
                Debug.Log("Success");
            }

            else
            {
                evolutionChoiceBtnComp.enabled = false;
                evolutionChoiceImgComp.color = Color.grey;
                Debug.Log("Fail");
            }

            foreach(CatEvolutionRequirement.EvolutionRequirement req in cer.requirement)
            {
                CatEvolutionItem.cat_evolution_item_type item = req.item;
                GameObject itemRequirementObj = Instantiate(itemRequirementItem, itemRequirementContent);
                itemRequirementObj.GetComponent<Image>().sprite = Inventory.Instance?.GetDataInfo(item).icon;
                itemRequirementObj.GetComponent<LayoutElement>().preferredHeight = 80;
                itemRequirementObj.GetComponent<LayoutElement>().preferredWidth = 80;
                itemRequirementObj.transform.GetChild(0).GetComponent<Text>().text = req.amount.ToString();

            }
        }
    }

    public void AddItemRequirement(CatEvolutionItem.cat_evolution_item_type type, Transform tf)
    {
        //GameObject 
    }

    public void OnEvolveBtnPress()
    {
        ShowEvolutionUI(true);
        AddEvolutions();
        Debug.Log("Evolve");
    }

    public void InteractByPetting()
    {
        //cat?.InteractWithCat(CatInteraction.Type.pet);
        cat?.InteractWithCat();
    }

    public void InteractByFeeding()
    {
        //cat?.InteractWithCat(CatInteraction.Type.feed);
        cat?.InteractWithCat();
    }
    public void InteractByPlaying()
    {
        //cat?.InteractWithCat(CatInteraction.Type.play);
        cat?.InteractWithCat();
    }
    public void InteractByCleaning()
    {
        //cat?.InteractWithCat(CatInteraction.Type.clean);
        cat?.InteractWithCat();

    }

}
