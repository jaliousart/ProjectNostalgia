using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public float Direction { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
    void Update()
    {
        GetDirection();
        GetJump();
        GetCrouch();
    }
    private void GetDirection()
    {
        Direction = Input.GetAxis("Horizontal");
        //Debug.Log("Direction is " + Direction);
    }
    private void GetJump()
    {
        if (Input.GetAxis("Jump") > 0.1f)
            Jump = true;
        else
            Jump = false;
        //Debug.Log("Jump is " + Jump);
    } 
    private void GetCrouch()
    {
        if (Input.GetAxis("Crouch") > 0.1f)
            Crouch = true;
        else
            Crouch = false;
        //Debug.Log("Crouch is " + Crouch);
    }    
}

