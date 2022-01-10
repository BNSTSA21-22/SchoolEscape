using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementForce = 10f;
    private float movementX;
  
    private SpriteRenderer sr;
    private float jumpForce = 10f;
    private Rigidbody2D rb;


  // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithKeyboardInput();
        AnimatePlayer();
        Jump();
    }


    void MoveWithKeyboardInput()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * movementForce;
    }
    private string RUN_ANIMATION = "running";
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();

    rb = GetComponent<Rigidbody2D>();

    }
       void AnimatePlayer()
    {
        if (movementX != 0)
        {
            if (movementX > 0)
            {
                sr.flipX = false;
            } else
            {
                sr.flipX = true;
            }
            anim.SetBool(RUN_ANIMATION, true);
        }
        else
        {
            anim.SetBool(RUN_ANIMATION, false);
        }
    }
    void FixedUpdate()
    {
        
    }
    
       void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }


    private bool isGrounded = true;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
