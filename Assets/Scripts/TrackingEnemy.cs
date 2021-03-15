using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingEnemy : MonoBehaviour
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

    public float ranger = 50.0f;

    private bool onRange = false;

    int stopper = 0;

    public Vector3 pos1 = new Vector3(-4, 0, 0);
    public Vector3 pos2 = new Vector3(4, 0, 0);

    private bool isGrounded;

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

            stopper = 1;
        }

        else if(stopper == 0)
        {
            transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);

            onRange = Vector3.Distance(transform.position, player.position) < ranger;
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerHealth>().TakeDamage(damage);

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
