using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterInput characterInput;
    public float jumpForce;
    public float moveForce;
    public float maxSpeed;

    
    public float walkFactor;
    public float crawlFactor;

    private Rigidbody2D rbody;
    private Animator animator;
    private bool movementLocked;

    private float moveFactor;

    private bool isGrounded
    {
        get
        {
            return animator.GetBool("isGrounded");
        }
        set
        {
            animator.SetBool("isGrounded", value);
        }
    } private bool Crouched
    {
        get
        {
            return animator.GetBool("Crouched");
        }
        set
        {
            animator.SetBool("Crouched", value);
        }
    }    
    // Update is called once per frame
    private void Start()
    {
        characterInput = gameObject.GetComponent<CharacterInput>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (!movementLocked)
        {
            SwitchDirection();
            Walk();
            Jump();
            Crouch();
        }
        
    }
    private void Walk()
    {
        switch (Crouched)
        {
            case true:
                moveFactor = crawlFactor;
                break;
            case false:
                moveFactor = walkFactor;
                break;
        }

        if (rbody.velocity.magnitude < maxSpeed * moveFactor)
        {
            AddForce(characterInput.Direction * moveForce * moveFactor, 0f);            
        }

        if (rbody.velocity.x > 0.1f || rbody.velocity.x < -0.1f)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
                    
    }
    private void Jump()
    {
        if (characterInput.Jump && isGrounded)
        {
            AddForce(0f, jumpForce);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }
        if (rbody.velocity.y < 0.0f)
            animator.SetTrigger("Fall"); 
    }
    private void Crouch()
    {
        if (characterInput.Crouch && isGrounded)
        {
            Crouched = true;           
        }
        else
        {
            Crouched = false;            
        }
    }
    private void SwitchDirection()
    {
        if (isGrounded) {
            if (characterInput.Direction > 0.1f)
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (characterInput.Direction < -0.1f)
                this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }    
    
    private void AddForce(float xFac, float yFac)
    {
        rbody.AddForce(new Vector3(xFac,yFac));
    }
    private void SwitchMovementLock()
    {
        movementLocked =! movementLocked;
        Debug.Log("Movement is locked: " + movementLocked);
    }   
    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
