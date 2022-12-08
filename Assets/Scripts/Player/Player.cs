using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Item> inventory;
    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            

        else
            Destroy(this.gameObject);

    }
    void Start()
    {
        inventory= new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
