using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float movementForce = 10f;
    private float movementX;
  
    private SpriteRenderer sr;
    private float jumpForce = 10f;
    private Rigidbody2D rb;

    private string ATTACK_ANIMATION = "Attacking";
    private string DIE_ANIMATION = "Dying";
    private string HIT_ANIMATION = "Hitting";
    private string RUN_ANIMATION = "Running";
    private string victoryAnim = "Victory";
    private string DEAD_ANIMATION = "Dead";

    private bool allowMovement = true;

    [SerializeField]
    private Slider healthBar;
    private float maxHealth = 100f;
    private float currentHealth;

  // Start is called before the first frame update
    void Start()
    {
        // Setup health system
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Character Selection") || SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Victory Screen"))
        {
            allowMovement = false;
            healthBar.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool(HIT_ANIMATION, false);
            anim.SetBool(RUN_ANIMATION, false);
            anim.SetBool(DIE_ANIMATION, true);
        }
        else if (allowMovement)
        {
            MoveWithKeyboardInput();

            if (!anim.GetBool(ATTACK_ANIMATION) && !anim.GetBool(HIT_ANIMATION)) { 
                AnimatePlayer();
            Jump();
        }
        }
    }


    void MoveWithKeyboardInput()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * movementForce;
    }
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
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool(ATTACK_ANIMATION, false);
            anim.SetBool(HIT_ANIMATION, false);
            anim.SetBool(RUN_ANIMATION, true);
        }
    }

    public void StartHitting(float strength)
    {
        currentHealth = currentHealth - (10 * strength);
        healthBar.value = currentHealth;

        // Player is being hit!
        anim.SetBool(RUN_ANIMATION, false);
        anim.SetBool(HIT_ANIMATION, true);
    }

    public void StopHitting()
    {
        anim.SetBool(HIT_ANIMATION, false);
        anim.SetBool(RUN_ANIMATION, true);
    }

    public void Attack()
    {
        // Player is attacking!
        anim.SetBool(RUN_ANIMATION, false);
        anim.SetBool(HIT_ANIMATION, false);
        anim.SetBool(ATTACK_ANIMATION, true);
    }

    void leave()
    {
        SceneManager.LoadScene("Death Screen");
    }

    void idle()
    {
        anim.SetBool(victoryAnim, false);
        anim.SetBool(DEAD_ANIMATION, false);
    }
}
