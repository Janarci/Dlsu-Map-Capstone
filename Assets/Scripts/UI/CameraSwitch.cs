using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera ARCamera;
    public Camera WorldCamera;

    // Start is called before the first frame update
    void Start()
    {
        
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


    public void ActivateWorldCamera()
    {
        
    }

    public void GoToMainMenu()
    {
        LoadScene.LoadMenuScene();
    }
}
