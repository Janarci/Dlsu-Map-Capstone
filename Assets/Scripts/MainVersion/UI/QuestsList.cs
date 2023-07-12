using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class QuestsList : MonoBehaviour
{


    [SerializeField] GameObject sideQuestItemTemplate;
    [SerializeField] GameObject mainQuestItemTemplate;
    [SerializeField] GameObject sideQuestContent;
    [SerializeField] GameObject mainQuestContent;
    [SerializeField] Dictionary<SideQuest.QuestCode, GameObject> sideQuestList;
    GameObject mainQuestItem = null;
    bool isDeselecting = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isDeselecting = true;
        }

        if(Input.GetMouseButtonUp(0) && isDeselecting)
        {
            gameObject.SetActive(false);
            isDeselecting = false;
        }
    }

    public void StartQuestsUI()
    {
        if(sideQuestList == null)
        {
            sideQuestList = new Dictionary<SideQuest.QuestCode, GameObject>();
        }

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

            foreach (SideQuest quest in AccomplishmentDatabase.Instance.sideQuestList)
            {
                int i = 0;
                while (i < quest.tasks.Count)
                {
                    if (AchievementsManager.instance.sideQuestsProgress[quest.code] < quest.tasks[i].requirement)
                    {
                        GameObject newQuestItem = Instantiate(sideQuestItemTemplate, sideQuestContent.transform);
                        bool hasQuestComp1 = newQuestItem.TryGetComponent<SideQuestItemBehaviour>(out SideQuestItemBehaviour questComp);
                        if (hasQuestComp1)
                        {
                            questComp.SetInfo(quest.instruction, AchievementsManager.instance.sideQuestsProgress[quest.code], quest.tasks[i].requirement, i, quest.expanded_tooltip, quest.code);
                            sideQuestList.Add(quest.code, newQuestItem);
                            questComp.OnMaxOut += RemoveSideQuestItem;
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

            if(AchievementsManager.instance.currentMainQuest.info.type != MainQuest.QuestCode.none)
            {
                mainQuestItem = Instantiate(mainQuestItemTemplate, mainQuestContent.transform);
                bool hasQuestComp2 = mainQuestItem.TryGetComponent<MainQuestItemBehaviour>(out MainQuestItemBehaviour questComp);
                if(hasQuestComp2)
                {
                    questComp.SetInfo(AchievementsManager.instance.currentMainQuest.instruction, AchievementsManager.instance.currentMainQuest.info.type);
                }
            }


        }
    }

    public void UpdateMainQuestItem(MainQuest.QuestCode _code, string _info)
    {
        if(mainQuestItem != null)
        {
            mainQuestItem.GetComponent<MainQuestItemBehaviour>().SetInfo(_info, _code);
        }
    }
    public void UpdateSideQuestItem(SideQuest.QuestCode _code, int _newCurrentProgress)
    {
        if(sideQuestList.ContainsKey(_code))
        {
            sideQuestList[_code].TryGetComponent<SideQuestItemBehaviour>(out SideQuestItemBehaviour qb);
            if(qb)
            {
                qb.SetCurrentProgress(_newCurrentProgress);
            }
        }
    }

    public void RemoveSideQuestItem(SideQuest.QuestCode _code)
    {

        if(sideQuestList.ContainsKey(_code))
        {

            if (sideQuestList.Remove(_code, out GameObject _item))
            {
                Destroy(_item);
            }
        }
    }
}
