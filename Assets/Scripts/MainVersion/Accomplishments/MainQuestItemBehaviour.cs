using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestItemBehaviour : MonoBehaviour
{
    MainQuest.QuestCode code = MainQuest.QuestCode.none;
    [SerializeField] Text txtTask;
    [SerializeField] Button btnTask;
    bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        btnTask.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(code != MainQuest.QuestCode.none)
        {
            if (!isComplete && AchievementsManager.instance.mainQuestPerformed[code] == true)
            {
                isComplete = true;
                btnTask.gameObject.SetActive(true);
            }
        }
        
    }

    public void SetInfo(string _info, MainQuest.QuestCode _code)
    {
        txtTask.text = _info;
        code = _code;

        isComplete = false;
        btnTask.gameObject.SetActive(false);
        btnTask.onClick.AddListener(delegate { OnTaskBtnPress(_code); });
    }
    
    void OnTaskBtnPress(MainQuest.QuestCode _code)
    {
        if (AchievementsManager.instance.currentMainQuest.info.type == _code)
        {
            AchievementsManager.instance.FinishCurrentMainQuest();
        }
    }
}
