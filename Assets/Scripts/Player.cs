using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB; //in Inspector Drag Player unto this (rigidbody)
    public float jumpForce;

    private bool isGrounded; // used with layers in Inspector to check if (def False)
    public Transform groundCheckPoint; // don't forget to drag Object at foot of
    public LayerMask whatIsGround; // allows to select layer from Inspector

    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR; //to enable us to flip the character 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //getaxisraw returns flat input
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        if (isGrounded){ canDoubleJump = true; }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            else
            {
                if(canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }

        }

        if(theRB.velocity.x < 0)
        {
            theSR.flipX = true; // Flip character when running left
        }
        else if(theRB.velocity.x > 0)
        {
            theSR.flipX = false; // Flip character when running right
        }

        // Get Animations accordingly
        anim.SetBool("isGrounded", isGrounded); // set Animator variable to animate jump!
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));

    }
}
