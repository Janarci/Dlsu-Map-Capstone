using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuildingDatabase;

public class ChillSpaceDatabase : MonoBehaviour
{
    [SerializeField] private List<ChillSpace.Detail> chillspacesList;
    [SerializeField] private Dictionary<ChillSpace.Area, ChillSpace.Detail> mappedDatabase;

    public static ChillSpaceDatabase Instance;
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
        chillspacesList = new List<ChillSpace.Detail>();
        mappedDatabase = new Dictionary<ChillSpace.Area, ChillSpace.Detail>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChillspaceToDatabase(ChillSpace cs)
    {
        if(!mappedDatabase.ContainsKey(cs.detail.area))
        {
            chillspacesList.Add(cs.detail);
            mappedDatabase[cs.detail.area] = cs.detail;
        }
        
    }

    public ChillSpace.Detail GetDataInfo(ChillSpace.Area area)
    {
        return mappedDatabase[area];
    }

    public List<ChillSpace.Detail> GetDataList()
    {
        return chillspacesList;
    }
}
