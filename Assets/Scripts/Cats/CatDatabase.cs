using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatDatabase : MonoBehaviour
{
    // Start is called before the first frame update

    [Serializable]
    public class CatData
    {
        public string catTypeLabel;
        public CatType.Type type;
        public GameObject model;
        public Cat script;
        public Sprite icon;
    }

    public static CatDatabase Instance;

    public List<CatData> data;
    private Dictionary<CatType.Type, CatData> mappedDatabase;

    public bool isInitialized { get; private set; }

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
        isInitialized = false;
        //InitializeCatDatabase();
    }

    public void InitializeCatDatabase()
    {
        if(!isInitialized)
        {
            StartCoroutine(Initialize());
        }
    }

    IEnumerator Initialize()
    {
        int i = 0;
        mappedDatabase = new Dictionary<CatType.Type, CatData>();

        while(i < data.Count)
        {
            CatData dataInstance = data[i];
            mappedDatabase.Add(dataInstance.type, dataInstance);
            i++;
            //Debug.Log(i);
            yield return null;
        }

        isInitialized = true;
        //foreach (CatData dataInstance in data)
        //{
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CatData GetCatData(CatType.Type catData_CatType)
    {
        return mappedDatabase[catData_CatType];
    }
}
