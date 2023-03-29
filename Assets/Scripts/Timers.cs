using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChillSpace;

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
            //Debug.Log(elapsedTime);

            if (elapsedTime >= endTime)
            {
                Execute();

                if (!restartTimer)
                {
                    isRunning = false;
                    elapsedTime = endTime;

                }

                else
                {
                    elapsedTime = 0;
                }

            }
        }

        public virtual void Execute()
        {

        }

        public float TimeRemaining()
        {
            return (endTime - elapsedTime);
        }

        public void StartTimer()
        {
            if (elapsedTime == endTime)
            {
                isRunning = true;
                elapsedTime = 0;
            }
                
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
        public ChillspaceScanCooldown(ChillSpace.Area _area) : base(15, false)
        {
            area = _area;
            Debug.Log(area);

        }

        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }
        public override void Execute()
        {
            base.Execute();
            ChillSpacesManager.Instance?.EndChillspaceCooldown(area);
        }


    }

    public class ShuffleCatsTimer : Timer
    {
        public ShuffleCatsTimer() : base(20, true)
        {

        }
        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }
        public override void Execute()
        {
            base.Execute();
            if(Values.befriended_cats.Count > 4)
            {
                List<int> holders = new List<int> { -1, -1, -1, -1 };
                for(int i = 0; i < 4; i++)
                {
                    int rnd = 0;
                    do
                    {
                        rnd = UnityEngine.Random.Range(0, Values.befriended_cats.Count - 1);
                    } while (holders.Contains(rnd));

                    Values.selected_cats[i] = Values.befriended_cats[rnd];
                    holders[i] = rnd;
                }
            }
        }
    }

    public static Timers Instance;
    private GameTimer gameTimer = null;
    private ShuffleCatsTimer shuffleCatsTimer = null;

    public List<Timer> timers = new List<Timer>();
    public Dictionary<ChillSpace.Area, ChillspaceScanCooldown> chillspaceCooldowns;

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
        chillspaceCooldowns = new Dictionary<ChillSpace.Area, ChillspaceScanCooldown>();

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

    public void StartCatShuffleTimer()
    {
        if(shuffleCatsTimer == null)
        {
            shuffleCatsTimer = new ShuffleCatsTimer();
            timers.Add(shuffleCatsTimer);
        }
    }
    public void AddChillspaceAreaCooldown(ChillSpace.Area area)
    {
        ChillspaceScanCooldown cscd = new ChillspaceScanCooldown(area);
        chillspaceCooldowns.Add(area, cscd);
        timers.Add(cscd);
    }
}
