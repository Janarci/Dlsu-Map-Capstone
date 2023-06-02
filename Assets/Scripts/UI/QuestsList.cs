using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class QuestsList : MonoBehaviour
{


    [SerializeField] GameObject questItemTemplate;
    [SerializeField] GameObject questContent;
    [SerializeField] Dictionary<Quest.QuestCode, GameObject> questList;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartQuestsUI(questContent.transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuestsUI()
    {
        if(questList == null)
        questList = new Dictionary<Quest.QuestCode, GameObject>();

        //StartCoroutine(InitializeQuestsUICoroutine());
        InitializeQuestsUI();
    }

    void InitializeQuestsUI()
    {
        if (AccomplishmentDatabase.Instance && AchievementsManager.instance)
        {
            //if (!AccomplishmentDatabase.Instance.isInitialized)
            //{
            //    Debug.Log("Delaying");
            //    yield return null;
            //}

            foreach (Quest quest in AccomplishmentDatabase.Instance.questsList)
            {
                int i = 0;
                while (i < quest.tasks.Count)
                {
                    if (AchievementsManager.instance.questsProgress[quest.code] < quest.tasks[i].requirement)
                    {
                        GameObject newQuestItem = Instantiate(questItemTemplate, questContent.transform);
                        bool hasQuestComp = newQuestItem.TryGetComponent<QuestItemBehaviour>(out QuestItemBehaviour questComp);
                        if (hasQuestComp)
                        {
                            questComp.SetInfo(quest.instruction, AchievementsManager.instance.questsProgress[quest.code], quest.tasks[i].requirement, i, quest.expanded_tooltip, quest.code);
                            questList.Add(quest.code, newQuestItem);
                            questComp.OnMaxOut += RemoveQuestItem;
                        }

                        break;
                    }

                    else
                    {
                        i++;
                    }

                    //yield return null;
                }
            }
        }
    }

    public void UpdateQuestItem(Quest.QuestCode _code, int _newCurrentProgress)
    {
        if(questList.ContainsKey(_code))
        {
            questList[_code].TryGetComponent<QuestItemBehaviour>(out QuestItemBehaviour qb);
            if(qb)
            {
                qb.SetCurrentProgress(_newCurrentProgress);
            }
        }
    }

    public void RemoveQuestItem(Quest.QuestCode _code)
    {

        if(questList.ContainsKey(_code))
        {

            if (questList.Remove(_code, out GameObject _item))
            {
                Destroy(_item);
            }
        }
    }
}
