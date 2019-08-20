using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    [SerializeField] float walkSpeed, jumpSpeed, climbSpeed;
    [SerializeField] Vector2 deathKick = new Vector2(7,7);

    bool playerAlive = true;
    Rigidbody2D playerBody;
    Animator Anim;
    CapsuleCollider2D PlayerBodyCollider;
    BoxCollider2D playerFeet;
    float gravityScaleStart;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        PlayerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();
        gravityScaleStart = playerBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerAlive) { return; }
        Run();
        CharFlip();
        Jump();
        ClimbLadder();
        Death();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * walkSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;

        bool playerSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        Anim.SetBool("Walking", playerSpeed);
        //print(playerVelocity);
    }

    private void ClimbLadder()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Anim.SetBool("Climbing", false);
            playerBody.gravityScale = gravityScaleStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerBody.velocity.x, controlThrow * climbSpeed);
        playerBody.velocity = climbVelocity;

        playerBody.gravityScale = 0f;

        bool ladderSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        Anim.SetBool("Climbing", ladderSpeed);

    }


    private void Jump()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }


        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpvelocityToadd = new Vector2(0f, jumpSpeed);
            playerBody.velocity += jumpvelocityToadd;

            Anim.SetBool("Jump", true);
        }
        else {
            Anim.SetBool("Jump", false);
        }
    }

    private void Death()
   {
        if (PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            playerAlive = false;
            Anim.SetTrigger("Death");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().playerDeath();
        }
   }


    private void CharFlip()
    {
        bool playerSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        if (playerSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(playerBody.velocity.x), 1f);
        }
    }

}// player
