namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
	using UnityEngine;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{

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
				Vector3 initialPos = transform.localPosition;
				var map = LocationProviderFactory.Instance.mapManager;
				transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
				Vector3 finalPos = transform.localPosition;

				if(initialPos != finalPos)
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