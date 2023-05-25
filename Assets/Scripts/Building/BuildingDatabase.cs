using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDatabase : MonoBehaviour
{
    [Serializable]
    public class BuildingData
    {
        public Building.Type type;
        public string name;
        [TextAreaAttribute]
        public string description;
        public List<ChillSpace.Area> chillspaces;
        public Sprite picture;
    }

    public static BuildingDatabase Instance;
    public List<BuildingData> data;
    public Dictionary<Building.Type, BuildingData> mappedDatabase;

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
        //EventManager.OnInitializeSector += AddBuildingToDatabase;
        isInitialized = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBuildingDatabase()
    {
        if (!isInitialized)
        {
            StartCoroutine(Initialize());
        }
    }

    IEnumerator Initialize()
    {
        int i = 0;
        mappedDatabase = new Dictionary<Building.Type, BuildingData>();

        while (i < data.Count)
        {
            BuildingData dataInstance = data[i];
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

    public void AddBuildingToDatabase(Sector sctr, Building bldg)
    {
        if(!(mappedDatabase.ContainsKey(bldg.SetData())))
        {
            string name = ""; string about = "";
            Building.Type type = bldg.SetData();
            name = bldg.buildingName;
            about = bldg.buildingAbout;
            BuildingData data = new BuildingData();
            data.type = type;
            data.name = name;
            data.description = about;
            //data.chillspaces = sctr.GetChillSpaces();
            data.picture = bldg.buildingPic;

            mappedDatabase[type] = data;
            this.data.Add(data);

            //Debug.Log("Added building: " + name);
        }
        
    }

    public BuildingData GetDataInfo(Building.Type bldgType)
    {
        return mappedDatabase[bldgType];
    }
}
