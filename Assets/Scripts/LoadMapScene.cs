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
        while(
            (!SectorManager.Instance.isInitialized) 
            ||
            (!ChillSpacesManager.Instance.isInitialized)
            ||
            (!MissionsManager.Instance.isInitialized)
            ||
            (!CatDatabase.Instance.isInitialized)
            ||
            (!CameraManager.Instance.isInitialized))
        {
            Debug.Log("loading");
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
