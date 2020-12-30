using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    [Header("Ground Detection")]
    [SerializeField] Vector2 CastPos;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rbody;

    private bool directionRight;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            rbody.velocity = new Vector2(speed, 0f);
        }
        if (Input.GetAxis("Horizontal") < -0.01f)
        {
            rbody.velocity = new Vector2(-speed, 0f);
        }        
    }   
}
