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
        intro,
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

    [Serializable]
    public class Dialogue
    {
        [TextArea(8, 5)] public string script;
    }

    public Node info;
    public string instruction;
    public List<Dialogue> dialogue;

    private MainQuest()
    {
        base.type = Accomplishment.Type.quest;
    }
}