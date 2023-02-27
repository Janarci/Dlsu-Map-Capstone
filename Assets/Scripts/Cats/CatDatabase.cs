using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDatabase : MonoBehaviour
{
    public static CatDatabase Instance;
    [Serializable] public class CatData
    {
        public cat_type type;
        public GameObject model;
        public Cat script;
    }

    [SerializeField] public List<CatData> data;
    private Dictionary<cat_type, CatData> catDataMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        if(catDataMap == null)
        {
            catDataMap = new Dictionary<cat_type, CatData>();

            foreach (CatData info in data)
            {
                catDataMap[info.type] = info;
            }
        }

        else
        {
           
        }
    }

    public CatData GetCatData(cat_type type)
    {
        return catDataMap[type];
    }
}