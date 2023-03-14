namespace Mapbox.Examples
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Mapbox.Unity.Map;
	using System;

	public class CameraMovement : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		float _panSpeed = 12.5f;

		[SerializeField]
		float _zoomSpeed = 50f;

		[SerializeField]
		Camera _referenceCamera;

		[SerializeField]
		GameObject PlayerTarget; // reference for player in game

		Quaternion _originalRotation;
		Vector3 _origin;
		Vector3 _delta;
		bool _shouldDrag;

		void HandleTouch()
		{
            Debug.Log("touch");

            float zoomFactor = 0.0f;
			//pinch to zoom. 
			switch (Input.touchCount)
			{
				case 1:
					{
						HandleMouseAndKeyBoard();
					}
					break;
				case 2:
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
						zoomFactor = 0.0008f * (touchDeltaMag - prevTouchDeltaMag);


                        var turnAngleF1 = Vector2.Angle(touchZero.position, touchZero.deltaPosition);
                        var turnAngleF2 = Vector2.Angle(touchOne.position, touchOne.deltaPosition);

                        var prevDir = touchOnePrevPos - touchZeroPrevPos;
                        var currDir = touchOne.position - touchZero.position;

                        //var turnAngle = (turnAngleF1 + turnAngleF2) / 2;
                        var angle = Vector2.SignedAngle(prevDir, currDir);

                        Debug.Log(angle);
                        transform.Rotate(0, 0, angle * 1.5f);
                        ZoomMapUsingTouchOrMouse(zoomFactor);
                        
                    }

					

					break;
				default:
					break;
			}
		}

		void ZoomMapUsingTouchOrMouse(float zoomFactor)
		{
			Vector3 newPos = Vector3.zero;
			var y = zoomFactor * _zoomSpeed;

			transform.localPosition += (transform.forward * y);
			//if(transform.localPosition.x > 0)
			//	newX = Mathf.Min(transform.localPosition.x, 100.0f);
			//else
   //             newX = Mathf.Max(transform.localPosition.x, -100.0f);
			newPos.x = Math.Clamp(transform.localPosition.x, -50, 50);
            newPos.y = Math.Clamp(transform.localPosition.y, 55, 80);
            newPos.z = Math.Clamp(transform.localPosition.z, -50, 50);


			//if (transform.localPosition.y > 0)
			//    newY = Mathf.Min(transform.localPosition.y, 75.0f);
			//else
			//    newY = Mathf.Max(transform.localPosition.y, -75.0f);

			//if (transform.localPosition.z > 0)
			//    newZ = Mathf.Min(transform.localPosition.z, 75.0f);
			//else
			//    newZ = Mathf.Max(transform.localPosition.z, -75.0f);
			transform.localPosition = newPos;
			Debug.Log(transform.localPosition);
		}

        //void RotateMapUsingTouchOrMouse(float rotFactor)
        //{
        //    var y = zoomFactor * _zoomSpeed;
        //    transform.localPosition += (transform.forward * y);
        //}

        void HandleMouseAndKeyBoard()
		{
			//this.transform.LookAt(PlayerTarget.transform);// constant look at player cahracter

			if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				var mousePosition = Input.mousePosition;
				mousePosition.z = _referenceCamera.transform.localPosition.y;
				_delta = _referenceCamera.ScreenToWorldPoint(mousePosition) - _referenceCamera.transform.localPosition;
				_delta.y = 0f;
				if (_shouldDrag == false)
				{
					_shouldDrag = true;
					_origin = _referenceCamera.ScreenToWorldPoint(mousePosition);
				}
			}
			else
			{
				_shouldDrag = false;
			}

			if (_shouldDrag == true)
			{
				var offset = _origin - _delta;
				offset.y = transform.localPosition.y;
				transform.localPosition = offset;
			}
			else
			{
				if (EventSystem.current.IsPointerOverGameObject())
				{
					return;
				}

				var x = Input.GetAxis("Horizontal");
				var z = Input.GetAxis("Vertical");
				var y = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
				if (!(Mathf.Approximately(x, 0) && Mathf.Approximately(y, 0) && Mathf.Approximately(z, 0)))
				{
					transform.localPosition += transform.forward * y + (_originalRotation * new Vector3(x * _panSpeed, 0, z * _panSpeed));
					_map.UpdateMap();
				}
			}


		}

		void ResetCamera()
		{
			this.transform.LookAt(PlayerTarget.transform);
		}
		void Awake()
		{
			_originalRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

			if (_referenceCamera == null)
			{
				_referenceCamera = GetComponent<Camera>();
				if (_referenceCamera == null)
				{
					throw new System.Exception("You must have a reference camera assigned!");
				}
			}

			if (_map == null)
			{
				_map = FindObjectOfType<AbstractMap>();
				if (_map == null)
				{
					throw new System.Exception("You must have a reference map assigned!");
				}
			}
		}

		void LateUpdate()
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