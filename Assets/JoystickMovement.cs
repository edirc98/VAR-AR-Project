using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform GamePad;

    private Vector3 move;
    private bool PointerDown;
    private bool PointerUp; 
    public Vector3 JoystickDir
    {
        get { return move; }
    }
    public bool IsPointerDown
    {
        get { return PointerDown; }
    }
    public bool IsPointerUp
    {
        get { return PointerUp; }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)GamePad.position, GamePad.rect.width * 0.5f);
        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y
        Debug.Log(move);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
        PointerUp = false; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp = true;
        PointerDown = false; 
        transform.localPosition = Vector3.zero; // Joystick returns to mean pos when not touched

        move = Vector3.zero;
    }
}
