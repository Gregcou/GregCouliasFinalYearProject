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

    bool holdingObject = false;
    bool holdingGrass = false;

    public GameObject seedPacket;

    void Start()
    {

    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // sets the direction the player should face based on its movement
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

        // passes the movement to the animators blendtree through variables so use the correct directional animations
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        // animator uses for changing between idle and walking animations
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("FaceDirection", faceDirection);


        if (holdingObject == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pickUp = true;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
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


        if (drop == true && holdingGrass == true)
        {
            seedPacket.SetActive(false);
            holdingObject = false;
            holdingGrass = false;
        }


        if (holdingGrass)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pickUp = true;
            }
        }

        // control the side of the body the seed packet is on while carrying it
        if (holdingGrass == true)
        {
            if (faceDirection == 0)
            {
                seedPacket.transform.position = transform.position + new Vector3(0, -0.5f, 0);
                seedPacket.GetComponent<SpriteRenderer>().sortingOrder = 2;

            }
            else if (faceDirection == 2)
            {
                seedPacket.transform.position = transform.position + new Vector3(-0.3f, -0.5f, 0);
                seedPacket.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if (faceDirection == 3)
            {
                seedPacket.transform.position = transform.position + new Vector3(0.3f, -0.5f, 0);
                seedPacket.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if (faceDirection == 1)
            {
                seedPacket.transform.position = transform.position + new Vector3(0, -0.2f, 0);
                seedPacket.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        // move the player based on the current position, user input and movement speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Turtle")
        {
            if (pickUp == true)
            {
                holdingObject = true;
                other.gameObject.GetComponent<TurtleBehavior>().pickedUpFollow(transform,this);
            }
            else if(drop == true)
            {
                other.gameObject.GetComponent<TurtleBehavior>().pickedUpStop();
                holdingObject = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "PickedUp")
        {
            Debug.Log("turtle trigger");
            if (drop == true)
            {
                other.gameObject.GetComponent<TurtleBehavior>().pickedUpStop();
                holdingObject = false;
            }
        }
        else if (other.gameObject.tag == "Grass")
        {
            if (pickUp == true && holdingObject == false)
            {
                other.gameObject.GetComponent<SquareControl>().eatOrPickUpGrass();
                seedPacket.SetActive(true);
                holdingObject = true;
                holdingGrass = true;
            }
        }
        else if (other.gameObject.tag == "Square")
        {
            if (other.gameObject.GetComponent<SquareControl>().currentState == 0)
            {
                if (pickUp == true && holdingGrass == true)
                {
                    other.gameObject.GetComponent<SquareControl>().putDownGrass();
                    seedPacket.SetActive(false);
                    holdingObject = false;
                    holdingGrass = false;
                }
            }
        }


    }
}
