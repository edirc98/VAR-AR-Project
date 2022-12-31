using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FoxMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public RectTransform gamePad;
    public float moveSpeed = 0.05f;

    private Camera _ARCamera; 
    private GameObject _ARObject;
    private Vector3 move;

    bool running;

    void Start()
    {
        _ARObject = GameObject.FindGameObjectWithTag("Fox");
        _ARCamera = Camera.main; 
    }

    private void Update()
    {
       
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)gamePad.position, gamePad.rect.width * 0.5f);

        Vector3 joystickMovement = transform.localPosition.normalized;


        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y
        //move = _ARObject.transform.forward;

        Debug.Log(move);
        if (!running)
        {
            running = true;
            _ARObject.GetComponent<Animator>().SetBool("Run", true); // on drag start the Run animation
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // do the movement when touched down
        StartCoroutine(PlayerMovement());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero; // joystick returns to mean pos when not touched
        move = Vector3.zero;
        StopCoroutine(PlayerMovement());
        running = false;
        _ARObject.GetComponent<Animator>().SetBool("Run", false);
    }

    IEnumerator PlayerMovement()
    {
        while (true)
        {
            _ARObject.transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            if (move != Vector3.zero)
            {
                _ARObject.transform.rotation = Quaternion.Slerp(_ARObject.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 5.0f);
            }
            yield return null;
        }
    }
}
