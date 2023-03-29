using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BuildingDatabase;

public class ChillSpaceDatabase : MonoBehaviour
{
    //[SerializeField] private List<ChillSpace.Detail> chillspacesList;
    [SerializeField] private Dictionary<ChillSpace.Area, ChillSpace.Detail> chillspacesList;

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
        //chillspacesList = new List<ChillSpace.Detail>();
        chillspacesList = new Dictionary<ChillSpace.Area, ChillSpace.Detail>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChillspaceToDatabase(ChillSpace cs)
    {
        if(!chillspacesList.ContainsKey(cs.detail.area))
        {
            //chillspacesList.Add(cs.detail);
            chillspacesList[cs.detail.area] = cs.detail;
        }
        
    }

    public ChillSpace.Detail GetDataInfo(ChillSpace.Area area)
    {
        return chillspacesList[area];
    }

    public List<ChillSpace.Area> GetDataList()
    {
        return chillspacesList.Keys.ToList() ;
    }
}
