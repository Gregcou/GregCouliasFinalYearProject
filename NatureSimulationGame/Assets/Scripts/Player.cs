using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    float faceDirection = 0;

    public float moveSpeed;
    Vector2 movement;
    public bool moveable = true;

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

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
