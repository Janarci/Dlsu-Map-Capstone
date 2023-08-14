using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainQuestDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    // Start is called before the first frame update
    
    public void SetText(string text)
    { txt.text = text; }
}
