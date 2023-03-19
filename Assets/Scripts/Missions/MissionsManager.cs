using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MissionsManager : MonoBehaviour
{
    public static MissionsManager Instance;
    [SerializeField] Dictionary<int, Mission> missionList;

    public bool isInitialized { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(this);
    }
    void Start()
    {
        isInitialized = false;
        InitializeMissionsManager();

    }


    void InitializeMissionsManager()
    {
        StartCoroutine(Initialize());
    }
    private IEnumerator Initialize()
    {
        missionList = new Dictionary<int, Mission>();
        //EventManager.Instance.OnMissionTargetDetected += OnTargetFound;
        GameObject[] missionListObj = GameObject.FindGameObjectsWithTag("MissionTarget");

        if (missionListObj.Length == 0)
            Debug.Log("missions list is empty");
        foreach (GameObject missionObj in missionListObj)
        {
            if (missionObj.TryGetComponent(out ObserverBehaviour ob))
            {
                ob.OnTargetStatusChanged += OnTargetStatusChanged;
            }
            if (missionObj.TryGetComponent(out Mission mission))
            {
                missionList.Add(mission.getId(), mission);
            }
            yield return null;
            //Debug.Log("Adding mission " + missionObj.name + " to list of missions");
        }

        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTargetStatusChanged(ObserverBehaviour Target, TargetStatus targetStatus)
    {
        if(targetStatus.Status == Status.TRACKED)
        {
            
            OnTargetFound(Target.gameObject);
        }
    }
    private void OnTargetFound(GameObject missionTarget)
    {
        Debug.Log("found");
        Mission mission = missionTarget.GetComponent<Mission>();
        bool isMissionComplete = mission.IsMissionComplete();
        if (!isMissionComplete)
        {
            mission.CompleteMission();
        }
    }

    public void AddMission(Mission m)
    {
        missionList.Add(m.getId(), m);
    }
}
