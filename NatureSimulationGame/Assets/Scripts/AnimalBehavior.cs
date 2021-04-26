using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    public int animalNum;
    public AnimalManager manager;
    public Rigidbody2D rb;
    public Animator animator;
    BoxCollider2D boxCollider;
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

    public int growthStage = 0;
    public bool canHaveChild = false;
    public bool beingMovedTowards = false;
    public string animalName;

    void Start()
    {
        manager = GameObject.Find("AnimalManager").GetComponent<AnimalManager>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
        visionArea = transform.GetChild(0).gameObject;
        reachArea = transform.GetChild(1).gameObject;
        StartCoroutine(randomMovement());
        StartCoroutine(decreaseHunger());
        StartCoroutine(growUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveable == true)
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
                boxCollider.size = new Vector2(0.96875f, 0.6875f);
            }
            else if (movement.x < 0)
            {
                faceDirection = 2;
                visionArea.transform.localPosition = new Vector3(-3.5f, 0, 0);
                reachArea.transform.localPosition = new Vector3(-0.5f, 0, 0);
                boxCollider.size = new Vector2(0.96875f, 0.6875f);
            }
            else if (movement.y > 0)
            {
                faceDirection = 1;
                visionArea.transform.localPosition = new Vector3(0, 3.5f, 0);
                reachArea.transform.localPosition = new Vector3(0, 0.5f, 0);
                boxCollider.size = new Vector2(0.6875f, 0.96875f);
            }
            else if (movement.y < 0)
            {
                faceDirection = 0;
                visionArea.transform.localPosition = new Vector3(0, -3.5f, 0);
                reachArea.transform.localPosition = new Vector3(0, -0.5f, 0);
                boxCollider.size = new Vector2(0.6875f, 0.96875f);
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetFloat("FaceDirection", faceDirection);
        }
        

        

        if (hungerLevel < 0)
        {
            death();
        }

        if (growthStage == 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if (growthStage == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (growthStage == 2)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


    private void death()
    {
        manager.animals.Remove(gameObject);
        Destroy(gameObject);
    }

    public void moveToObject(Vector2 objectPosition)
    {
        movingToObject = true;
        positionToMoveTo = objectPosition;
    }

    public void haveChild()
    {
        manager.haveChild(transform.position,animalName);
        canHaveChild = false;
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
            yield return new WaitForSeconds(40);
            if (hungerLevel >= 0)
            {
                hungerLevel -= 1;
            }

        }
    }

    private IEnumerator growUp()
    {
        if (growthStage == 0)
        {
            yield return new WaitForSeconds(10);
            growthStage = 1;
        }
        yield return new WaitForSeconds(10);
        growthStage = 2;
        //yield return new WaitForSeconds(15);
        canHaveChild = true;
        yield return new WaitForSeconds(60);
        death();
    }
}
