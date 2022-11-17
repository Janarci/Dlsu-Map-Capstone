using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void LoadHQScene()
    {
        SceneManager.LoadScene("HQ");
    }

    public static void LoadCatBefriendingScene()
    {
        SceneManager.LoadScene("0");
    }

    public static void LoadSectorUnlockingScene()
    {
        SceneManager.LoadScene("ARTest");
    }

    public static void LoadMapScene()
    {
        SceneManager.LoadScene("PlayingAround");

    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("Splash");

    }

    public static void LoadDebugScene()
    {
        SceneManager.LoadScene("Debug");
    }
}
