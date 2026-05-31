using System;
using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] UnityEngine.Vector2 deathKick = new UnityEngine.Vector2 (10f,20f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    UnityEngine.Vector2 moveInput;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodycollider;

    float GravityScaleAtStart;
    Animator myAnimator;

    BoxCollider2D feetCollider;
    
    bool isAlive = true;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodycollider = GetComponent<CapsuleCollider2D>();
        GravityScaleAtStart = myRigidBody.gravityScale;
        feetCollider = GetComponent<BoxCollider2D>();
        
    }
    
    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
        
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}

        moveInput = value.Get<UnityEngine.Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (!isAlive) {return;}

        if (value.isPressed)
        {
            myRigidBody.linearVelocity += new UnityEngine.Vector2(0f, jumpSpeed);
        }
    }


    void Run()
    {
        UnityEngine.Vector2 playerVelocity = new UnityEngine.Vector2 (moveInput.x*moveSpeed, myRigidBody.linearVelocity.y) ;
        myRigidBody.linearVelocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed == true)
        {
            myAnimator.SetBool("IsRunning", true);
        }
        else
        {
            myAnimator.SetBool("IsRunning", false);
        }
        
    }
    
    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed == true)
        {
            transform.localScale = new UnityEngine.Vector2 (Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
        }
        
    }

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = GravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }

        myRigidBody.gravityScale = 0f;

        UnityEngine.Vector2 climbVelocity = new UnityEngine.Vector2 (myRigidBody.linearVelocity.x, moveInput.y * climbSpeed) ;
        myRigidBody.linearVelocity = climbVelocity;
        
        bool hasVerticalSpeed = Mathf.Abs(myRigidBody.linearVelocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", hasVerticalSpeed);
        
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive) {return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }



    void Die()
    {
        if (myBodycollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.linearVelocity = deathKick;

            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();

        }
    }

    
}
