/*
this code works very similarly to how the catepiller enemy
you can change the speed with the speed float
you also need to set the tracking of the enemy
set the player variable to the Player prefab to start the tracking
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeEnemy : MonoBehaviour
{

    private Rigidbody rb;
    public float health = 3;
    public float maxHealth = 3;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int damage = 1;
    public float moveTimer;
    [SerializeField] public float moveDeltaTime = 1.5f;

    public Transform player;
    public float range = 7.0f;

    private bool chasePlayer = false;
    

    public float distance;
    public float speed = 1.0f;

    public bool SnakeJump;
    public float jumpDistance;
    public float jumpRange;
    int jump = 0;
    public Vector3 jumping;
    public float jumpForce = 2.0f;
    public float jumpForceAttack = 6.0f;
    int jumpAttack = 0;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.value = health / maxHealth;

        jumping = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void Update()
    {
        healthBar.value = health / maxHealth;
        distance = Vector2.Distance(transform.position, player.position);
        jumpDistance = Vector2.Distance(transform.position, player.position);
        Debug.Log(distance);
        Debug.Log(jumpDistance);

        if (distance < range)
        {
            chasePlayer = true;
        }
        else
        {
            chasePlayer = false;
        }

        if (jumpDistance < jumpRange)
        {
            SnakeJump = true;
        }
        else
        {
            SnakeJump = false;
        }


    }


    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {

        if (SnakeJump == true && jump == 0)
        {
            rb.AddForce(jumping * jumpForce, ForceMode.Impulse);

            jump = 1;
        }

        if (chasePlayer && Time.time > moveTimer && jumpAttack == 0)
        {
           
            {
                rb.AddForce(jumping * jumpForceAttack, ForceMode.Impulse);
                rb.velocity = new Vector3(-speed, 0, 0);
                
            }

            jumpAttack = 1;

        }

        if (chasePlayer && Time.time > moveTimer && jumpAttack == 1)
        {
           
            {
                rb.velocity = new Vector3(-speed, 0, 0);
                
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerHealth>().TakeDamage(damage);
         
            rb.AddForce(new Vector3(4, 1, 0), ForceMode.Impulse);
            moveTimer = Time.time + moveDeltaTime;
    
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}