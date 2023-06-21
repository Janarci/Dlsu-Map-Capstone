using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsModes : MonoBehaviour
{
    public enum Location
    {
        Automated,
        Tracking,
    }
   
    public static Location locationMode = Location.Automated;
}
