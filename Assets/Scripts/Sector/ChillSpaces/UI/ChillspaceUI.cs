using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChillspaceUI : MonoBehaviour
{
    [SerializeField] private Text areaName;
    [SerializeField] private Text areaInfo;
    [SerializeField] private Text areaContactNumber;
    [SerializeField] private Text areaEmail;
    [SerializeField] private Text areaItems;




    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(areaName.text);
    }

    public void SetName(string name)
    {
        areaName.text = name;
        
        Debug.Log("Changing name");
        Debug.Log(areaName.text);
    }

    public void SetInfo(string info)
    {
        this.areaInfo.text = info;
    }

    public void SetNumber(string number)
    {
        this.areaContactNumber.text = number;
    }

    public void SetEmail(string email)
    {
        this.areaEmail.text = email;
    }

    public void SetItems(List<CatEvolutionItem.cat_evolution_item_type> items)
    {
        foreach(CatEvolutionItem.cat_evolution_item_type item in items)
        {
            areaItems.text += (item.ToString() + ",\n");
        }
    }
}
