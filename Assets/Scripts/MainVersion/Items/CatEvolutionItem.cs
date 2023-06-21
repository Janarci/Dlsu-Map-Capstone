using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEvolutionItem : Item
{
    public enum cat_evolution_item_type
    {
        homework,
        paycheck,
        script,
        costume,
        baton,
        steering_wheel,
        pen,
        book,
        money,
        theater_mask,
        boombox,
        microphone,
        trombone,
        video_camera,
        newspaper,
        stethoscope,
        syringe,
        dental_probe,
        usg_flag,
        cso_flag,
        scales_of_justice,
        laptop,
        basketball,
        bible,
        fish
    }

    public cat_evolution_item_type evolutionItemType
    {
        get;
        private set;
    }
    public CatEvolutionItem(cat_evolution_item_type evolution_item_type) : base(item_type.cat_evolution_material)
    {
        evolutionItemType = evolution_item_type;
    }
}

