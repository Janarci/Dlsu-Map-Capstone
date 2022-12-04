using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    public GameObject map;
    [SerializeField] private GameObject blockerTemplate;
    public int[] DLSUTiles =
    {
        1, 3, 4, 6, 7, 10, 11
    };

    private List<Sector> sectorList;

    private float spawnCatInterval = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnInitializeMap += InitalizeSectors;
        EventManager.OnMissionComplete += OnMissionComplete;
        EventManager.OnCatClick += OnCatClickedInSector;
        sectorList = new List<Sector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitalizeSectors()
    {
        for (int i = 1; i < map.transform.childCount; i++)
        {
            if(DLSUTiles.Contains(i))
            {
                GameObject sectorObj = map.transform.GetChild(i).gameObject;
                Sector newSectorComponent = sectorObj.AddComponent<Sector>();
                newSectorComponent.SetSectorBlockerObj(blockerTemplate);

                int sectorID = sectorList.Count;
                newSectorComponent.Initialize(sectorID);

                sectorList.Add(newSectorComponent);
            }
            
        }

        for(int i = 0; i < Values.unlocked_sectors.Count;i++)
        {
            UnlockSector(Values.unlocked_sectors[i]);
        }

        StartCoroutine(GenerateCatsInSector());
    }

    private void UnlockSector(int sectorIndex)
    {
        sectorList[sectorIndex]?.Unlock();
    }

    void OnMissionComplete(int missionID)
    {
        switch (missionID)
        {
            case 0:
                {
                    UnlockSector(0);
                    if (!Values.unlocked_sectors.Contains(0))
                        Values.unlocked_sectors.Add(0);
                }
                break;

            case 1:
                {
                    UnlockSector(1);
                    if (!Values.unlocked_sectors.Contains(1))
                        Values.unlocked_sectors.Add(1);
                }
                break;

            case 2:
                {
                    UnlockSector(2);
                    if (!Values.unlocked_sectors.Contains(2))
                        Values.unlocked_sectors.Add(2);
                }
                break;
        }
    }

    private IEnumerator GenerateCatsInSector()
    {
        Debug.Log("here");
        while (true)
        {

            yield return new WaitForSeconds(spawnCatInterval);
            Debug.Log("here");
            int sectorIndex = UnityEngine.Random.Range(0, sectorList.Count - 1);
            Debug.Log(sectorList.Count - 1 + " " + sectorIndex);

            if (sectorList[sectorIndex].isUnlocked)
            {
                CatSpawnerUpdated csu = FindObjectOfType<CatSpawnerUpdated>();

                csu?.InstantiateDroid(sectorList[sectorIndex].gameObject.transform.position);
            }
            Debug.Log("here");

            Debug.Log(sectorIndex);
            

        }
    }

    private void OnCatClickedInSector(Cat clickedCat)
    {
        Values.approached_cat = GameObject.Instantiate(clickedCat.gameObject);
        Values.approached_cat.SetActive(false);
        GameObject.DontDestroyOnLoad(Values.approached_cat);
        int sectorCatIsOn = -1;

        Debug.Log("clicked in sectormap scene");
        foreach(Sector s in sectorList)
        {
            GameObject sectorPlane = s.gameObject;
            Vector3[] planeVertices = sectorPlane.GetComponent<MeshFilter>().sharedMesh.vertices;
            Rect sectRect = new Rect(sectorPlane.transform.position.x - (sectorPlane.GetComponent<Renderer>().bounds.size.x / 2),
                sectorPlane.transform.position.z - (sectorPlane.GetComponent<Renderer>().bounds.size.z / 2),
                (sectorPlane.GetComponent<Renderer>().bounds.size.x),
                (sectorPlane.GetComponent<Renderer>().bounds.size.z));

            //Debug.Log(s.getID().ToString() + " " + (sectorPlane.transform.position.x - (sectorPlane.GetComponent<Renderer>().bounds.size.x / 2)).ToString() + " " + clickedCat.gameObject.transform.position.x.ToString());
            //Debug.Log(sectRect.xMin.ToString() + " " + sectRect.xMax.ToString());
            if(sectRect.Contains(new Vector2(clickedCat.gameObject.transform.position.x, clickedCat.gameObject.transform.position.z)))
            {
                Debug.Log(s.getID());
                sectorCatIsOn = s.getID();
            }

            Values.belonging_sector = sectorCatIsOn;
        }

        if (sectorCatIsOn != -1)
            LoadScene.LoadCatBefriendingScene();
    }

    public void OnDestroy()
    {
        EventManager.OnCatClick -= OnCatClickedInSector;
    }
}
