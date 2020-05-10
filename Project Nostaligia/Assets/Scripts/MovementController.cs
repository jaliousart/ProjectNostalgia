using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterInput characterInput;
    public float jumpForce;
    public float moveForce;

    private Rigidbody2D rbody;
    private bool isGrounded;
    
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
        if (characterInput.Jump && isGrounded)
        {            
            AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
            Debug.Log("Character jumped");
        }
        AddForce(new Vector2(moveForce * characterInput.Direction, 20f));
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
