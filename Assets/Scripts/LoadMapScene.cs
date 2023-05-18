using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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

        CameraManager.Instance.InitializeARCamera();
        while(!CameraManager.Instance.isInitialized)
        {
            Debug.Log("Initializing Camera");
            yield return null;
        }

        CatDatabase.Instance.InitializeCatDatabase();
        while (!CatDatabase.Instance.isInitialized)
        {
            Debug.Log("Initializing Cats");
            yield return null;
        }

        DataPersistenceManager.instance.InitializeGameData();
        while (!DataPersistenceManager.instance.isInitialized)
        {
            Debug.Log("Initializing Game Data");
            yield return null;
        }

        ChillSpacesManager.Instance.InitializeChillspaceManager();
        while (!ChillSpacesManager.Instance.isInitialized)
        {
            Debug.Log("Initializing Chillspaces");
            yield return null;
        }

        SectorManager.Instance.InitializeSectors();
        while (!SectorManager.Instance.isInitialized)
        {
            Debug.Log("Initializing Sectors");
            yield return null;
        }

        startBtn.SetActive(true);
        loadingObj.SetActive(false);
        
    }

    public void LoadMapSceneFromLoadingScreen()
    {
        LoadScene.LoadSectorUnlockingScene();
    }
}
