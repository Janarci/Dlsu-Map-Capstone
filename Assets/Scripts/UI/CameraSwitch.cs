using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera ARCamera;
    public Camera WorldCamera;
    private Vector3 initialPos;
    private Quaternion initialRot;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = WorldCamera.transform.localPosition;
        initialRot = WorldCamera.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera()
    {
        if(ARCamera.gameObject.activeInHierarchy)
        {
            ARCamera.gameObject.SetActive(false);
            WorldCamera.gameObject.SetActive(true);
        }

        else
        {
            ARCamera.gameObject.SetActive(true);
            WorldCamera.gameObject.SetActive(false);
        }
        
    }

    public void ResetWorldCamera()
    {
        WorldCamera.transform.localPosition = initialPos;

        WorldCamera.transform.localRotation = initialRot;
    }


    public void ActivateWorldCamera()
    {
        
    }

    public void GoToMainMenu()
    {
        LoadScene.LoadMenuScene();
    }
}
