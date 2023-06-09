using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMapScene : MonoBehaviour
{
    public Image loadingBar;
    public GameObject startBtn;
    public GameObject loadingObj;

    AsyncOperation op;
    // Start is called before the first frame update
    void Start()
    {
        //op = SceneManager.LoadSceneAsync("MapScene");
        //op.allowSceneActivation = false;
        //StartCoroutine(LoadMapSceneAsync());

        StartCoroutine(LoadMapSceneAsync());
        //op = Resources.LoadAsync("");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(op.progress);

    }



    private IEnumerator LoadMapSceneAsync()
    {

        //while (!(op.isDone))
        //{
        //    loadingBar.fillAmount = op.progress;


        //    if(op.progress >= 0.9f)
        //    {
        //        startBtn.SetActive(true);
        //        startBtn.GetComponent<Button>().onClick.AddListener(delegate { op.allowSceneActivation = true; LoadMapSceneFromLoadingScreen(); }); ;
        //        break;
        //    }

        //    yield return null;

        //}
        
        //everything here is a singleton
        CameraManager.Instance.InitializeARCamera();
        while(!CameraManager.Instance.isInitialized)
        {
            Debug.Log("Initializing Camera");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Camera...";
            yield return null;
        }


        Inventory.Instance.InitializeInventory();
        while (!Inventory.Instance.isInitialized)
        {
            Debug.Log("Initializing Inventory");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Inventory...";
            yield return null;
        }

        CatDatabase.Instance.InitializeCatDatabase();
        while (!CatDatabase.Instance.isInitialized)
        {
            Debug.Log("Initializing Cats");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Cats...";
            yield return null;
        }

        BuildingDatabase.Instance.InitializeBuildingDatabase();
        while (!BuildingDatabase.Instance.isInitialized)
        {
            Debug.Log("Initializing Buildings");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Buildings...";
            yield return null;
        }

        AccomplishmentDatabase.Instance.InitializeAchievementsAndQuests();
        while (!AccomplishmentDatabase.Instance.isInitialized)
        {
            Debug.Log("Initializing achievements");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Quests and Achievements...";
            yield return null;
        }


        MissionsManager.Instance.InitializeMissionsManager();
        while (!MissionsManager.Instance.isInitialized)
        {
            Debug.Log("Initializing Missions");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Missions...";
            yield return null;
        }

        ChillSpacesManager.Instance.InitializeChillspaceManager();
        while (!ChillSpacesManager.Instance.isInitialized)
        {
            //Debug.Log("Initializing Chillspaces");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Chillspaces...";
            yield return null;
        }

        SectorManager.Instance.InitializeSectors();
        while (!SectorManager.Instance.isInitialized)
        {
            Debug.Log("Initializing Sectors");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Sectors...";
            yield return null;
        }

        AchievementsManager.instance.InitializeAccomplishmentsManager();
        while (!AchievementsManager.instance.isInitialized)
        {
            Debug.Log("Initializing accomplishments manager");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading quests and achievements 2...";
            yield return null;
        }

        DataPersistenceManager.instance.InitializeGameData();
        while (!DataPersistenceManager.instance.isInitialized)
        {
            Debug.Log("Initializing Game Data");
            loadingObj.GetComponent<TextMeshProUGUI>().text = "Loading Game Data...";
            yield return null;
        }

        

        

        


        startBtn.SetActive(true);
        loadingObj.SetActive(false);
        
    }

    public void LoadMapSceneFromLoadingScreen()
    {

        //called by button in scene
        LoadScene.LoadSectorUnlockingScene();
    }
}
