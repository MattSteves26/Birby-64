//This enemy will just be placed into the scene, once in the scene, you can change the range it can detect the player and the speed
//Increasing the speed float can allow for the speed to be quicker or slower, and the range float can be changed for the player to be detected. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
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
    private bool goingRight;

    public float distance;
    public float speed = 1.0f;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.value = health / maxHealth;
    }

    void Update()
    {
        healthBar.value = health / maxHealth;
        distance = Vector2.Distance(transform.position, player.position);
        Debug.Log(distance);

        if (distance < range)
        {
            chasePlayer = true;
        }
        else
        {
            chasePlayer = false;
        }
    }


    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        if (chasePlayer && Time.time > moveTimer)
        {
            if (transform.position.x < player.position.x)
            {
                rb.velocity = new Vector3(speed, 0, 0);
                goingRight = true;
            }
            else
            {
                rb.velocity = new Vector3(-speed, 0, 0);
                goingRight = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerHealth>().TakeDamage(damage);
            if (goingRight == true)
            {
                rb.AddForce(new Vector3(-4, 1, 0), ForceMode.Impulse);
                moveTimer = Time.time + moveDeltaTime;
            }
            else
            {
                rb.AddForce(new Vector3(4, 1, 0), ForceMode.Impulse);
                moveTimer = Time.time + moveDeltaTime;
            }

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
