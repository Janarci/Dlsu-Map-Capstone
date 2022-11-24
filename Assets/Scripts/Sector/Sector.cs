using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    [SerializeField] private GameObject sectorBlockerTemplate;
    [SerializeField] private GameObject sectorBlockerObj;
    private int id;
    private bool isUnlocked = false;
    // Start is called before the first frame update
    
    public void Initialize(int sectorID)
    {
        id = sectorID;
        SpawnBlocker();
    }    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        isUnlocked = true;
        HideBlocker();
    }

    public void SetSectorBlockerObj(GameObject template)
    {
        sectorBlockerTemplate = template;
    }

    private void SpawnBlocker()
    {
        sectorBlockerObj = GameObject.Instantiate(sectorBlockerTemplate, this.gameObject.transform);
        sectorBlockerObj.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void HideBlocker()
    {
        sectorBlockerObj.SetActive(false);
    }
}
