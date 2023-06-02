namespace Mapbox.Unity.Map
{
	using System.Collections;
	using Mapbox.Unity.Location;
	using UnityEngine;

	public class InitializeMapWithLocationProvider : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		ILocationProvider _locationProvider;
    
		private void Awake()
		{
			// Prevent double initialization of the map. 
			_map.InitializeOnStart = false;
		}

		protected virtual IEnumerator Start()
		{
			yield return null;
			_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
			_locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;

			//LocationProvider_OnLocationUpdated();
		}

        public void ResetPosition()
        {
			//_map.ResetMap();
			if (SettingsModes.locationMode == SettingsModes.Location.Automated)
			{
                Debug.Log("Automated");
                //LocationProvider_OnLocationUpdated();
				//_map.SetCenterLatitudeLongitude(new Utils.Vector2d(14.56473f, 120.993796f));
                //LocationProvider_OnLocationUpdated();	
                //Location tempLoc = new Location();
                //tempLoc.LatitudeLongitude = new Utils.Vector2d(14.56473f, 120.993796f);
                //_map.Initialize(tempLoc.LatitudeLongitude, _map.AbsoluteZoom);

            }

            else if(SettingsModes.locationMode == SettingsModes.Location.Tracking)
			{
				Debug.Log("Tracking");
				Debug.Log(_locationProvider.CurrentLocation.LatitudeLongitude);
                //LocationProvider_OnLocationUpdated(_locationProvider.CurrentLocation);
				//_map.SetCenterLatitudeLongitude(_locationProvider.CurrentLocation.LatitudeLongitude);
				//if(_locationProvider != null)
				//{
				//	_map.Initialize(_locationProvider.CurrentLocation.LatitudeLongitude, _map.AbsoluteZoom);
    //            }

            }
        }

        void LocationProvider_OnLocationUpdated(Unity.Location.Location location)
		{
            _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;

            if (SettingsModes.locationMode == SettingsModes.Location.Tracking)
			{
                _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
            }

			else
			{
                Location tempLoc = new Location();
                tempLoc.LatitudeLongitude = new Utils.Vector2d(14.56473f, 120.993796f);
                _map.Initialize(tempLoc.LatitudeLongitude, _map.AbsoluteZoom);
            }
			
			MapTracker.isMapInitialized = true;
			EventManager.InitializeMap();
		}

		//void LocationProvider_OnLocationUpdated()
		//{
		//	Location tempLoc = new Location();
		//	tempLoc.LatitudeLongitude = new Utils.Vector2d(14.56473f, 120.993796f);
		//	_map.Initialize(tempLoc.LatitudeLongitude, _map.AbsoluteZoom);
		//	MapTracker.isMapInitialized = true;
		//	EventManager.InitializeMap();
		//}
	}
}
