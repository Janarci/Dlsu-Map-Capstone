using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    //[SerializeField] private GameObject sectorBlockerTemplate;
    [SerializeField] private GameObject sectorBlockerObj;
    private int id;
    public bool isUnlocked
    { get; private set; }

    // Start is called before the first frame update
    
    public void Initialize(int sectorID)
    {
        id = sectorID;
        ShowBlocker();
        isUnlocked = false;
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
        sectorBlockerObj = template;


    }

    void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
    //    //if(sectorBlockerObj)
    //    //Gizmos.DrawLine(new Vector3(GetAreaRect().x - GetAreaRect().width/2, 0, GetAreaRect().y), new Vector3(GetAreaRect().x + GetAreaRect().width / 2, 0, GetAreaRect().y));
    //   // Gizmos.DrawLine(new Vector3(GetAreaRect().x, 0, GetAreaRect().y - GetAreaRect().height / 2), new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height / 2));

    //    //Gizmos.DrawLine(new Vector3(GetAreaRect().x, 0, GetAreaRect().y - GetAreaRect().height / 2), new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height / 2));

        if(sectorBlockerObj)
        {
            Gizmos.DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
            Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position);

            Gizmos.DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
            Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position);

            Gizmos.DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
            Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position);

            Gizmos.DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
            Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position);
        }

        


    }


    private void ShowBlocker()
    {

        sectorBlockerObj.SetActive(true);
    }

    private void HideBlocker()
    {
        sectorBlockerObj.SetActive(false);
    }

    public Rect GetAreaRect()
    {
        if(sectorBlockerObj)
        {
            Rect areaRect = new Rect(sectorBlockerObj.transform.position.x - ((sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.x * sectorBlockerObj.transform.lossyScale.x) / 2),
                sectorBlockerObj.transform.position.z - ((sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.z * sectorBlockerObj.transform.lossyScale.z) / 2),
                sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.x * sectorBlockerObj.transform.lossyScale.x,
                sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.z * sectorBlockerObj.transform.lossyScale.z);

            return areaRect;
        }

        return Rect.zero;
    }

    public int getID()
    {
        return id;
    }
}
