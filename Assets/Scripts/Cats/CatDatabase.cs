using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDatabase : MonoBehaviour
{
    // Start is called before the first frame update

    [Serializable]
    public class CatData
    {
        public cat_type type;
        public GameObject model;
        public Cat script;
    }

    public static CatDatabase Instance;

    public List<CatData> data;
    private Dictionary<cat_type, CatData> mappedDatabase;

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
        mappedDatabase = new Dictionary<cat_type, CatData>();
        foreach(CatData dataInstance in data) 
        {
            mappedDatabase.Add(dataInstance.type, dataInstance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CatData GetCatData(cat_type catData_CatType)
    {
        return mappedDatabase[catData_CatType];
    }
}
