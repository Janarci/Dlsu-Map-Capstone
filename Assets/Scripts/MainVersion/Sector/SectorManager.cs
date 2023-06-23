using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectorManager : MonoBehaviour, IDataPersistence
{
    public InitializeMapWithLocationProvider map;
    //[SerializeField] private GameObject blockerTemplate;
    //public int[] DLSUTiles =
    //{
    //    1, 3, 4, 6, 7, 10, 11
    //};

    /* 0 = andrew
     * 1 = LS
     * 2 = Henry Sy
     */
    [SerializeField] public Dictionary<Building.Type, Sector> sectorList { get; private set; }
    public List<Building.Type> unlockedSectors;

    private float spawnCatInterval = 4.0f;

    public static SectorManager Instance;
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
    public bool isInitialized { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //EventManager.OnInitializeMap += InitializeSectors;
        EventManager.OnMissionComplete += OnMissionComplete;
        //EventManager.OnCatClick += OnCatClickedInSector;
        EventManager.OnInitializeSector += OnInitalizeSector;
        EventManager.OnReleaseSector += OnReleaseSector;
        SceneManager.sceneLoaded += OnSceneLoaded;
        sectorList = new Dictionary<Building.Type, Sector>();
        isInitialized = false;
        unlockedSectors = new List<Building.Type>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSectors()
    {
        if(!isInitialized)
            StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        //for (int i = 1; i < map.transform.childCount; i++)
        //{
        //    if(DLSUTiles.Contains(i))
        //    {
        //        GameObject sectorObj = map.transform.GetChild(i).gameObject;
        //        Sector newSectorComponent = sectorObj.AddComponent<Sector>();
        //        newSectorComponent.SetSectorBlockerObj(blockerTemplate);

        //        int sectorID = sectorList.Count;
        //        newSectorComponent.Initialize(sectorID);

        //        sectorList.Add(newSectorComponent);
        //    }

        //}

        

        while (!(ChillSpacesManager.Instance.isInitialized))
        {
            Debug.Log("Chill spaces not initialized");
            yield return null;
        }

        InitializeMapWithLocationProvider.instance.StartCoroutine(InitializeMapWithLocationProvider.instance.Initialize());
        while (!(InitializeMapWithLocationProvider.instance.isInitialized))
        {
            Debug.Log("Chill spaces not initialized");
            yield return null;
        }

        //foreach (Sector s in FindObjectsOfType<Sector>(true))
        //{
        //    InitializeSectorIndividual(s);
        //    yield return null;
        //}


        //for(int i = 0; i < DataPersistenceManager.instance.gameData.unlocked_sectors.Count;i++)
        //{
        //    Debug.Log("Pre-emptively unlocking sector: " + i);
        //    UnlockSector(DataPersistenceManager.instance.gameData.unlocked_sectors[i]);
        //    //yield return null;
        //}

        isInitialized = true;
        StartCoroutine(GenerateCatsInSector());
    }

    private void OnInitalizeSector(Sector s, Building b)
    {
        

        if (!sectorList.ContainsKey(s.type))
        {
            sectorList[s.type] = s;
            s.SetSectorBlockerObj(s.gameObject.transform.GetChild(0).gameObject);
            Debug.Log("adding sector with id: " + s.getID() + " to sector list");
            s.Lock();
            //s.InitializeSector();

            if(unlockedSectors.Contains(s.type))
            {
                UnlockSector(s.type);
            }
            CatsManager.instance.num_sectors = sectorList.Keys.Count;

            //if(DataPersistenceManager.instance.isInitialized)
            //{
            //    if(DataPersistenceManager.instance.gameData.unlocked_sectors.Contains(s.type))
            //    {
            //        UnlockSector(s.type);
            //    }

            //    else
            //    {
            //        b.MakeTransparent();
            //    }
            //}
        }



    }

    private void OnReleaseSector(Sector s, Building b)
    {
        if(sectorList.ContainsKey(s.type))
        {
            sectorList.Remove(s.type);
            Debug.Log("removing sector with id: " + s.getID() + " from sector list");

        }

        CatsManager.instance.num_sectors = sectorList.Keys.Count;

    }

    //private void UnlockSector(int sectorIndex)
    //{
    //    sectorList[sectorIndex]?.Unlock();
    //}

    public void UnlockSector(Building.Type bldgType)
    {
        if(sectorList.ContainsKey(bldgType))
        {
            Debug.Log("unlockingt" + sectorList[bldgType].name);
            sectorList[bldgType].Unlock();
        }

        if (!unlockedSectors.Contains(bldgType))
        {
            unlockedSectors.Add(bldgType);
        }

    }

    void OnMissionComplete(int missionID)
    {
        //if (missionID >=0 && missionID <= 14)
        //{
        //    if (sectorList[missionID].isUnlocked == false)
        //    {
        //        UnlockSector(missionID);
        //        if (!Values.unlocked_sectors.Contains(missionID))
        //        {
        //            sectorList[missionID].DisplayTooltip();
        //            Values.unlocked_sectors.Add(missionID);
        //        }
        //    }
        //}

        //if(sectorList.ContainsKey(missionID))
        //{
        //    sectorList[missionID].DisplayTooltip();

        //    if (sectorList[missionID].isUnlocked == false)
        //    {
        //        UnlockSector(missionID);
        //    }

            
        //}
        
    }

    private IEnumerator GenerateCatsInSector()
    {
        while (true)
        {
            

            if(CatsManager.instance.queuedSpawns.Count != 0)
            {
                Building.Type sctr = (Building.Type)CatsManager.instance.queuedSpawns.Last().Value;
                if(sectorList.ContainsKey(sctr))
                {
                    if (sectorList[sctr].isUnlocked)
                    {
                        CatSpawnerUpdated csu = FindObjectOfType<CatSpawnerUpdated>();

                        Rect sectAreaRect = sectorList[sctr].GetAreaRect();
                        if (sectAreaRect != Rect.zero)
                            CatsManager.instance.SpawnCat(sectorList[sctr].transform, sectorList[sctr].GetAreaRect());

                        Debug.Log("Spawning cat in sector " + CatsManager.instance.queuedSpawns.Last().Value);
                    }

                    else
                        Debug.Log("Cant spawn cat in locked sector");
                }

                else
                {
                    Debug.Log("Sector list doesnt contain key: " + sctr);
                }
                

                CatsManager.instance.queuedSpawns.RemoveAt(CatsManager.instance.queuedSpawns.Count-1);
            }

            yield return null;
            //yield return new WaitForSeconds(spawnCatInterval);
            //Debug.Log("here");
            //int sectorIndex = UnityEngine.Random.Range(0, sectorList.Count);
            //Debug.Log(sectorList.Count + " " + sectorIndex);

            //if (sectorList[sectorIndex].isUnlocked)
            //{
            //    CatSpawnerUpdated csu = FindObjectOfType<CatSpawnerUpdated>();

            //    Rect sectAreaRect = sectorList[sectorIndex].GetAreaRect();
            //    if(sectAreaRect != Rect.zero)
            //        csu?.InstantiateDroid(sectorList[sectorIndex].transform, sectorList[sectorIndex].GetAreaRect());
            //}
            //Debug.Log("here");

            //Debug.Log(sectorIndex);
            

        }
    }
    
    public void SpawnCatsInSector(int sectorIndex)
    {
        Building.Type sectorType = (Building.Type)sectorIndex;
        if (sectorList[sectorType])
        {
            //if (sectorList[sectorIndex].isUnlocked)
            //{
            //    CatSpawnerUpdated csu = FindObjectOfType<CatSpawnerUpdated>();

            //    Rect sectAreaRect = sectorList[sectorIndex].GetAreaRect();
            //    if (sectAreaRect != Rect.zero)
            //        csu?.InstantiateDroid(sectorList[sectorIndex].transform, sectorList[sectorIndex].GetAreaRect());
            //}
           

        }
    }

    public Sector GetSector(Building.Type bldgType)
    {
        return sectorList[bldgType];
    }

    public int GetSectorCount()
    {
        return sectorList.Keys.Count;
    }

    public void ClaimItemFromSector(Building.Type _type)
    {
        sectorList[_type].ClaimItem();
    }
    //private void OnCatClickedInSector(Cat clickedCat)
    //{

    //    int sectorCatIsOn = -1;

    //    Debug.Log("clicked in sectormap scene");
    //    foreach(Sector s in sectorList.Values)
    //    {
    //        Debug.Log("Sector id:" + s.getID());
    //        GameObject sectorBounds = s.transform.GetChild(0).gameObject;
    //        Vector3[] planeVertices = sectorBounds.GetComponent<MeshFilter>().sharedMesh.vertices;
    //        Rect sectRect = s.GetAreaRect();

    //        //Debug.Log(s.getID().ToString() + " " + (sectorPlane.transform.position.x - (sectorPlane.GetComponent<Renderer>().bounds.size.x / 2)).ToString() + " " + clickedCat.gameObject.transform.position.x.ToString());
    //        //Debug.Log(sectRect.xMin.ToString() + " " + sectRect.xMax.ToString());
    //        if(sectRect.Contains(new Vector2(clickedCat.gameObject.transform.position.x, clickedCat.gameObject.transform.position.z),true))
    //        {

    //        }

    //        Vector2 cat2Dpos = new Vector2(clickedCat.gameObject.transform.position.x, clickedCat.gameObject.transform.position.z);

    //        if(s.IsPointWithinSector(clickedCat.transform.position))
    //        {
    //            Debug.Log(s.getID());
    //            sectorCatIsOn = s.getID();
    //            Values.approached_cat = GameObject.Instantiate(clickedCat.gameObject);
    //            Values.approached_cat.SetActive(false);
    //            GameObject.DontDestroyOnLoad(Values.approached_cat);
    //        }

    //        Values.belonging_sector = sectorCatIsOn;
    //    }

    //    if (sectorCatIsOn != -1)
    //        LoadScene.LoadCatBefriendingScene();
    //}
    public void LoadGameData(GameData gameData)
    {
        foreach(Building.Type b in gameData.unlocked_sectors)
        {
            UnlockSector(b);
        }
    }

    public void SaveGameData(ref GameData gameData)
    {
        foreach(Building.Type b in unlockedSectors)
        {
            
            if(!gameData.unlocked_sectors.Contains(b))
            {
                gameData.unlocked_sectors.Add(b);
            }
            
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isInitialized = false;
    }

public void OnDestroy()
    {
        //EventManager.OnInitializeMap -= InitializeSectors;
        EventManager.OnMissionComplete -= OnMissionComplete;
        //EventManager.OnCatClick -= OnCatClickedInSector;
        EventManager.OnInitializeSector -= OnInitalizeSector;
    }

    
}
