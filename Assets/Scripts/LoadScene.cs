using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void LoadHQScene()
    {
        SceneManager.LoadScene("SergaHQ");
    }

    public static void LoadCatBefriendingScene()
    {
        //SceneManager.LoadScene("SergaCatBefriending");
        SceneManager.LoadScene("BefriendingScene");
    }

    public static void LoadSectorUnlockingScene()
    {
        SceneManager.LoadScene("MapScene");
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

    public static void LoadCatalogScene()
    {
        SceneManager.LoadScene("Catalog");

    }
}
