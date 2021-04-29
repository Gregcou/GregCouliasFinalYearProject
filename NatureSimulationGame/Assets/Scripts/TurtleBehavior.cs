using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehavior : MonoBehaviour
{
    AnimalBehavior animalBehavior;
    public bool pickedUp = false;
    Transform playerTransform;
    Player playerScript;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject visionArea;
    public GameObject reachArea;
    void Start()
    {
        animalBehavior = GetComponentInParent<AnimalBehavior>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        // keeps the turtle in front of the players body while being carried
        if (pickedUp)
        {
            if (playerScript.faceDirection == 0)
            {
                transform.position = playerTransform.position + new Vector3(0, -0.5f, 0);
                spriteRenderer.sortingOrder = 2;

            }
            else if (playerScript.faceDirection == 2)
            {
                transform.position = playerTransform.position + new Vector3(-0.3f, -0.5f, 0);
                spriteRenderer.sortingOrder = 2;
            }
            else if (playerScript.faceDirection == 3)
            {
                transform.position = playerTransform.position + new Vector3(0.3f, -0.5f, 0);
                spriteRenderer.sortingOrder = 2;
            }
            else if (playerScript.faceDirection == 1)
            {
                transform.position = playerTransform.position + new Vector3(0, -0.2f, 0);
                // makes the turtle appear behind the player when walking up
                spriteRenderer.sortingOrder = 0;
            }

        }
    }

    // stops the turtle moving or interacting with anything while being carried
    public void pickedUpFollow(Transform playerTransform, Player playerScript)
    {
        pickedUp = true;
        this.playerTransform = playerTransform;
        this.playerScript = playerScript;
        animalBehavior.moveable = false;
        gameObject.tag = "PickedUp";
        visionArea.SetActive(false);
        reachArea.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        animator.SetBool("PickedUp", true);
    }

    public void pickedUpStop()
    {
        pickedUp = false;
        animalBehavior.moveable = true;
        gameObject.tag = "Turtle";
        visionArea.SetActive(true);
        reachArea.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        animator.SetBool("PickedUp", false);
    }
}
