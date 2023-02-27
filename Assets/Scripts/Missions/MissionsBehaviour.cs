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
        EventManager.OnMissionComplete += OnMissionComplete;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMissionComplete(int missionID)
    {
        switch(missionID)
        {
            case 0:
                {
                    SectorList[0].transform.GetChild(0).gameObject.SetActive(false);
                    chillSpacesList[0]?.Unlock();
                    chillSpacesList[1]?.Unlock();
                    chillSpacesList[2]?.Unlock();


                }
                break;

            case 1:
                {
                    SectorList[1].transform.GetChild(0).gameObject.SetActive(false);
                    chillSpacesList[3]?.Unlock();
                    chillSpacesList[4]?.Unlock();
                    chillSpacesList[5]?.Unlock();

                }
                break;

            case 2:
                {
                    SectorList[2].transform.GetChild(0).gameObject.SetActive(false);
                    chillSpacesList[6]?.Unlock();
                    chillSpacesList[7]?.Unlock();
                    chillSpacesList[8]?.Unlock();

                }
                break;
            case 3:
                {
                    if (chillSpacesList[0] && !chillSpacesList[0].isLocked)
                    {
                        chillSpacesList[0].GiveItem();
                    }
                }
                break;
        }
    }

    public void OnDestroy()
    {
        EventManager.OnMissionComplete -= OnMissionComplete;
    }
}   
