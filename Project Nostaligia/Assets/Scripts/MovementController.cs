using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterInput characterInput;
    public float jumpForce;
    public float moveForce;
    public float maxSpeed;

    private Rigidbody2D rbody;    
    private bool isGrounded;

    public CharacterState CharacterState { private set; get; }
    
    // Update is called once per frame
    private void Start()
    {
        characterInput = gameObject.GetComponent<CharacterInput>();
        rbody = gameObject.GetComponent<Rigidbody2D>(); 
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        switch (CharacterState)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Walk:
                break;
            case CharacterState.Jump:
                break;
            case CharacterState.Fall:
                break;
            case CharacterState.Cling:
                break;
            case CharacterState.Crouch:
                break;
            case CharacterState.Crawl:
                break;
            case CharacterState.Slide:
                break;
            default:
                break;
        }
        //if (characterInput.Jump && isGrounded)
        //{
        //    AddForce(new Vector2(0f, jumpForce));
        //    isGrounded = false;
        //    Debug.Log("Character jumped");
        //}
        //if (isGrounded)
        //{
        //    if (rbody.velocity.magnitude <= maxSpeed)
        //    {
        //        AddForce(new Vector2(moveForce * characterInput.Direction, 0f));
        //    }
        //    else if (characterInput.Crouch)
        //    {

        //    }
        //}
    }
    private void AddForce(Vector2 direction)
    {
        rbody.AddForce(direction);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }   
}
