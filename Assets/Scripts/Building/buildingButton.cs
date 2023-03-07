using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class buildingButton : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject henryButton;

    private bool mouseIsOver = false;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        //Close the Window on Deselect only if a click occurred outside this panel
        if (!mouseIsOver)
            gameObject.SetActive(false);
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        Debug.Log("mouse over true");

        mouseIsOver = true;
		EventSystem.current.SetSelectedGameObject(gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        Debug.Log("mouse over false");

        mouseIsOver = false;
		EventSystem.current.SetSelectedGameObject(gameObject);
	}

	// Update is called once per frame
	void Update()
    {

    }
}
