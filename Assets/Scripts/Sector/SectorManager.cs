using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    public GameObject map;
    [SerializeField] private GameObject blockerTemplate;

    private List<Sector> sectorList = new List<Sector>();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnInitializeMap += InitalizeSectors;
        EventManager.OnMissionComplete += OnMissionComplete;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitalizeSectors()
    {
        for (int i = 1; i < map.transform.childCount; i++)
        {
            GameObject sectorObj = map.transform.GetChild(i).gameObject;
            Sector newSectorComponent = sectorObj.AddComponent<Sector>();
            newSectorComponent.SetSectorBlockerObj(blockerTemplate);

            int sectorID = sectorList.Count;
            newSectorComponent.Initialize(sectorID);

            sectorList.Add(newSectorComponent);
        }
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
                }
                break;

            case 1:
                {
                    UnlockSector(1);
                }
                break;

            case 2:
                {
                    UnlockSector(2);
                }
                break;
        }
    }
}
