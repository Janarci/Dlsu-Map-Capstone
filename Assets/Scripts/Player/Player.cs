using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Item> inventory;
    //public static Player Instance;

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
            

    //    else
    //        Destroy(this.gameObject);

    //}
    void Start()
    {
        //inventory= new List<Item>();
    }
    private Sector currentSector;
    // Update is called once per frame
    void Update()
    {
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sector"))
        {
            Sector sectorScript = other.GetComponent<Sector>();
            if (sectorScript != null)
            {
                foreach (Building.Type b in SectorManager.Instance.sectorList.Keys)
                {
                    if ( sectorScript == SectorManager.Instance.sectorList[b] && !SectorManager.Instance.sectorList[b].isUnlocked)//not unlocked
                    {
                        SectorManager.Instance.UnlockSector(b); ;
                    }
                }
            }
               
			
            //isInSector = true;
            // Perform any actions or set variables related to being inside the sector
        }
    }
}
