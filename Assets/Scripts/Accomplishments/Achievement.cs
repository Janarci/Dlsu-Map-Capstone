using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Achievement : Accomplishment
{
    public enum AchievementCode
    {
        befriend_first_cat,
        unlock_first_sector
    }

    public AchievementCode code;
    public string name;
    [TextArea(3, 5)] public string description;
    public Sprite icon;


    private Achievement()
    {
        base.type = Accomplishment.Type.achievement;
    }
}
