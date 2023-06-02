using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AchievementPopupBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Text desc;
    [SerializeField] Image icon;
    public void SetInfo(Achievement.AchievementCode _type)
    {
        if(AccomplishmentDatabase.Instance)
        {
            title.text = AccomplishmentDatabase.Instance.GetAchievementData(_type).name;
            desc.text = AccomplishmentDatabase.Instance.GetAchievementData(_type).description;
            icon.sprite = AccomplishmentDatabase.Instance.GetAchievementData(_type).icon;

        }
    }
}
