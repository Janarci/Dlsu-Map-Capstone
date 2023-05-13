using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsList : MonoBehaviour
{
    public static List<GameObject> befriended_cats;
    public static GameObject[] selected_cats;
    public static List<GameObject> stashed_cat_spawns;
    public static List<KeyValuePair<int, int>> queuedSpawns;
    public static bool spawn_cats;
    public static int num_base_cats;
    public static int num_sectors;
    // Start is called before the first frame update
    static CatsList()
    {
        befriended_cats= new List<GameObject>();
        selected_cats = new GameObject[4];
        stashed_cat_spawns = new List<GameObject>();
        queuedSpawns = new List<KeyValuePair<int, int>>();
        spawn_cats = true;
        num_base_cats = -1;
        num_sectors = -1;
    }

    public static void AddNewCatToQueue()
    {
        if(num_base_cats != -1 && num_sectors != -1)
        {
            int catIndex = UnityEngine.Random.Range(0, num_base_cats);
            int sectorIndex = UnityEngine.Random.Range(0, num_sectors);
            KeyValuePair<int, int> cat_spawn = new KeyValuePair<int, int>(catIndex, sectorIndex);
            queuedSpawns.Add(cat_spawn);

        }

        else
        {
            Debug.Log("undefined num cats or num sectors");
        }
    }

    public static void AddExistingCatToStash(int catIndex, int sectorIndex)
    {
        KeyValuePair<int, int> cat_spawn = new KeyValuePair<int, int>(catIndex, sectorIndex);
        queuedSpawns.Add(cat_spawn);
    }
}
