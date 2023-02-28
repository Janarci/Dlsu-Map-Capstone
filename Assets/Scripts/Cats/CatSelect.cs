using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSelect : MonoBehaviour
{
    public static CatSelect Instance;
    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


        else
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended ) 
            {
                Ray r = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(r, out hit, 1000.0f ))
                {
                    if(hit.collider.gameObject.TryGetComponent<Cat>(out Cat clickedCat))
                    {
                        EventManager.CatClick(clickedCat);
                        Debug.Log("Cicked cat on phone");
                    }
                }
            }
            
        }
    }
}
