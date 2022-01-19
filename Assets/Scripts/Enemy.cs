using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private string RUN_ANIMATION = "Running";
    private string ATTACK_ANIMATION = "Attacking";
    private Animator anim;
    private Transform player;
    private float movementForce = 10f;
    private float movementX;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        AnimatePlayer();
 
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
            anim.SetBool(RUN_ANIMATION, false);
            anim.SetBool(ATTACK_ANIMATION, true);
        }
    }
    
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool(ATTACK_ANIMATION, false);
            anim.SetBool(RUN_ANIMATION, true);
        }
    }
}
