using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CatalogChillSpaceInfo : MonoBehaviour
{
    [SerializeField] GameObject infoUI;
    [SerializeField] Image chillSpacePicture;
    [SerializeField] TextMeshProUGUI chillSpaceName;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] Text chillSpaceInfo;
    [SerializeField] Transform itemsListContent;
    [SerializeField] GameObject itemsListItem;
    [SerializeField] Text hrsTxt;
    [SerializeField] Text contactsTxt;
    [SerializeField] Button claimBtn;

    [SerializeField] CatalogCatInfo catalogCatInfo;
    [SerializeField] Button quizBtn;
    [SerializeField] QuizController quizController;
    [SerializeField] ChillspaceQuizPool QuizPool;
    [SerializeField] GameObject quizUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetChillSpaceDetails(ChillSpace.Detail data)
    {
        if(ChillSpacesManager.Instance.unlocked_chillspaces.Contains(data.area))
        {
            chillSpacePicture.sprite = data.picture;
            chillSpaceName.text = data.areaName;
            chillSpaceName.gameObject.SetActive(true);
            nameField.gameObject.SetActive(false);

            claimBtn.onClick.AddListener(delegate { OnClaimItemPress(data.area); });
            chillSpaceInfo.text = data.info;

            hrsTxt.text = data.officeHours;
            contactsTxt.text = data.email + "\n" + data.contactNumber;

            AddItems(ChillSpaceDatabase.Instance.GetDataInfo(data.area).giveawayItems);

        }

        else
        {
            chillSpacePicture.sprite = data.picture;
            chillSpaceName.gameObject.SetActive(false);
            nameField.gameObject.SetActive(true);
            nameField.onEndEdit.AddListener(delegate { ValueChangeCheck(data); });
            chillSpaceInfo.text = data.info;

            hrsTxt.text = null;
            contactsTxt.text = null;

            AddItems(null);
        }

        //quizBtn.onClick.AddListener(delegate {StartChillspaceQuiz(data.area);});

        
    }

    public void ValueChangeCheck(ChillSpace.Detail data)
    {
        Debug.Log("Value Changed");

        if((String.Compare(nameField.text, data.areaName, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0) || (String.Compare(nameField.text, data.abbreviation, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0 && data.abbreviation != string.Empty))
        {
            Debug.Log("Correct");
            ChillSpacesManager.Instance.UnlockChillSpace(data.area);

            SetChillSpaceDetails(ChillSpaceDatabase.Instance.GetDataInfo(data.area));
        }

        else
        {
            Debug.Log(data.areaName);
        }
    }

    public void OnClaimItemPress(ChillSpace.Area area)
    {
        if(ChillSpacesManager.Instance.GetItemFromChillSpace(area))
        {
            claimBtn.gameObject.SetActive(false);
        }
    }

    public void AddItems(List<CatEvolutionItem.cat_evolution_item_type> data)
    {
        //foreach(GameObject c in chillspaceListContent.transform)
        //{
        //    Destroy(c);
        //}

        if (itemsListContent.transform.childCount > 0)
            for (int i = itemsListContent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(itemsListContent.transform.GetChild(i).gameObject);
            }

        if (data == null)
            return;

        foreach (CatEvolutionItem.cat_evolution_item_type item in data)
        {
            GameObject itemObj = Instantiate(itemsListItem, itemsListContent);
            itemObj.GetComponent<Image>().sprite = Inventory.Instance.GetDataInfo(item).icon;
        }
    }

    public void StartChillspaceQuiz(ChillSpace.Area _area)
    {

        ChillspaceQuizPool.Quizzes.Quiz quiz = QuizPool.GetQuestion(_area);

        if(quiz != null)
        {
            infoUI.SetActive(false);
            quizUI.SetActive(true);
            quizController.InitializeGame(quiz.quiz, quiz.answer);
            quizController.OnFinishQuiz += EndQuiz;
        }

        void EndQuiz(bool _isSuccess)
        {
            quizController.OnFinishQuiz -= EndQuiz;
            quizUI.SetActive(false);
            infoUI.SetActive(true);
            ClaimChillspaceItemFromQuiz(_isSuccess, _area);
        }
    }
    
    

    public void ClaimChillspaceItemFromQuiz(bool _isSuccess, ChillSpace.Area _area)
    {
        if(_isSuccess)
        {
            ChillSpacesManager.Instance.GetItemFromChillspaceQuiz(_area, _isSuccess);
        }
    }
}
