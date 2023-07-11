using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainQuest : Accomplishment
{
    public enum QuestCode
    {
        none,
        open_catalog_main,
        open_catalog_cats,
        open_catalog_buildings,
        unlock_sector,
        unlock_chillspace,
        befriend_cat,
        open_inventory,
        visit_hq,
        level_up_cat,
        evolve_cat,

    }

    [Serializable]
    public class Node
    {
        public QuestCode type;
        public QuestCode prev;
        public QuestCode next;
    }

    public Node info;
    public string instruction;
    [TextArea(3, 5)] public string expanded_tooltip;

    private MainQuest()
    {
        base.type = Accomplishment.Type.quest;
    }
}