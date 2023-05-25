using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            

        else
            Destroy(this);
    }
    void Start()
    {
        isInitialized = false;
        //Initialize();

    }


    public void InitializeMissionsManager()
    {
        if(!isInitialized)
        {
            StartCoroutine(Initialize());
        }
    }
    private IEnumerator Initialize()
    {
        missionList = new Dictionary<int, Mission>();
        //EventManager.Instance.OnMissionTargetDetected += OnTargetFound;
        //GameObject[] missionListObj = GameObject.FindGameObjectsWithTag("MissionTarget");

        //if (missionListObj.Length == 0)
        //    Debug.Log("missions list is empty");
        //foreach (GameObject missionObj in missionListObj)
        //{
        //    if (missionObj.TryGetComponent(out ObserverBehaviour ob))
        //    {
        //        ob.OnTargetStatusChanged += OnTargetStatusChanged;
        //    }
        //    if (missionObj.TryGetComponent(out Mission mission))
        //    {
        //        missionList.Add(mission.getId(), mission);
        //    }
        //    //Debug.Log("Adding mission " + missionObj.name + " to list of missions");
        //}

        Mission[] missions = FindObjectsOfType<Mission>(true);

        foreach (Mission _mission in missions)
        {
            GameObject missionObj = _mission.gameObject;
            if (missionObj.TryGetComponent(out ObserverBehaviour ob))
            {
                ob.OnTargetStatusChanged += OnTargetStatusChanged;
            }
            //if (missionObj.TryGetComponent(out Mission mission))
            //{
            //    missionList.Add(mission.getId(), mission);
            //}
            if(!missionList.ContainsKey(_mission.getId()))
                missionList.Add(_mission.getId(), _mission);

            else
                Debug.Log(missionList[_mission.getId()].gameObject.name + " | " + _mission.gameObject.name);


            Debug.Log("Adding mission " + missionObj.name + " to list of missions");
            yield return null;
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

    public void OnDestroy()
    {
        //Instance = null;

        if(missionList != null)
        {
            foreach (Mission _mission in missionList.Values)
            {
                GameObject missionObj = _mission.gameObject;
                if (missionObj.TryGetComponent(out ObserverBehaviour ob))
                {
                    ob.OnTargetStatusChanged -= OnTargetStatusChanged;
                }


                Debug.Log("removing mission " + missionObj.name + " to list of missions");
            }
        }
        
    }
}
