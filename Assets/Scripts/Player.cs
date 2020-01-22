using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance; //singleton to manage pushback

    public float moveSpeed;
    public Rigidbody2D theRB; //in Inspector Drag Player unto this (rigidbody)
    public float jumpForce;

    private bool isGrounded; // used with layers in Inspector to check if (def False)
    public Transform groundCheckPoint; // don't forget to drag Object at foot of
    public LayerMask whatIsGround; // allows to select layer from Inspector

    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR; //to enable us to flip the character

    public float knockBackLength, knockBackForce; // push back when damage
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.instance.isPaused && !stopInput)
        { 
            //only give user control when we are not "knocking back damage"
            if (knockBackCounter <= 0)
            {

                //getaxisraw returns flat input
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
                if (isGrounded) { canDoubleJump = true; }

                // Jump
                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(10);
                        }
                    }

                }

                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true; // Flip character when running left
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false; // Flip character when running right
                }

            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    //we're facing right, push back to left
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);

                }
            }

            // Get Animations accordingly
            anim.SetBool("isGrounded", isGrounded); // set Animator variable to animate jump!
            anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0, knockBackForce);
        anim.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
