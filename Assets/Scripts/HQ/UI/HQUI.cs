using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HQUI : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject canvas;

    private Cat focusedCat;
    private float zoomTvalue = 0;
    private Vector3 cameraInitialPos;
    private bool isZoomingOnCat;

    // Start is called before the first frame update
    void Start()
    {
        cameraInitialPos = camera.transform.position;
        EventManager.OnCatClick += OnCatSelect;
    }

    // Update is called once per frame
    void Update()
    {
        if(isZoomingOnCat)
        {
            CameraFocusOnCat(focusedCat.gameObject);
        }

        if(focusedCat != null)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                UnselectCat();
            }
        }
    }

    private void OnCatSelect(Cat selectedCat)
    {
        focusedCat = selectedCat;
        isZoomingOnCat = true;
    }

    private void UnselectCat()
    {
        focusedCat = null;
        ResetCamera();
    }
    private void CameraFocusOnCat(GameObject selectedCat)
    {
        //Debug.Log(Time.deltaTime);
        zoomTvalue += Time.deltaTime * 0.8f;

        Vector3 catFrontPosition = new Vector3(selectedCat.transform.position.x, selectedCat.transform.position.y + 5, selectedCat.transform.position.z - 5);
        Vector3 cameraNewPosition = Vector3.Lerp(camera.transform.position, catFrontPosition, zoomTvalue);
        camera.transform.position = cameraNewPosition;
        
        if (zoomTvalue >= 1.0f)
        {
            zoomTvalue = 0.0f;
            isZoomingOnCat = false;
        }

    }

    private void ResetCamera()
    {
        camera.transform.position = cameraInitialPos;
        isZoomingOnCat = false;


        Debug.Log("Restting camera");
        Debug.Log(camera.transform.position);


    }
    private void OnDestroy()
    {
        EventManager.OnCatClick -= OnCatSelect;
    }
}
