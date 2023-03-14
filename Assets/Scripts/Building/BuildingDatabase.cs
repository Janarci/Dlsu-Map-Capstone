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
        public string description;
        public List<ChillSpace.Area> chillspaces;
        public Sprite icon;
    }

    public static BuildingDatabase Instance;
    public List<BuildingData> data;
    public Dictionary<Building.Type, BuildingData> mappedDatabase;

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
        mappedDatabase = new Dictionary<Building.Type, BuildingData>();
        EventManager.OnInitializeSector += AddBuildingToDatabase;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            data.chillspaces = sctr.GetChillSpaces();

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
