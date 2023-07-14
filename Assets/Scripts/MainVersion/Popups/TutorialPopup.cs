using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    public Action OnPress;
    bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!pressed && gameObject.activeInHierarchy)
            {
                if (OnPress != null)
                {
                    OnPress();
                    pressed = true;
                }
            }
        }
        
    }

    public void SetText(string _text)
    {
        message.text = _text;
    }

    
}
