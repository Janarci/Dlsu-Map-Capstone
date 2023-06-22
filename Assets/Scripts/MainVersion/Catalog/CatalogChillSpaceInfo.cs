using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CatalogChillSpaceInfo : MonoBehaviour
{
    [SerializeField] GameObject infoUI;
    [SerializeField] Image chillSpacePicture;
    [SerializeField] TextMeshProUGUI chillSpaceName;
    [SerializeField] Text chillSpaceInfo;
    [SerializeField] Transform itemsListContent;
    [SerializeField] GameObject itemsListItem;
    [SerializeField] Text hrsTxt;
    [SerializeField] Text contactsTxt;

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
            chillSpaceInfo.text = data.info;

            hrsTxt.text = data.officeHours;
            contactsTxt.text = data.email + "\n" + data.contactNumber;

            AddItems(ChillSpaceDatabase.Instance.GetDataInfo(data.area).giveawayItems);
        }

        else
        {
            chillSpacePicture.sprite = data.picture;
            chillSpaceName.text = data.areaName;
            chillSpaceInfo.text = null;

            hrsTxt.text = null;
            contactsTxt.text = null;

            AddItems(null);
        }

        quizBtn.onClick.AddListener(delegate {StartChillspaceQuiz(data.area);});

        
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
