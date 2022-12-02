using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission : MonoBehaviour
{
    [SerializeField] private int id = 0;
    private MissionTarget target = null;
    [SerializeField] GameObject ui = null;

    private bool isMissionComplete = false;

    public int getId()
    {
        return id;
    }

    public MissionTarget getTarget()
    {
        return target;
    }

    void Start()
    {
        
    }

    public void CompleteMission()
    {
        isMissionComplete = true;
        EventManager.MissionComplete(this.id);
        DisplayDesignatedUI();
        //EventManager.Instance.MissionTargetDetected(this.id, true);
        Debug.Log("Mission " + id + " complete");
    }

    public void OnTargetLost()
    {

    }

    public void DisplayDesignatedUI()
    {
        ui?.SetActive(true);
    }

    public bool IsMissionComplete()
    {
        return isMissionComplete;
    }


}
