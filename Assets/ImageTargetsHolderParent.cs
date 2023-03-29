using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageTargetsHolderParent : MonoBehaviour
{
    private static ImageTargetsHolderParent Instance;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnChangeScene;
        }

        else
            Destroy(gameObject);    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnChangeScene(Scene s, LoadSceneMode lsm)
    {
        if (s.name == "MapScene")
            gameObject.SetActive(true);

        else
            gameObject.SetActive(false);
    }
}
