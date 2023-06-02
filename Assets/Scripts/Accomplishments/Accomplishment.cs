using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Accomplishment
{
    public enum Type
    {
        none,
        achievement,
        quest,
    }

    protected Type type;
}
