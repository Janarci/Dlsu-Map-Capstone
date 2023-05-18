using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    public GameObject map;
    //[SerializeField] private GameObject blockerTemplate;
    //public int[] DLSUTiles =
    //{
    //    1, 3, 4, 6, 7, 10, 11
    //};

    /* 0 = andrew
     * 1 = LS
     * 2 = Henry Sy
     */
    [SerializeField] private Dictionary<int, Sector> sectorList;
    public List<Sector> visualList;

    private float spawnCatInterval = 4.0f;

    public static SectorManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
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
        sectorList = new Dictionary<int, Sector>();
        isInitialized = false;
        visualList = new List<Sector>();
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

        while(!MapTracker.isMapInitialized)
        {
            yield return null;
        }

        foreach(Sector s in FindObjectsOfType<Sector>(true)) 
        {
            while(!(ChillSpacesManager.Instance.isInitialized))
            {
                yield return null;
            }

            visualList.Add(s);
            sectorList[s.getID()] = s;
            s.SetSectorBlockerObj(s.gameObject.transform.GetChild(0).gameObject);
            Debug.Log("adding sector with id: " + s.getID() + " to sector list");
            s.InitializeSector();
            UnlockSector(s.getID());
            yield return null;
        }
        

        for(int i = 0; i < DataPersistenceManager.instance.gameData.unlocked_sectors.Count;i++)
        {
            Debug.Log("Pre-emptively unlocking sector: " + i);
            UnlockSector(DataPersistenceManager.instance.gameData.unlocked_sectors[i]);
            //yield return null;
        }

        isInitialized = true;
        CatsList.instance.num_sectors = sectorList.Count;
        StartCoroutine(GenerateCatsInSector());
    }

    private void UnlockSector(int sectorIndex)
    {
        sectorList[sectorIndex]?.Unlock();
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

        if(sectorList.ContainsKey(missionID))
        {
            sectorList[missionID].DisplayTooltip();

            if (sectorList[missionID].isUnlocked == false)
            {
                UnlockSector(missionID);
            }

            
        }
        
    }

    private IEnumerator GenerateCatsInSector()
    {
        while (true)
        {
            

            if(CatsList.instance.queuedSpawns.Count != 0)
            {
                if (sectorList[CatsList.instance.queuedSpawns.Last().Value].isUnlocked)
                {
                    CatSpawnerUpdated csu = FindObjectOfType<CatSpawnerUpdated>();

                    Rect sectAreaRect = sectorList[CatsList.instance.queuedSpawns.Last().Value].GetAreaRect();
                    if (sectAreaRect != Rect.zero)
                        csu?.InstantiateDroid(CatsList.instance.queuedSpawns.Last().Key, sectorList[CatsList.instance.queuedSpawns.Last().Value].transform, sectorList[CatsList.instance.queuedSpawns.Last().Value].GetAreaRect());

                    Debug.Log("Spawning cat in sector " + CatsList.instance.queuedSpawns.Last().Value);
                }

                else
                    Debug.Log("Cant spawn cat in locked sector");

                CatsList.instance.queuedSpawns.RemoveAt(CatsList.instance.queuedSpawns.Count-1);
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
        if (sectorList[sectorIndex])
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

    public Sector GetSector(int i)
    {
        return sectorList[i];
    }

    public int GetSectorCount()
    {
        return sectorList.Keys.Count;
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

    public void OnDestroy()
    {
        //EventManager.OnInitializeMap -= InitializeSectors;
        EventManager.OnMissionComplete -= OnMissionComplete;
        //EventManager.OnCatClick -= OnCatClickedInSector;
    }
}

