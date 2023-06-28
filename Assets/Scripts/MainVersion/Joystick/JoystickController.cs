using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector2 joystickDirection;

    // Public event to handle joystick movement
    public event System.Action<Vector2> OnJoystickMove;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out position))
        {
            position.x /= transform.parent.GetComponent<RectTransform>().rect.width * 0.5f;
            position.y /= transform.parent.GetComponent<RectTransform>().rect.height * 0.5f;
            position = Vector2.ClampMagnitude(position, 1f);

            joystickDirection = position;

            // Invoke the event to notify other components of joystick movement
            OnJoystickMove?.Invoke(joystickDirection);

            transform.localPosition = position * transform.parent.GetComponent<RectTransform>().rect.width * 0.5f;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickDirection = Vector2.zero;
        transform.localPosition = Vector2.zero;

        // Invoke the event with zero direction to indicate joystick release
        OnJoystickMove?.Invoke(joystickDirection);
    }
}
