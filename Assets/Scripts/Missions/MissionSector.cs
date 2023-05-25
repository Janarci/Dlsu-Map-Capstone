using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSector : Mission
{
    public Building.Type type { get; private set; }

    public override void CompleteMission()
    {
        base.CompleteMission();
        if(SectorManager.Instance)
        {
            SectorManager.Instance.UnlockSector(type);
        }
    }
}
