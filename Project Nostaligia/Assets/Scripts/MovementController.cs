using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterInput characterInput;
    public float jumpForce;
    public float moveForce;
    public float walkSpeed;
    public float crawlSpeed;
   

    private Rigidbody2D rbody;
    private Animator animator;
    private bool movementLocked;

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
    }
    private bool Crouched
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
    private float Speed
    {
        get
        {
            return animator.GetFloat("Speed");
        }
        set
        {
            animator.SetFloat("Speed", value);
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
        float tanOfNormal = GetTanOfGround();
        if (rbody.velocity.magnitude < walkSpeed)
        {
            AddForce(characterInput.Direction * Mathf.Cos(tanOfNormal) * moveForce, Mathf.Sin(tanOfNormal) * moveForce);
            Debug.DrawRay(transform.position, new Vector3(characterInput.Direction * Mathf.Cos(tanOfNormal), Mathf.Sin(tanOfNormal)), Color.cyan);
        }

        SetCharacterFriction(tanOfNormal);
        SetCharacterSpeed();
    }
    private void Jump()
    {
        if (characterInput.Jump && isGrounded && !Crouched)
        {
            AddForce(0f, jumpForce);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }
        if (rbody.velocity.y < -0.5f)
            animator.SetTrigger("Fall");
    }
    private void Crouch()
    {
        if (characterInput.Crouch && isGrounded)
            Crouched = true;
        else
            Crouched = false;
    }
    private void SwitchDirection()
    {
        if (isGrounded)
        {
            if (characterInput.Direction > 0.1f)
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (characterInput.Direction < -0.1f)
                this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    private void SetCharacterSpeed()
    {
        switch (Crouched)
        {
            case true:
                Speed = Mathf.Abs(rbody.velocity.x);
                break;
            case false:
                Speed = Mathf.Abs(rbody.velocity.x);
                break;
        }
        Speed = Mathf.Round(Speed * 10f) / 10f;
    }
    private void SetCharacterFriction(float tan)
    {
        if (transform.localScale.x > 0)
        {
            rbody.sharedMaterial.friction = (1f / (Mathf.Abs(Mathf.Atan(tan) + 1f)));
        }
        else if (transform.localScale.x < 0)
        {
            rbody.sharedMaterial.friction = (1f / (Mathf.Abs(Mathf.Atan(tan) - 1f)));
        }
        else
        rbody.sharedMaterial.friction = 1f;
        Debug.Log("Friction Coeff " + rbody.sharedMaterial.friction);    
    }
    private float GetTanOfGround()
    {
        int layerMask = 1 << 8;

        Vector3 rayOrigon = transform.position + new Vector3(0f, 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigon, new Vector2(0f, -1f), 1f, layerMask);

        if (hit.collider != null)
            Debug.DrawRay(rayOrigon, new Vector3(0f, -1f) * hit.distance, Color.green);

        else
            Debug.DrawRay(rayOrigon, new Vector3(0f, -1f), Color.red);


        //Debug.Log(Mathf.Atan2(hit.normal.x, hit.normal.y));
        return Mathf.Atan2(hit.normal.x, hit.normal.y);
    }
    private void AddForce(float xFac, float yFac)
    {
        rbody.AddForce(new Vector3(xFac, yFac));
    }
    private void SwitchMovementLock()
    {
        movementLocked = !movementLocked;
        Debug.Log("Movement is locked: " + movementLocked);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }


}
