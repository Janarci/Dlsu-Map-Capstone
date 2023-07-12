using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestItemBehaviour : MonoBehaviour
{
    MainQuest.QuestCode code = MainQuest.QuestCode.none;
    [SerializeField] Text txtTask;
    [SerializeField] Button btnTask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string _info, MainQuest.QuestCode _code)
    {
        txtTask.text = _info;
        code = _code;
    }
}
