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
    void Start()
    {
        animalBehavior = GetComponentInParent<AnimalBehavior>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        if (pickedUp)
        {
            if (playerScript.faceDirection == 0)
            {
                transform.position = playerTransform.position + new Vector3(0, -0.5f, 0);
                spriteRenderer.sortingOrder = 1;

            }
            else if (playerScript.faceDirection == 2)
            {
                transform.position = playerTransform.position + new Vector3(-0.3f, -0.5f, 0);
                spriteRenderer.sortingOrder = 1;
            }
            else if (playerScript.faceDirection == 3)
            {
                transform.position = playerTransform.position + new Vector3(0.3f, -0.5f, 0);
                spriteRenderer.sortingOrder = 1;
            }
            else if (playerScript.faceDirection == 1)
            {
                transform.position = playerTransform.position + new Vector3(0, -0.2f, 0);
                spriteRenderer.sortingOrder = 0;
            }

        }
    }

    public void pickedUpFollow(Transform playerTransform, Player playerScript)
    {
        pickedUp = true;
        this.playerTransform = playerTransform;
        this.playerScript = playerScript;
        animalBehavior.enabled = false;
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        animator.SetBool("PickedUp", true);
    }

    public void pickedUpStop()
    {
        pickedUp = false;
        animalBehavior.enabled = true;
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        //gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        animator.SetBool("PickedUp", false);
    }
}
