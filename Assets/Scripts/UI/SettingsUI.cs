using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;
    

public class SettingsUI : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Toggle automatedLocationToggle;
    [SerializeField] Toggle trackedLocationToggle;
    [SerializeField] ToggleGroup locationToggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        if(AudioManager.Instance)
        {
            masterSlider.value = AudioManager.Instance.MasterVolume;
        }

        if(SettingsModes.locationMode == SettingsModes.Location.Tracking)
        {
            trackedLocationToggle.isOn = true;
            locationToggleGroup.NotifyToggleOn(trackedLocationToggle);
        }

        else
        {
            automatedLocationToggle.isOn = true;
            locationToggleGroup.NotifyToggleOn(automatedLocationToggle);
        }
        automatedLocationToggle.onValueChanged.AddListener(delegate { ToggleLocationSettings(); });
        trackedLocationToggle.onValueChanged.AddListener(delegate { ToggleLocationSettings(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableCustomAudioSettings(Toggle t)
    {
        masterSlider.interactable = !t.isOn;
        BGMSlider.interactable = t.isOn;
        SFXSlider.interactable = t.isOn;

        if (t.isOn)
        {
            if (AudioManager.Instance)
            {
                AudioManager.Instance.SetBGMVolume(BGMSlider.value);
                AudioManager.Instance.SetSFXVolume(SFXSlider.value);
            }
        }

        else
        {
            if(AudioManager.Instance)
            {
                AudioManager.Instance.SetMasterVolume(masterSlider.value);
            }
        }
    }

    public void SetMasterVolume(Slider masterVolumeSlider)
    {
        AudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
    }

    public void SetMusicVolume(Slider BGMVolumeSlider)
    {
        AudioManager.Instance.SetBGMVolume(BGMVolumeSlider.value);
    }

    public void SetSFXVolume(Slider SFXVolumeSlider)
    {
        AudioManager.Instance.SetSFXVolume(SFXVolumeSlider.value);
    }

    public void ToggleLocationSettings()
    {
        InitializeMapWithLocationProvider imwlp = FindObjectOfType<InitializeMapWithLocationProvider>(true);
        if (imwlp)
        {

            if (locationToggleGroup.GetFirstActiveToggle() == automatedLocationToggle)
            {
                SettingsModes.locationMode = SettingsModes.Location.Automated;
                Debug.Log("Automated");
            }

            else if (locationToggleGroup.GetFirstActiveToggle() == trackedLocationToggle)
            {
                SettingsModes.locationMode = SettingsModes.Location.Tracking;
                Debug.Log("Tracking");
            }

            imwlp.ResetPosition();
        }

    }
}
