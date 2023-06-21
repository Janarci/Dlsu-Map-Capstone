using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public GameObject miniMenu;
    public GameObject mainUIBtn;
    public GameObject inventoryBtn;
    public GameObject resetCameraBtn;
    public GameObject questsBtn;
    public GameObject HQBtn;
    public GameObject CatalogBtn;
    public GameObject inventoryUI;
    public GameObject questsUI;
    public GameObject SwitchCameraBtn;
    public GameObject playerIconObj;
    public GameObject fChar;
    public GameObject mChar;


    public Sprite menuBtnDefault;
    public Sprite menuBtnPressed;
    public Sprite cameraModeBtn1;
    public Sprite cameraModeBtn2;
    public Sprite playerIconM;
    public Sprite playerIconF;

    public GameObject LoadingCanvas;
    public GameObject MainCanvas;
    public GameObject CatalogCanvas;



    bool isShowingMiniMenu = false;


    // Start is called before the first frame update
    void Start()
    {
        Inventory.Instance?.StartInventoryUI(inventoryUI);
        questsUI.TryGetComponent<QuestsList>(out QuestsList ql);
        if (ql)
        {
            ql.StartQuestsUI();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideMiniMenu()
    {
        miniMenu.SetActive(false);
        resetCameraBtn.transform.localPosition = Vector3.zero;
        inventoryBtn.transform.localPosition = Vector3.zero;
        CatalogBtn.transform.localPosition = Vector3.zero;
        questsBtn.transform.localPosition = Vector3.zero;

    }

    public void DisplayMiniMenu()
    {
        miniMenu.SetActive(true);
        StartCoroutine(startMiniMenuAnimation());
    }

    public void toggleMiniMenu()
    {
        if (miniMenu.activeInHierarchy)
        {
            HideMiniMenu();
            mainUIBtn.GetComponent<Image>().sprite = menuBtnDefault;

        }

        else
        {
            DisplayMiniMenu();
            mainUIBtn.GetComponent<Image>().sprite = menuBtnPressed;
            inventoryUI.SetActive(false);
        }
    }

    public void ToggleCameraView()
    {
        if (SwitchCameraBtn.GetComponent<Image>().sprite == cameraModeBtn1)
        {
            SwitchCameraBtn.GetComponent<Image>().sprite = cameraModeBtn2;
            RotateMap.shouldRotate = false;
        }

        else
        {
            SwitchCameraBtn.GetComponent<Image>().sprite = cameraModeBtn1;
            RotateMap.shouldRotate = true;

        }
    }

    public void TogglePlayerIcon()
    {
        if (playerIconObj.GetComponent<Image>().sprite == playerIconM)
        {
            playerIconObj.GetComponent<Image>().sprite = playerIconF;
            fChar.SetActive(true);
            mChar.SetActive(false);
        }

        else
        {
            playerIconObj.GetComponent<Image>().sprite = playerIconM;
            fChar.SetActive(false);
            mChar.SetActive(true);
        }

    }

    private IEnumerator startMiniMenuAnimation()
    {
        float currLerp = 0.0f;
        Vector3 btn1InitialPos = resetCameraBtn.transform.position;
        Vector3 btn2InitialPos = inventoryBtn.transform.position;
        Vector3 btn3InitialPos = CatalogBtn.transform.position;
        Vector3 btn4InitialPos = questsBtn.transform.position;

        Vector3 btn1FinalPos = new Vector3(mainUIBtn.transform.position.x - (220 * (Screen.width / 720.0f)), mainUIBtn.transform.position.y + (90.0f * (Screen.height / 1440.0f)), mainUIBtn.transform.position.z);
        Vector3 btn2FinalPos = new Vector3(mainUIBtn.transform.position.x - (110 * (Screen.width / 720.0f)), mainUIBtn.transform.position.y + (215 * (Screen.height / 1440.0f)), mainUIBtn.transform.position.z);
        Vector3 btn3FinalPos = new Vector3(mainUIBtn.transform.position.x + (220 * (Screen.width / 720.0f)), mainUIBtn.transform.position.y + (90.0f * (Screen.height / 1440.0f)), mainUIBtn.transform.position.z);
        Vector3 btn4FinalPos = new Vector3(mainUIBtn.transform.position.x + (110 * (Screen.width / 720.0f)), mainUIBtn.transform.position.y + (215 * (Screen.height / 1440.0f)), mainUIBtn.transform.position.z);

        isShowingMiniMenu = true;
        while (isShowingMiniMenu)
        {
            if (currLerp >= 1.0f)
            {
                isShowingMiniMenu = false;
                currLerp = 0.0f;
                break;
            }
            resetCameraBtn.transform.position = Vector3.Lerp(btn1InitialPos, btn1FinalPos, currLerp);
            inventoryBtn.transform.position = Vector3.Lerp(btn2InitialPos, btn2FinalPos, currLerp);
            CatalogBtn.transform.position = Vector3.Lerp(btn3InitialPos, btn3FinalPos, currLerp);
            questsBtn.transform.position = Vector3.Lerp(btn4InitialPos, btn4FinalPos, currLerp);

            currLerp += Time.deltaTime * 5.0f;
            yield return null;
        }
    }

    public void DisplayInventory()
    {
        HideMiniMenu();
        inventoryUI.SetActive(true);
    }

    public void DisplayQuests()
    {
        HideMiniMenu();
        questsUI.SetActive(true);
    }

    public void HideMainCanvasUI()
    {
        mainUIBtn.SetActive(false);
        HQBtn.SetActive(false);
        playerIconObj.SetActive(false);
        SwitchCameraBtn.SetActive(false);
    }

    public void TestAchievement()
    {
        //AchievementsManager.instance.AccomplishAchievemnt(Accomplishment.Type.BefriendedFirstCat);
        AchievementsManager.instance.ProgressQuest(Quest.QuestCode.befriend_cats, 1);
    }

    public void PlayMapBGM()
    {
        AudioManager.Instance.Play("BGM Main", "bgm");
    }

    public void OnDestroy()
    {
        AudioManager.Instance.Stop("BGM Main", "bgm");
    }
}
