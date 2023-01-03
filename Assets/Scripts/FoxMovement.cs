using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoxMovement : MonoBehaviour
{
    public float MovementSpeed; 
    private Animator FoxAnimator;
    [SerializeField]
    private JoystickMovement _joysickMovement; 

    private void Awake()
    {
        FoxAnimator = GetComponent<Animator>();
        _joysickMovement = GameObject.Find("Joystick").GetComponent<JoystickMovement>();
    }
    void Start()
    {
        
    }

    private void Update()
    {
        if(_joysickMovement != null)
        {
            if (_joysickMovement.IsPointerDown)
            {
                //IF POINTER IS DOWN , FOX IS MOVING IN MOVE DIR
                Vector3 newPos = transform.position + _joysickMovement.JoystickDir; 
                transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * MovementSpeed);

                //APLY MOVE ANIMATION
                FoxAnimator.SetBool("Run", true);
            }
        }
    }
}
