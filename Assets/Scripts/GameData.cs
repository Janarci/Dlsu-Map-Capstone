using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    [Serializable]
    public class CatData
    {
        public int id = -1;
        public CatType.Type type;
        public float friendship_value = 0;
        public float relationship_value = 0;
        public int relationship_level = 0;
        public int befriend_attempts = 0;
        public CatTrait.Type trait = CatTrait.Type.none;
        public float sadnessAilmentValue;
        public float hungerAilmentValue;
        public float boredomAilmentValue;
        public float dirtAilmentValue;
    }

    public  List<CatData> befriended_cats;
    public  List<CatType.Type> collected_cat_types;
    public  List<int> unlocked_sectors;
    public  List<ChillSpace.Area> unlocked_chillspaces;
    public  List<Item> inventory;
    public  CatData[] selected_cats;
    public  GameObject approached_cat;
    public  int belonging_sector;
    public  float runTime = 0.0f;

    public GameData()
    {
        befriended_cats = new List<CatData>();
        collected_cat_types= new List<CatType.Type>();
        unlocked_sectors = new List<int>();
        unlocked_chillspaces = new List<ChillSpace.Area>();
        approached_cat = null;
        belonging_sector = -1;
        selected_cats = new CatData[4];
    }
}
