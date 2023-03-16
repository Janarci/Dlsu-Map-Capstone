using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{
    [Serializable]
    public class Timer
    {
        public float elapsedTime;
        public float endTime;
        public bool restartTimer;
        public bool isRunning = true;
        
        public Timer(float duration, bool isRestart)
        {
            endTime= duration;
            restartTimer = isRestart;
        }
        public virtual void RunTime(float _elapsedTime)
        {
            elapsedTime += _elapsedTime;
            Debug.Log(elapsedTime);

            if (elapsedTime >= endTime)
            {
                Execute();
                elapsedTime= 0;

                if (!restartTimer)
                {
                    isRunning = false;
                }

            }
        }

        public virtual void Execute()
        {

        }
    }

    public class GameTimer : Timer
    {
        public GameTimer() : base(1200, false)
        {
            
        }

        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }

        public override void Execute()
        {
            base.Execute();
            Values.runTime = elapsedTime;
            LoadScene.LoadEndScene();
        }
    }
    public class ChillspaceScanCooldown : Timer
    {
        private ChillSpace.Area area;
        public ChillspaceScanCooldown(ChillSpace.Area _area) : base(15, true)
        {
            area = _area;
            Debug.Log(area);

        }

        public override void Execute()
        {
            base.Execute();
            ChillSpacesManager.Instance?.EndChillspaceCooldown(area);
        }


    }

    public static Timers Instance;
    public GameTimer gameTimer = null;
    public List<Timer> timers = new List<Timer>();
    public List<ChillspaceScanCooldown> chillspaceCooldowns;

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
    void Start()
    {
        timers = new List<Timer>();
        chillspaceCooldowns = new List<ChillspaceScanCooldown>();   
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Timer t in timers)
        {
            if(t.isRunning)
                t.RunTime(Time.deltaTime);
        }
    }
    
    public void StartGameTimer()
    {
        if(gameTimer == null)
        {
            gameTimer = new GameTimer();
            timers.Add(gameTimer);
        }
        
    }

    public void AddChillspaceAreaCooldown(ChillSpace.Area area)
    {
        ChillspaceScanCooldown cscd = new ChillspaceScanCooldown(area);
        chillspaceCooldowns.Add(cscd);
        timers.Add(cscd);
    }
}
