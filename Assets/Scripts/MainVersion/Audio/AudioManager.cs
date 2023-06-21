using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] music;
    public Sounds[] soundEffects;

    public static AudioManager Instance;

    [Range(0f, 1f)] private float masterVolume = 1.0f;
    public float MasterVolume { get { return masterVolume; } }
    [Range(0f, 1f)] private float musicVolume = 1.0f;
    public float MusicVolume { get { return musicVolume; } }

    [Range(0f, 1f)] private float sfxVolume = 1.0f;
    public float SFXVolume { get { return sfxVolume; } }

    [Range(0.5f, 1f)] private float pitch = 1.0f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sounds s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * masterVolume;
            s.source.pitch = s.pitch * pitch;
            s.source.loop = s.loop;
        }

        foreach (Sounds s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * masterVolume;
            s.source.pitch = s.pitch * pitch;
            s.source.loop = s.loop;
        }
    }

    public float getCurrVolume()
    {
        Sounds s = Array.Find(music, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return 0;
        }

        return s.source.volume;
    }

    private void AdjustMusicVolume()
    {
        foreach(Sounds s in music)
        {
            s.source.volume = (0.75f * masterVolume * musicVolume);
        }
    }

    public void AdjustMusicVolume(string name, float newVolume)
    {
        Sounds s = Array.Find(music, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = 0.75f * newVolume;
    }

    private void AdjustSFXVolume()
    {
        foreach(Sounds s in soundEffects)
        {
            s.source.volume = (0.75f * masterVolume * sfxVolume);
        }
    }

    public void AdjustPitch(string name, string type, float newPitch)
    {
        if (String.Compare(type, "bgm") == 0)
        {
            Sounds s = Array.Find(music, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.pitch = newPitch;
        }
        else if (String.Compare(type, "sfx") == 0)
        {
            Sounds s = Array.Find(music, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.pitch = newPitch;
        }
    }

    public void SetSFXVolume(float newSFXVolume)
    {
        sfxVolume = newSFXVolume;
        foreach (Sounds s in soundEffects)
        {
            s.source.volume = (newSFXVolume) * s.volume;
        }
    }

    public void SetBGMVolume(float newBGMVolume)
    {
        musicVolume = newBGMVolume;
        foreach (Sounds s in music)
        {
            s.source.volume = (newBGMVolume) * s.volume;
        }
    }

    public void SetMasterVolume(float newMasterVolume)
    {
        masterVolume = newMasterVolume;

        foreach (Sounds s in music)
        {
            s.source.volume = (newMasterVolume) * s.volume;
        }

        foreach (Sounds s in soundEffects)
        {
            s.source.volume = (newMasterVolume) * s.volume;
        }
    }
    public void UpdateMasterVolume(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
        AdjustMusicVolume();
        AdjustSFXVolume();
    }

    public void UpdateMusicVolume(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        AdjustMusicVolume();
    }

    public void UpdateSFXVolume(float newSFXVolume)
    {
        sfxVolume = newSFXVolume;
        AdjustSFXVolume();
    }

    public void Play(string name, string type, bool isLoop = true)
    {
        if (String.Compare(type, "bgm") == 0)
        {
            Sounds s = Array.Find(music, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if(!s.source.isPlaying)
            {
                s.source.Play();
            }
            s.source.loop = isLoop;
        }
        else if (String.Compare(type, "sfx") == 0)
        {
            Sounds s = Array.Find(soundEffects, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if(!s.source.isPlaying)
            {
                s.source.Play();
            }
            s.source.loop = isLoop;
        }
    }

    public void Stop(string name, string type)
    {
        if (String.Compare(type, "bgm") == 0)
        {
            Sounds s = Array.Find(music, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.Stop();
            s.source.loop = false;
        }
        else if (String.Compare(type, "sfx") == 0)
        {
            Sounds s = Array.Find(soundEffects, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.Stop();
            s.source.loop = false;
        }
    }

    public void Pause(string name, string type)
    {
        if (String.Compare(type, "bgm") == 0)
        {
            Sounds s = Array.Find(music, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if(s.source.isPlaying)
            {
                s.source.Pause();
            }
        }
        else if (String.Compare(type, "sfx") == 0)
        {
            Sounds s = Array.Find(soundEffects, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if (s.source.isPlaying)
            {
                s.source.Pause();
            }
        }
    }
}
