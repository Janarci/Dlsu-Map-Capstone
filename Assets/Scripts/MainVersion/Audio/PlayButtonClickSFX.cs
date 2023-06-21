using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonClickSFX : MonoBehaviour
{
    public void Play()
    {
        if(AudioManager.Instance)
        {
            AudioManager.Instance.Play("Catmeow Button Press", "sfx", false);
        }
    }
}
