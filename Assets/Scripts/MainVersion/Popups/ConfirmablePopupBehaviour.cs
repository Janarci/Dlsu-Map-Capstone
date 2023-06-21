using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmablePopupBehaviour : MonoBehaviour
{
    public Action onConfirm;
    public Action onCancel;
    
    public void Confirm()
    {
        if(onConfirm!= null) onConfirm();
    }

    public void Cancel()
    {
        if (onCancel != null) onCancel();
    }

    private void OnDestroy()
    {
        if (onCancel != null) onCancel();
    }
}
