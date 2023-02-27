using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldPositionButton : MonoBehaviour
{
    [SerializeField] public Transform targettTransform;



    private RectTransform rectTransform;
    private Image image;



	private void Awake()
	{
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(targettTransform.position);
        //GetComponent<RectTransform>().position = screenPoint;
        rectTransform.position = screenPoint;

        var viewportPoint = Camera.main.WorldToViewportPoint(targettTransform.position);
        var distanceFromCenter = Vector2.Distance(viewportPoint, Vector2.one * 0.5f);

        //var show = distanceFromCenter < 0.3f;

        //GetComponent<Image>().enabled = show;

    }
}
