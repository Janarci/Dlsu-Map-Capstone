using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public bool toDestroy = false;
        
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

        public virtual void Destroy()
        {
            isRunning = false;
            toDestroy = true;
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
            DataPersistenceManager.instance.gameData.runTime = elapsedTime;
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
        public ShuffleCatsTimer() : base(720, true)
        {

        }
        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }
        public override void Execute()
        {
            base.Execute();
            if(CatsList.instance.befriended_cats.Count > 4)
            {
                List<int> holders = new List<int> { -1, -1, -1, -1 };
                for(int i = 0; i < 4; i++)
                {
                    int rnd = 0;
                    do
                    {
                        rnd = UnityEngine.Random.Range(0, CatsList.instance.befriended_cats.Count - 1);
                    } while (holders.Contains(rnd));

                    CatsList.instance.selected_cats[i] = CatsList.instance.befriended_cats[rnd];
                    holders[i] = rnd;
                }
            }
        }
    }

    public class SpawnCatsTimer : Timer
    {
        public SpawnCatsTimer() : base(5, true)
        {

        }
        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }
        public override void Execute()
        {
            base.Execute();
            CatsList.instance.AddNewCatToQueue();
        }
    }

    public class ReplaceCatTimer : Timer
    {
        public ReplaceCatTimer() : base(25, true)
        {

        }
        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }
        public override void Execute()
        {
            base.Execute();
            Debug.Log("Executing replace cats");
            UpdateCats.Instance.CatHQStayRequest();
        }
    }
    public class UnbefriendedCatExistenceCountdown : Timer
    {
        public Cat cat
        {
            get; private set;
        }
        public UnbefriendedCatExistenceCountdown(Cat _cat) : base(150, false)
        {
            cat = _cat;
        }
        public override void RunTime(float _elapsedTime)
        {
            base.RunTime(_elapsedTime);
        }
        public override void Execute()
        {
            base.Execute();
            this.Destroy();
        }

        public override void Destroy()
        {
            base.Destroy();
            

            CatSpawnerUpdated csu = GameObject.FindObjectOfType<CatSpawnerUpdated>();
            if(csu)
            {
                csu.RemoveCatFromSpawnList(cat.gameObject);
            }

            if (CatsList.instance.stashed_cat_spawns.Contains(cat.gameObject))
                CatsList.instance.stashed_cat_spawns.Remove(cat.gameObject);

            GameObject.Destroy(cat.gameObject);
        }

        public void Disable()
        {
            base.Destroy();
        }
    }

    public static Timers Instance;
    private GameTimer gameTimer = null;
    private ShuffleCatsTimer shuffleCatsTimer = null;
    private SpawnCatsTimer spawnCatsTimer = null;
    private ReplaceCatTimer replaceCatTimer= null;

    public List<Timer> timers = new List<Timer>();
    public List<UnbefriendedCatExistenceCountdown> catDurationTimers = new List<UnbefriendedCatExistenceCountdown>();
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

        List<Timer> toDestroyList = new List<Timer>();
        foreach(Timer t in timers)
        {
            if (t.toDestroy)
            {
                toDestroyList.Add(t);
            }
        }

        foreach(Timer t in toDestroyList)
        {
            timers.Remove(t);
        }

        toDestroyList.Clear();
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

    public void StartCatSpawnTimer()
    {
        if (spawnCatsTimer == null)
        {
            spawnCatsTimer = new SpawnCatsTimer();
            Debug.LogWarning("NO SPAWN CATS TIMER " + timers.Count);
            timers.Add(spawnCatsTimer);
            Debug.LogWarning("ADDED SPAWN CATS TIMER " + timers.Count);
        }

        
    }

    public void StartReplaceCatTimer()
    {
        if (replaceCatTimer == null)
        {
            replaceCatTimer = new ReplaceCatTimer();
            //replaceCatTimer = new ReplaceCatTimer();
            Debug.LogWarning("NO SPAWN CATS TIMER " + timers.Count);
            timers.Add(replaceCatTimer);
            //Debug.LogWarning("ADDED SPAWN CATS TIMER " + timers.Count);
        }


    }
    public void AddChillspaceAreaCooldown(ChillSpace.Area area)
    {
        ChillspaceScanCooldown cscd = new ChillspaceScanCooldown(area);
        chillspaceCooldowns.Add(area, cscd);
        timers.Add(cscd);
    }

    public void StartCatDurationCountdown(Cat cat)
    {
        UnbefriendedCatExistenceCountdown ucec = new UnbefriendedCatExistenceCountdown(cat);
        catDurationTimers.Add(ucec);
        timers.Add(ucec);
    }

    public void EndCatDurationCountdown(Cat _cat)
    {
        foreach(UnbefriendedCatExistenceCountdown ucec in catDurationTimers)
        {
            if(ucec.cat == _cat)
            {
                ucec.Disable();
                break;
            }
        }
    }
}
