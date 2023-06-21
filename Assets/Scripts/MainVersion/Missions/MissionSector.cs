using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSector : Mission
{
    [SerializeField] public Building.Type type;

    public override void CompleteMission()
    {
        base.CompleteMission();
        if(SectorManager.Instance)
        {
            SectorManager.Instance.UnlockSector(type);

            if (!(DataPersistenceManager.instance.gameData.unlocked_sectors.Contains(type)))
            {
                PopupGenerator.Instance?.GenerateCloseablePopup(
                "You have unlocked Sector: " + BuildingDatabase.Instance.GetDataInfo(type).name
                );
            }


            else
            {
                PopupGenerator.Instance?.GenerateCloseablePopup(
                "Sector " + BuildingDatabase.Instance.GetDataInfo(type).name + " already unlocked:"
                );
            }
        }
    }
}
