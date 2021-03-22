using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody rb;
    public float health = 3;
    public float maxHealth = 3;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int damage = 1;
    public float moveTimer;
    [SerializeField] public float moveDeltaTime = 1.5f;


    public Transform player;
    public float range = 50.0f;

    private bool onRange = false;

    public float distance;

    public Vector3 pos1 = new Vector3(-4, 0, 0);
    public Vector3 pos2 = new Vector3(4, 0, 0);
    public float speed = 1.0f;

    public Transform groundDetect;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.value = health / maxHealth;
    }

    void Update()
    {
        healthBar.value = health / maxHealth;

        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);

        onRange = Vector3.Distance(transform.position, player.position) < range;
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * 25);
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
