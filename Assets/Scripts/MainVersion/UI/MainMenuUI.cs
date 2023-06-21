using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    //[SerializeField] Button befriendCatsBtn;
    //[SerializeField] Button unlockSectorsBtn;
    //[SerializeField] Button openMapBtn;
    //[SerializeField] Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToBefriendCatsScene()
    {
        LoadScene.LoadCatBefriendingScene();
    }

    public void GoToUnlockSectorsScene()
    {
        LoadScene.LoadSectorUnlockingScene();
    }

    public void GoToMapScene()
    {
        LoadScene.LoadMapScene();
    }

    public void GoToMenuScene()
    {
        LoadScene.LoadMenuScene();
    }

    public void GoToDebugScene()
    {
        LoadScene.LoadDebugScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
