using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetsHolder : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        if (VuforiaApplication.Instance.IsInitialized)
        {
            InitializeImageTargets();
        }

        else
            VuforiaApplication.Instance.OnBeforeVuforiaInitialized += InitializeImageTargets;
    }

    private void InitializeImageTargets()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if (go.TryGetComponent<ImageTargetBehaviour>(out ImageTargetBehaviour itb))
            {
                go.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        VuforiaApplication.Instance.OnBeforeVuforiaInitialized -= InitializeImageTargets;

    }
}
