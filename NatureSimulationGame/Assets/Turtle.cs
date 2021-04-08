using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    float faceDirection = 0;

    public float moveSpeed;
    public Vector2 movement;
    public bool moveable = true;
    int horizontalMovementValue;
    int verticalMovementValue;
    public int randomDelay;
    public bool movingToObject = false;
    public Vector2 positionToMoveTo;
    GameObject visionArea;
    GameObject reachArea;
    public int hungerLevel = 1;

    void Start()
    {
        visionArea = transform.GetChild(0).gameObject;
        reachArea = transform.GetChild(1).gameObject;
        StartCoroutine(randomMovement());
        StartCoroutine(decreaseHunger());
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToObject == true)
        {
            //movement = (positionToMoveTo - rb.position).normalized;
            if (Mathf.Abs(rb.position.x - positionToMoveTo.x) > 0.3)
            {
                if (positionToMoveTo.x > rb.position.x)
                {
                    movement = new Vector2(1, 0);
                }
                else if (positionToMoveTo.x < rb.position.x)
                {
                    movement = new Vector2(-1, 0);
                }
            }
            else if (Mathf.Abs(rb.position.y - positionToMoveTo.y) > 0.3)
            {
                if (positionToMoveTo.y > rb.position.y)
                {
                    movement = new Vector2(0, 1);
                }
                else if (positionToMoveTo.y < rb.position.y)
                {
                    movement = new Vector2(0, -1);
                }
            }

            //if (Mathf.Abs(rb.position.x - positionToMoveTo.x) < 1 && Mathf.Abs(rb.position.y - positionToMoveTo.y) < 1)
            //{
            //    movingToObject = false;
            //}
        }
        else
        {
            movement.x = horizontalMovementValue;
            movement.y = verticalMovementValue;
        }
        

        if (movement.x > 0)
        {
            faceDirection = 3;
            visionArea.transform.localPosition = new Vector3(3.5f, 0, 0);
            reachArea.transform.localPosition = new Vector3(0.5f, 0, 0);
        }
        else if (movement.x < 0)
        {
            faceDirection = 2;
            visionArea.transform.localPosition = new Vector3(-3.5f, 0, 0);
            reachArea.transform.localPosition = new Vector3(-0.5f, 0, 0);
        }
        else if (movement.y > 0)
        {
            faceDirection = 1;
            visionArea.transform.localPosition = new Vector3(0, 3.5f, 0);
            reachArea.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
        else if (movement.y < 0)
        {
            faceDirection = 0;
            visionArea.transform.localPosition = new Vector3(0, -3.5f, 0);
            reachArea.transform.localPosition = new Vector3(0, -0.5f, 0);
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("FaceDirection", faceDirection);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void moveToObject(Vector2 objectPosition)
    {
        movingToObject = true;
        positionToMoveTo = objectPosition;
    }

    private IEnumerator randomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomDelay);
            if (movingToObject == false)
            {
                horizontalMovementValue = Random.Range(-1, 2);
                verticalMovementValue = Random.Range(-1, 2);
                if (horizontalMovementValue == 1 || horizontalMovementValue == -1 && verticalMovementValue == 1 || verticalMovementValue == -1)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        horizontalMovementValue = 0;
                    }
                    else
                    {
                        verticalMovementValue = 0;
                    }
                }

                if (Random.Range(0, 5) == 1)
                {
                    verticalMovementValue = 0;
                    horizontalMovementValue = 0;
                }
            }
            
        }
    }

    private IEnumerator decreaseHunger()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            if (hungerLevel >= 0)
            {
                hungerLevel -= 1;
            }

        }
    }
}
