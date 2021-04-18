//you can use the speed to change how fast you go 
//In the pos1 and pos2, you declare where the enemy starts and where the enemy ends, you can't drag and drop
//You can change these in the Unity editor on the right
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody rb;
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
    }

    void Update()
    {
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



}
