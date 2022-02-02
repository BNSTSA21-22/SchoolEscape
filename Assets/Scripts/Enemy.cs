using System;
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

    [SerializeField]
    private float endurance;

    private string ATTACK_ANIMATION = "Attacking";
    private string DIE_ANIMATION = "Dying";
    private string HIT_ANIMATION = "Hitting";
    private string RUN_ANIMATION = "Running";

    private Animator anim;
    private Transform player;
    private float movementForce = 10f;
    private float movementX;
    private SpriteRenderer sr;
    private AreaEffector2D ae;

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
        ae = GetComponent<AreaEffector2D>();

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
        float diff = transform.position.x - player.position.x;
        if (Math.Abs(diff) > 0.1)
        {
            if (movementX > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
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
                if (transform.position.x < 0)
                {
                    ae.forceAngle = 45;
                }
                else
                {
                    ae.forceAngle = 135;
                }

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
        currentHealth = currentHealth - (10 * (1 - endurance));
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
