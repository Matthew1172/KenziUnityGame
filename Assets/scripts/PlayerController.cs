using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;
    private float moveSpeedStore;
    private float speedMilestoneCountStore;
    public float speedIncreaseMilestoneStore;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    private Rigidbody2D myRigidBody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    //private Collider2D myCollider;

    private Animator myAnimator;
    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
    }
    
    void Update()
    {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= speedMultiplier;
            moveSpeed *= speedMultiplier;
        }

        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }
        }

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {

                if (jumpTimeCounter > 0)
                {
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
            
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
        }

        myAnimator.SetFloat("speed", myRigidBody.velocity.x);
        myAnimator.SetBool("grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
        }
    }
}
