using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    public static DataPersistenceManager instance { get; private set; }

    private List<IDataPersistence> dataPersistenceList;

    public GameData gameData;

    private FileDataHandler dataHandler;

    public bool isInitialized = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.dataPersistenceList = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    public void InitializeGameData()
    {
        LoadGame();
        isInitialized = true;
    }
    
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {

        this.gameData = dataHandler.Load();
        if(this.gameData == null)
        {
            NewGame(); 
        }

        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.LoadGameData(gameData);
        }

    }

    public void SaveGame()
    {
        foreach(IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.SaveGameData(ref gameData);
        }

        dataHandler.Save(gameData);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
