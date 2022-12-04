using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatUI : MonoBehaviour
{
    [SerializeField] private Image friendship_bar;
    [SerializeField] private Image relationship_level_bar;
    [SerializeField] private GameObject tooltip_popup;


    public Cat cat;
    // Start is called before the first frame update
    void Start()
    {
        //friendship_bar = gameObject.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFriendshipBarValue(float value)
    {
        //friendship_bar.fillAmount = value;
    }

    public void SetCat()
    {

    }
    public void SetCat(Cat cat)
    {
        this.cat = cat;
    }


}
