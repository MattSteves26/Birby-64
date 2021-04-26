//This enemy will just be placed into the scene, once in the scene, you can change the range it can detect the player and the speed
//Increasing the speed float can allow for the speed to be quicker or slower, and the range float can be changed for the player to be detected. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField] private int damage = 1;
    public float moveTimer;
    [SerializeField] public float moveDeltaTime = 1.5f;

    public Transform player;
    public float range = 7.0f;

    private bool chasePlayer = false;
    private bool goingRight;

    public float distance;
    public float speed = 1.0f;

    public Animator anim;
    public Transform model;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
       // Debug.Log(distance);

        if (distance < range)
        {
            chasePlayer = true;
            if (anim) {anim.SetBool("isActive", true);}
            
        }
        else
        {
            chasePlayer = false;
            if (anim) {anim.SetBool("isActive", false);}
            
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
                model.localRotation = Quaternion.Euler(0,90,0);
            }
            else
            {
                rb.velocity = new Vector3(-speed, 0, 0);
                goingRight = false;
                model.localRotation = Quaternion.Euler(0,-90,0);
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

}
