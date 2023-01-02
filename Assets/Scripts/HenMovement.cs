using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenMovement : MonoBehaviour
{
    public bool isMoving = false; 
    public float radius;
    public float minDistanceToPos;
    public float movementSpeed;
    public float rotationSpeed; 
    private Vector3 newPos;
    private Vector3 currentPos;
    private Vector3 looktoTarget; 

    private bool findNewPos = true;
    private bool movingToPos = false;

    private Animator _henAnimator; 

    // Start is called before the first frame update
    private void Start()
    {
        _henAnimator = GetComponent<Animator>(); 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            if (findNewPos)
            {
                _henAnimator.SetBool("Run", false);
                currentPos = transform.position;
                newPos = new Vector3(currentPos.x + Random.Range(-radius, radius), 0, currentPos.z + Random.Range(-radius, radius));
                looktoTarget = (newPos - currentPos).normalized;
                findNewPos = false;
                movingToPos = true;
            }
            if (movingToPos)
            {
                _henAnimator.SetBool("Run", true);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(looktoTarget), Time.deltaTime * rotationSpeed);

                transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * movementSpeed);
                if (Vector3.Distance(transform.position, newPos) < minDistanceToPos)
                {
                    movingToPos = false;
                    findNewPos = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.DrawLine(currentPos, newPos);

        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(newPos, 0.1f);
        
        
    }
}
