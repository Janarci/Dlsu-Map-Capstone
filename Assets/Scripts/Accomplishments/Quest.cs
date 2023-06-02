using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest : Accomplishment
{
    public enum QuestCode
    {
        unlock_sectors,
        befriend_cats,
        discover_cat_types,
    }

    [Serializable]
    public class QuestReward
    {
        public CatEvolutionItem.cat_evolution_item_type item;
        public int amount;
    }

    [Serializable]
    public class QuestTask
    {
        public int requirement;
        public List<QuestReward> rewards;
    }

    public QuestCode code;
    public List<QuestTask> tasks;
    public string instruction;
    [TextArea(3, 5)] public string expanded_tooltip;

    private Quest()
    {
        base.type = Accomplishment.Type.quest;
    }
}
