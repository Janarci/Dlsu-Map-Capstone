using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
public class CameraManager : MonoBehaviour
{
    public Camera ARCamera;
    public Camera WorldCamera;

    public static CameraManager Instance;

    public bool isInitialized { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //InitializeARCamera();
        isInitialized = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
        VuforiaApplication.Instance.OnVuforiaStopped += OnVuforiaStopped;
        EnableARCamera();
        InitializeARCamera();
        EnableWorldCamera();

        isInitialized = true;
    }

    private void InitializeARCamera()
    {
        ARCamera.gameObject.GetComponent<VuforiaBehaviour>().enabled = true;
        ARCamera.gameObject.GetComponent<DefaultInitializationErrorHandler>().enabled = true;

        VuforiaApplication.Instance.Initialize();
    }

    void Update()
    {

    }

    public void EnableWorldCamera()
    {
        WorldCamera.gameObject.SetActive(true);
        ARCamera.gameObject.SetActive(false);
    }

    public void EnableARCamera()
    {
        WorldCamera.gameObject.SetActive(false);
        ARCamera.gameObject.SetActive(true);
        //if (!(VuforiaApplication.Instance.IsInitialized))
        //{
        //    InitializeARCamera();
        //}
        
    }

    public void SwitchCamera()
    {
        if (ARCamera.gameObject.activeInHierarchy)
        {
            EnableWorldCamera();
        }

        else
        {
            EnableARCamera();
        }
    }

    public void DisableCameras()
    {
        ARCamera.gameObject.SetActive(false);
        WorldCamera.gameObject.SetActive(false);
    }

    public void SetWorldCameraPosition(Vector3 pos)
    {
        WorldCamera.transform.position = pos;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DisableCameras();
    }

    void OnVuforiaStopped()
    {
        Debug.Log("vuforia stopped on its own");
    }

}
