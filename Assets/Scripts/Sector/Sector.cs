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
    [SerializeField] List<ChillSpace> chillSpaces;


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

        EventManager.InitializeSector(this);
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
        //DisplayTooltip();
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

        PopupGenerator.Instance?.GenerateCloseablePopup(
            "You have unlocked Sector: " + id + "\n" + "Directions to next Sector:"
            );
    }

    public void SetSectorBlockerObj(GameObject template)
    {
        sectorBlockerObj = template;
    }

    public void SwtTooltipUITemplate(GameObject UITemplate)
    {
        TooltipUI = UITemplate;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //    //if(sectorBlockerObj)
        //    //Gizmos.DrawLine(new Vector3(GetAreaRect().x - GetAreaRect().width/2, 0, GetAreaRect().y), new Vector3(GetAreaRect().x + GetAreaRect().width / 2, 0, GetAreaRect().y));
        //   // Gizmos.DrawLine(new Vector3(GetAreaRect().x, 0, GetAreaRect().y - GetAreaRect().height / 2), new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height / 2));

        //    //Gizmos.DrawLine(new Vector3(GetAreaRect().x, 0, GetAreaRect().y - GetAreaRect().height / 2), new Vector3(GetAreaRect().x, 0, GetAreaRect().y + GetAreaRect().height / 2));

        if (sectorBlockerObj)
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
}
