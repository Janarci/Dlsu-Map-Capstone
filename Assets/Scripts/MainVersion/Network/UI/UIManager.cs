using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public string username = "testuser";

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(this);
    }

    public void ConnectToServer()
    {
        Client.instance.ConnectToServer();
    }
}
