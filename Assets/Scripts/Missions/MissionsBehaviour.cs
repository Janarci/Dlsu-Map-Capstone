using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsBehaviour : MonoBehaviour
{
    public List<GameObject> SectorList;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnMissionComplete += OnMissionComplete;
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
                }
                break;

            case 1:
                {
                    SectorList[1].transform.GetChild(0).gameObject.SetActive(false);
                }
                break;

            case 2:
                {
                    SectorList[2].transform.GetChild(0).gameObject.SetActive(false);
                }
                break;
        }
    }

    public void OnDestroy()
    {
        EventManager.Instance.OnMissionComplete -= OnMissionComplete;
    }
}   
