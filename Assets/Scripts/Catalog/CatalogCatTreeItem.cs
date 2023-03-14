using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogCatTreeItem : MonoBehaviour
{
    public List<GameObject> childrenEvolutionPath;
    public List<GameObject> LineHolders;
    public CatType.Type type;
    public CatalogCatInfo catInfo;
    public GameObject catInfoMenu;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<Text>().text = type.ToString();
        LineHolders = new List<GameObject>();
        foreach(GameObject gj in childrenEvolutionPath)
        {
            GameObject temp = new GameObject("empty");
            temp.transform.parent = transform;
            LineHolders.Add(temp);
            LineRenderer lr = temp.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.25f; lr.endWidth = 0.25f;
            lr.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.005f));
            lr.SetPosition(1, new Vector3(gj.transform.position.x, gj.transform.position.y, gj.transform.position.z - 0.005f));


        }

        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DisplayCatInfo(type); });
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gj in LineHolders)
        {
            LineRenderer lr = gj.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.005f));
            lr.SetPosition(1, new Vector3(gj.transform.position.x, gj.transform.position.y, gj.transform.position.z - 0.005f));


        }

        for(int i = 0; i < LineHolders.Count; i++)
        {
            LineRenderer lr = LineHolders[i].GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.005f));
            lr.SetPosition(1, new Vector3(childrenEvolutionPath[i].transform.position.x, childrenEvolutionPath[i].transform.position.y, childrenEvolutionPath[i].transform.position.z - 0.005f));
        }
    }

    public void DisplayCatInfo(CatType.Type catType)
    {
        catInfo.SetCatInfo(type);
        catInfo.SetCatHabitats(type);

        Catalog.currentMenu.SetActive(false);
        Catalog.menuHistory.Add(Catalog.currentMenu);
        Catalog.currentMenu = catInfoMenu;
        Catalog.currentMenu.SetActive(true);
    }
}
