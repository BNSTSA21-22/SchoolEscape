using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float strength;

    private string ATTACK_ANIMATION = "Attacking";
    private string DIE_ANIMATION = "Dying";
    private string HIT_ANIMATION = "Hitting";
    private string RUN_ANIMATION = "Running";

    private Animator anim;
    private Transform player;
    private float movementForce = 10f;
    private float movementX;
    private SpriteRenderer sr;
    [SerializeField]
    uint damageColliderHash;
    // Health system variables
    [SerializeField]
    private Slider healthBar;
    private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField]
    private int nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();

        // Setup health system
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool(HIT_ANIMATION, false);
            anim.SetBool(RUN_ANIMATION, false);
            anim.SetBool(DIE_ANIMATION, true);
        } else if (!anim.GetBool(ATTACK_ANIMATION) && !anim.GetBool(HIT_ANIMATION))
        {
            MoveToPlayer();
            AnimatePlayer();
        }
    }

    void MoveToPlayer() {
        if (transform.position.x > player.position.x)
        {
            movementX = -1 * speed;
        }
        else
        {
            movementX = 1 * speed;
        }
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * movementForce;
    }
    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        anim.SetBool(RUN_ANIMATION, true);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hash=" + collision.otherCollider.GetShapeHash());
            if (collision.otherCollider.GetShapeHash() == damageColliderHash)
            {
                // Enemy is being hit!
                anim.SetBool(RUN_ANIMATION, false);
                anim.SetBool(HIT_ANIMATION, true);
                StartPlayerAttack();
            }
            else
            {
                // Enemy is attacking!
                anim.SetBool(RUN_ANIMATION, false);
                anim.SetBool(ATTACK_ANIMATION, true);
            }
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool(ATTACK_ANIMATION, false);
            anim.SetBool(HIT_ANIMATION, false);
            anim.SetBool(RUN_ANIMATION, true);
        }
    }

    // Invoked from animation event.
    void UpdateHealth()
    {
        currentHealth = currentHealth - (10 * (1 - speed));
        healthBar.value = currentHealth;
    }

    void NextLevel() {
        SceneManager.LoadScene(nextLevel);
    }

    void StartPlayerAttack()
    {
        player.gameObject.GetComponent<Player>().Attack();
    }

    void StartHittingPlayer()
    {
        player.gameObject.GetComponent<Player>().StartHitting(strength);
    }

    void StopHittingPlayer()
    {
        player.gameObject.GetComponent<Player>().StopHitting();
    }

}
