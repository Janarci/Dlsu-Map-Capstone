using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Intro()
    {
        TutorialManager.instance.UnlockTutorial(TutorialManager.Type.intro);
        AchievementsManager.instance.PerformMainQuest(MainQuest.QuestCode.intro);

        if(AchievementsManager.instance.currentMainQuest.info.type == MainQuest.QuestCode.intro)
        {

                foreach (MainQuest.Dialogue d in AchievementsManager.instance.currentMainQuest.dialogue)
                {
                    PopupGenerator.Instance?.GenerateTutorialPopup(d.script);
                }
            
        }
    }
}
