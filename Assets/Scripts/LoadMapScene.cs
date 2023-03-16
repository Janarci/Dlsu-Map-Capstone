using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMapScene : MonoBehaviour
{
    public Image loadingBar;
    public GameObject startBtn;
    AsyncOperation op;
    // Start is called before the first frame update
    void Start()
    {
        //op = SceneManager.LoadSceneAsync("MapScene");
        //op.allowSceneActivation = false;
        //StartCoroutine(LoadMapSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LoadMapSceneAsync()
    {

        while (!(op.isDone))
        {
            loadingBar.fillAmount = op.progress;
            Debug.Log(op.progress);

            if(op.progress >= 0.9f)
            {
                startBtn.SetActive(true);
                startBtn.GetComponent<Button>().onClick.AddListener(delegate { op.allowSceneActivation = true; LoadMapSceneFromLoadingScreen(); }); ;
                break;
            }

            yield return null;

        }
        

        
    }

    public void LoadMapSceneFromLoadingScreen()
    {
        LoadScene.LoadSectorUnlockingScene();
    }
}
