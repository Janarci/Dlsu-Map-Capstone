using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HQBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        int i = 0;

        if(Values.befriended_cats != null)
        foreach (GameObject go in Values.befriended_cats)
        {
            Debug.Log(i);
            GameObject catCopy = GameObject.Instantiate(go, new Vector3(-12 + (i * 3), 0, -20 + (j * 3)), Quaternion.Euler(new Vector3(-90, 180, 0)));
            catCopy.SetActive(true);
            i++;
            if(i >= 9)
            {
                j++;
                i = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToCatCatching()
    {
        LoadScene.LoadCatBefriendingScene();
    }
}
