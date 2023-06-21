using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsBehaviour : MonoBehaviour
{
    public List<GameObject> SectorList;
    public List<ChillSpace> chillSpacesList;
    // Start is called before the first frame update
    void Start()
    {
        //EventManager.OnMissionComplete += OnMissionComplete;
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMissionComplete(int missionID)
    {
        switch(missionID)
        {
            case 11:
                {
                    SectorList[missionID].GetComponent<Sector>().Unlock();
                    SectorList[missionID].transform.GetChild(0).gameObject.SetActive(false);
                    //Debug.Log(SectorList[missionID].gameObject.name); 

                    //chillSpacesList[0]?.Unlock();
                }
                break;

            case 29:
                {
                    chillSpacesList[missionID-12]?.GiveItem();
                    //Debug.Log("Chill space");

                }
                break;
            default:
                Debug.Log("no mission with id: " + missionID);
                break;
        }
    }

    public void OnDestroy()
    {
        //EventManager.OnMissionComplete -= OnMissionComplete;
    }
}   
