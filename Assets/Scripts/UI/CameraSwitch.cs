using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;

public class CameraSwitch : MonoBehaviour
{
    public Camera ARCamera;
    public Camera WorldCamera;
    public Vector3 initialPos;
    public Vector3 initialRot;

    public static CameraSwitch Instance;

    private void Awake()
    {
        //WaitForVuforia();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.EnableWorldCamera();
        WorldCamera = CameraManager.Instance.WorldCamera;
        WorldCamera.gameObject.transform.localPosition = initialPos;
        WorldCamera.gameObject.transform.localRotation = Quaternion.Euler(initialRot);

        //initialPos = WorldCamera.transform.localPosition;
        //initialRot = WorldCamera.transform.localRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera()
    {
        //if(ARCamera.gameObject.activeInHierarchy)
        //{
        //    ARCamera.gameObject.SetActive(false);
        //    WorldCamera.gameObject.SetActive(true);
        //}

        //else
        //{
        //    //ARCamera.gameObject.SetActive(true);
        //    //WorldCamera.gameObject.SetActive(false);
        //    if (ARCamera.GetComponent<VuforiaBehaviour>().enabled == false)
        //    {
        //        //WaitForVuforia();
        //    }

        //}

        CameraManager.Instance?.SwitchCamera();
        
    }

    async void WaitForVuforia()
    {
        ARCamera.GetComponent<VuforiaBehaviour>().enabled = true;
        await Task.Run(InitializeVuforia);
    }

    void InitializeVuforia()
    {
        VuforiaApplication.Instance.Initialize(FusionProviderOption.VUFORIA_FUSION_ONLY);
    }

    public void ResetWorldCamera()
    {
        WorldCamera.transform.localPosition = initialPos;

        WorldCamera.transform.localRotation = Quaternion.Euler(initialRot);
    }


    public void ActivateWorldCamera()
    {
        
    }

    public void GoToMainMenu()
    {
        LoadScene.LoadMenuScene();
    }
}
