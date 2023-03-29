using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public static bool shouldRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        shouldRotate = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShouldRotate(bool _shouldRotate)
    {
        shouldRotate = _shouldRotate;
    }
}
