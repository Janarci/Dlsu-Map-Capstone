using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
    }

    public static void SpawnCatReceived(Packet _packet)
    {
        int _sectorToSpawn = _packet.ReadInt();
        SectorManager csu = FindObjectOfType<SectorManager>();

        //csu.SpawnCatInSector(_sectorToSpawn);
    }


}