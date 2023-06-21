using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQCameraMovement : MonoBehaviour
{

    [SerializeField] GameObject hqCamera;

    private float zoomTvalue = 0;
    private Vector3 cameraInitialPos;
    private Quaternion cameraInitialRot;

    private GameObject focusedCat;

    public bool isZoomingOnCat { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        if (CameraManager.Instance != null)
        {
            
            //hqCamera.SetActive(true);
            //hqCamera.transform.position = new Vector3(9.89999962f, 27.8999996f, -44.1399994f);
            //hqCamera.transform.rotation = new Quaternion(0.204496101f, 0, 0, 0.978867352f);

            CameraManager.Instance.HQCamera.CopyFrom(hqCamera.GetComponent<Camera>());
            Destroy(hqCamera);
            hqCamera = CameraManager.Instance.HQCamera.gameObject;
            hqCamera.SetActive(true);
        }

        cameraInitialPos = hqCamera.transform.position;
        cameraInitialRot = hqCamera.transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtCat(GameObject _cat)
    {

        isZoomingOnCat = true;
        focusedCat = _cat;

    
    }

    private void CameraFocusOnCat()
    {
        //Debug.Log(Time.deltaTime);
        zoomTvalue += Time.deltaTime * 0.8f;

        Vector3 catFrontPosition = new Vector3(focusedCat.transform.position.x, focusedCat.transform.position.y + 12.15f, focusedCat.transform.position.z - 26.5f);

        Vector3 cameraNewPosition = Vector3.Lerp(hqCamera.transform.position, catFrontPosition, zoomTvalue);
        hqCamera.transform.position = cameraNewPosition;


        if (zoomTvalue >= 1.0f)
        {
            zoomTvalue = 0.0f;
            isZoomingOnCat = false;
            hqCamera.transform.LookAt(new Vector3(focusedCat.transform.position.x, focusedCat.transform.position.y + 10, focusedCat.transform.position.z));
            focusedCat.transform.LookAt(new Vector3(hqCamera.transform.position.x, focusedCat.transform.position.y, hqCamera.transform.position.z));
            //focusedCat.ui.transform.LookAt(camera.transform.position * -1);
            //focusedCat.ui.transform.localRotation = Quaternion.EulerAngles(focusedCat.ui.transform.localPosition.x + 45.0f, focusedCat.ui.transform.localPosition.x + 180.0f, focusedCat.ui.transform.localPosition.z);


        }

    }

    public void ResetCamera()
    {
        hqCamera.transform.position = cameraInitialPos;
        hqCamera.transform.rotation = cameraInitialRot;
        isZoomingOnCat = false;



        Debug.Log("Restting camera");
    }

    void HandleTouch()
    {
        Debug.Log("touch");

        float zoomFactor = 0.0f;
        //pinch to zoom. 
        if (Input.touchCount >= 1)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);


            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            zoomFactor = 0.0015f * (touchDeltaMag - prevTouchDeltaMag);


            var turnAngleF1 = Vector2.Angle(touchZero.position, touchZero.deltaPosition);
            var turnAngleF2 = Vector2.Angle(touchOne.position, touchOne.deltaPosition);

            var prevDir = touchOnePrevPos - touchZeroPrevPos;
            var currDir = touchOne.position - touchZero.position;

            //var turnAngle = (turnAngleF1 + turnAngleF2) / 2;
            var angle = Vector2.SignedAngle(prevDir, currDir);

            //Debug.Log(angle);

            hqCamera.transform.position += new Vector3(System.Math.Clamp(touchZero.deltaPosition.x, -300, 300), 0, 0).normalized;

        }
    }

    void HandleMouseAndKeyBoard()
    {
        //this.transform.LookAt(PlayerTarget.transform);// constant look at player cahracter

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Moving hq camera with mouse");
            //var mousePosition = Input.mousePosition;
            //mousePosition.z = hqCamera.transform.localPosition.y;
            //Vector3 _delta = hqCamera.ScreenToWorldPoint(mousePosition) - hqCamera.transform.localPosition;
            //_delta.y = 0f;
            float x = Input.GetAxis("Mouse X");
            Debug.Log(new Vector3(x, 0, 0).normalized);
            hqCamera.transform.position += new Vector3(x, 0, 0).normalized;
            hqCamera.transform.position = new Vector3(System.Math.Clamp(hqCamera.transform.position.x, transform.position.x -50, transform.position.x + 50), hqCamera.transform.position.y, hqCamera.transform.position.z);

        }


    }

    void LateUpdate()
    {
        if(isZoomingOnCat)
        {
            CameraFocusOnCat();
        }

        else if(hqCamera!= null)
        {
            if (Input.touchSupported && Input.touchCount > 0)
            {
                HandleTouch();
            }
            else
            {
                HandleMouseAndKeyBoard();
            }
        }

        
    }
}
