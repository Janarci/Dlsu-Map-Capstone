using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _text)
    {
        message.text = _text;
    }
}
