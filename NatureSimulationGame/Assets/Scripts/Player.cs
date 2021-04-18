using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float faceDirection = 0;

    public float moveSpeed;
    Vector2 movement;
    public bool moveable = true;

    public bool pickUp = false;
    public bool drop = false;

    void Start()
    {

    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (animator.GetBool("Cast") == false)
        {
            if (movement.x > 0)
            {
                faceDirection = 3;
            }
            else if (movement.x < 0)
            {
                faceDirection = 2;
            }
            else if (movement.y > 0)
            {
                faceDirection = 1;
            }
            else if (movement.y < 0)
            {
                faceDirection = 0;
            }
        }


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("FaceDirection", faceDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Cast", true);
            moveSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("Cast", false);
            moveSpeed = 5;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            pickUp = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            pickUp = false;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            drop = true;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            drop = false;
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Turtle")
        {
            Debug.Log("turtle collide");
            if (pickUp == true)
            {
                other.gameObject.GetComponent<TurtleBehavior>().pickedUpFollow(transform,this);
            }
            else if(drop == true)
            {
                other.gameObject.GetComponent<TurtleBehavior>().pickedUpStop();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Turtle")
        {
            Debug.Log("turtle trigger");
            if (drop == true)
            {
                other.gameObject.GetComponent<TurtleBehavior>().pickedUpStop();
            }
        }
    }
}
