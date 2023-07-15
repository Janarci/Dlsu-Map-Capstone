namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
	using UnityEngine;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{
		public bool joystick = false;
		[SerializeField] Animator animator;
		bool _isInitialized;
		bool _isMoving = false;

		ILocationProvider _locationProvider;
		ILocationProvider LocationProvider
		{
			get
			{
				if (_locationProvider == null)
				{
					_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
				}

				return _locationProvider;
			}
		}


		Vector3 _targetPosition;

		void Start()
		{
			LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
		}

        

        void LateUpdate()
		{
			if (_isInitialized)
			{
				Vector3 initialPos = initialPos = transform.localPosition;
                
                var map = LocationProviderFactory.Instance.mapManager;
				if (SettingsModes.locationMode == SettingsModes.Location.Automated)
				{
					//transform.localPosition = map.GeoToWorldPosition(new Utils.Vector2d(14.56473f, 120.993796f));
					//map.SetCenterLatitudeLongitude(new Utils.Vector2d(14.56473f, 120.993796f));
					
				}

				else if (SettingsModes.locationMode == SettingsModes.Location.Tracking)
				{
					transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
					map.SetCenterLatitudeLongitude(LocationProvider.CurrentLocation.LatitudeLongitude);
                }
                Vector3 finalPos = transform.localPosition;


				if (Vector3.Distance(transform.position, CameraManager.Instance.transform.position) > 1000.0f)
				{
					CameraManager.Instance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
					CameraManager.Instance.SetWorldCameraPosition(new Vector3(transform.position.x, transform.position.y + 50, transform.position.z - 20));
					Debug.Log("Respoitioning camera");
					//map.UpdateMap();

				}

				//else
				//{
				//	Debug.Log(Vector3.Distance(transform.position, CameraManager.Instance.transform.position));
				//}


				if (initialPos != finalPos)
				{
					//walk
					if(animator && !_isMoving)
					{
						Debug.Log("start walking");
						_isMoving = true;
						animator.SetTrigger("Walk");
					}
				}

				else
				{
                    //dont walk
					if(animator && _isMoving)
					{
						_isMoving = false;
                        animator.SetTrigger("Stop");
                    }

                }

            }
		}
	}
}