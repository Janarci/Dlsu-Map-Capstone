using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sector : MonoBehaviour
{

    

    //[SerializeField] private GameObject sectorBlockerTemplate;
    [SerializeField] private int id;
    [SerializeField] private GameObject sectorBlockerObj;
    [SerializeField] private GameObject TooltipUI;
    public Building.Type type;
    //[SerializeField] List<ChillSpace.Area> chillSpaces;

    private Building building;

    public string tooltip
    { private get; set; }
    public bool isUnlocked
    { get; private set; }

    // Start is called before the first frame update


    public void Initialize(int sectorID)
    {
        id = sectorID;
        ShowBlocker();
        isUnlocked = false;
        //if(chillSpaces == null)
        //{
        //    chillSpaces = new List<ChillSpace.Area>();
        //}

        EventManager.InitializeSector(this, gameObject.GetComponent<Building>());
    }

    public void InitializeSector()
    {
        EventManager.InitializeSector(this, gameObject.GetComponent<Building>());
    }
    void Start()
    {
        building = gameObject.GetComponent<Building>();
        InitializeSector();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Unlock()
    {
        //if (!DataPersistenceManager.instance.gameData.unlocked_sectors.Contains(type))
        //{
        //    //DisplayTooltip();
        //    DataPersistenceManager.instance.gameData.unlocked_sectors.Add(type);
        //}

        isUnlocked = true;
        HideBlocker();
        MakeDesignatedBuildingOpaque();
        //DisplayTooltip();

        //foreach(ChillSpace.Area cs in chillSpaces) 
        //{
        //    ChillSpacesManager.Instance.UnlockChillSpace(cs);
        //}
        int csCount = BuildingDatabase.Instance.GetDataInfo(type).chillspaces.Count;

        for (int i = 0; i < csCount; i++)
        {
            ChillSpace.Area cs = BuildingDatabase.Instance.GetDataInfo(type).chillspaces[i];
            ChillSpacesManager.Instance.UnlockChillSpace(cs);
        }
    }

    public void Lock()
    {
        isUnlocked = false;
        ShowBlocker();
        MakeDesignatedBuildingTransparent();

        int csCount = BuildingDatabase.Instance.GetDataInfo(type).chillspaces.Count;
        for (int i = 0; i < csCount; i++)
        {
            ChillSpace.Area cs = BuildingDatabase.Instance.GetDataInfo(type).chillspaces[i];
            ChillSpacesManager.Instance.LockChillSpace(cs);
        }
    }

    public void DisplayTooltip()
    {
        //if (TooltipUI != null)
        //{
        //    GameObject mainCanvas = GameObject.Find("InfoCanvas");
        //    GameObject infoUI = Instantiate(TooltipUI, mainCanvas.transform);
        //    infoUI.transform.GetChild(0).GetComponent<Text>().text = "You have unlocked Sector: " + id + "\n" + "Directions to next Sector:";
        //    infoUI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoUI); });
        //}

        if(!(DataPersistenceManager.instance.gameData.unlocked_sectors.Contains(type)))
        {
            PopupGenerator.Instance?.GenerateCloseablePopup(
            "You have unlocked Sector: " + id
            );
        }    
        

        else
        {
            PopupGenerator.Instance?.GenerateCloseablePopup(
            "Sector " + id + " already unlocked:"
            );
        }
    }

    public void SetSectorBlockerObj(GameObject template)
    {
        sectorBlockerObj = template;
    }

    public void SwtTooltipUITemplate(GameObject UITemplate)
    {
        TooltipUI = UITemplate;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    //    //if(sectorBlockerObj)
    //    //    //Gizmos.DrawLine(new Vector3(GetAreaRect().x - GetAreaRect().width/2, 0, GetAreaRect().y), new Vector3(GetAreaRect().x + GetAreaRect().width / 2, 0, GetAreaRect().y));
    //    //   // Gizmos.DrawLine(new Vector3(GetAreaRect().x, 0, GetAreaRect().y - GetAreaRect().height / 2), new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height / 2));

    //    //    //Gizmos.DrawLine(new Vector3(GetAreaRect().x, 0, GetAreaRect().y - GetAreaRect().height / 2), new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height / 2));

    //    if (sectorBlockerObj)
    //    {

    //        DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
    //        Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position, 5);

    //        DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
    //        Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position, 5);

    //        DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
    //        Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position, 5);

    //        DrawLine(Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position,
    //        Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position, 5);
    //    }




    //}

    //public static void DrawLine(Vector3 p1, Vector3 p2, float width)
    //{
    //    int count = 1 + Mathf.CeilToInt(width); // how many lines are needed.
    //    if (count == 1)
    //    {
    //        Gizmos.DrawLine(p1, p2);
    //    }
    //    else
    //    {
    //        Camera c = Camera.current;
    //        if (c == null)
    //        {
    //            Debug.LogError("Camera.current is null");
    //            return;
    //        }
    //        var scp1 = c.WorldToScreenPoint(p1);
    //        var scp2 = c.WorldToScreenPoint(p2);

    //        Vector3 v1 = (scp2 - scp1).normalized; // line direction
    //        Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

    //        for (int i = 0; i < count; i++)
    //        {
    //            Vector3 o = 0.99f * n * width * ((float)i / (count - 1) - 0.5f);
    //            Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
    //            Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
    //            Gizmos.DrawLine(origin, destiny);
    //        }
    //    }
    //}


    private void ShowBlocker()
    {

        sectorBlockerObj.SetActive(true);
    }

    private void HideBlocker()
    {
        sectorBlockerObj.SetActive(false);
    }

    private void MakeDesignatedBuildingOpaque()
    {
        Debug.Log("Making designated building opaque");
        GetComponent<Building>()?.MakeOpaque();
    }

    private void MakeDesignatedBuildingTransparent()
    {
        GetComponent<Building>()?.MakeTransparent();
    }

    public Rect GetAreaRect()
    {
        if (sectorBlockerObj)
        {
            Rect areaRect = new Rect(sectorBlockerObj.transform.position.x - ((sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.x * sectorBlockerObj.transform.lossyScale.x) / 2),
                sectorBlockerObj.transform.position.z - ((sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.z * sectorBlockerObj.transform.lossyScale.z) / 2),
                sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.x * sectorBlockerObj.transform.lossyScale.x,
                sectorBlockerObj.GetComponent<MeshFilter>().mesh.bounds.size.z * sectorBlockerObj.transform.lossyScale.z);

            return areaRect;
        }

        return Rect.zero;
    }

    public bool IsPointWithinSector(Vector3 point)
    {
        bool isPointWithinSector = true;

        Vector3 point1 = Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position;
        Vector3 point2 = Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position;

        Vector3 point3 = Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position;
        Vector3 point4 = Quaternion.LookRotation(sectorBlockerObj.transform.forward, sectorBlockerObj.transform.up) * (new Vector3(GetAreaRect().x + GetAreaRect().width, 0, GetAreaRect().y + GetAreaRect().height) - sectorBlockerObj.transform.position) + sectorBlockerObj.transform.position;

        float[] xValues = { point1.x, point2.x, point3.x, point4.x };
        float[] zValues = { point1.z, point2.z, point3.z, point4.z };

        float xMin = Mathf.Min(xValues);
        float xMax = Mathf.Max(xValues);
        float zMin = Mathf.Min(zValues);
        float zMax = Mathf.Max(zValues);

        if (point.x < xMin || point.x > xMax || point.z < zMin || point.z > zMax)
            isPointWithinSector = false;

        return isPointWithinSector;
    }

    public int getID()
    {
        return id;
    }

    //public List<ChillSpace.Area> GetChillSpaces()
    //{
    //    return chillSpaces;
    //}

    public Building GetBuilding()
    {
        return building;
    }

    private void OnDestroy()
    {
        EventManager.ReleaseSector(this, gameObject.GetComponent<Building>());
    }
}
