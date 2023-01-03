using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FoxMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public ParticleSystem HenDeathParticles; 
    [SerializeField]
    private Animator FoxAnimator;
    [SerializeField]
    private JoystickMovement _joysickMovement;
    private int score=0;
    private TextMeshProUGUI scoreText;


    private Vector3 looktoTarget;


    private void Awake()
    {
        FoxAnimator = GetComponent<Animator>();
        _joysickMovement = GameObject.Find("Joystick").GetComponent<JoystickMovement>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
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
                //Movement
                Vector3 newPos = transform.position + _joysickMovement.JoystickDir; 
                transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * MovementSpeed);

                //Rotation
                looktoTarget = (newPos - transform.position).normalized;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(looktoTarget), Time.deltaTime * RotationSpeed);

                //APLY MOVE ANIMATION
                FoxAnimator.SetBool("Run", true);
            }
            else if (_joysickMovement.IsPointerUp)
            {
                FoxAnimator.SetBool("Run", false);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Hen") == true)
        {
            //FoxAnimator.SetBool("Attack", true); 
            //We have collided with a Hen, so we kill it
            HenDeathParticles.Play();
            Destroy(other.gameObject);
            //FoxAnimator.SetBool("Attack", false);
            score += 1;
            scoreText.text = "Eaten chickens: " + score.ToString();
        }
    }
}
