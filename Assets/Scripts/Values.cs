using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Values
{
    
    public static List<GameObject> befriended_cats;
    public static List<CatType.Type> collected_cat_types;
    public static List<int> unlocked_sectors;
    public static List<ChillSpace.Area> unlocked_chillspaces;
    public static List<Item> inventory;
    public static GameObject[] selected_cats;
    public static GameObject approached_cat;
    public static int belonging_sector;
    public static float runTime = 0.0f;

    static Values()
    {
        befriended_cats = new List<GameObject>();
        collected_cat_types= new List<CatType.Type>();
        unlocked_sectors = new List<int>();
        unlocked_chillspaces = new List<ChillSpace.Area>();
        approached_cat = null;
        belonging_sector = -1;
        selected_cats = new GameObject[4];
    }
}
