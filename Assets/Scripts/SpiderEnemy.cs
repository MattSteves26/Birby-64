/*
Min and Max do not need to be set, they are the same at where you place the spider in the scene
Speed Changes how fast the spider moves
Bounce Determines how high the spider bounces
Travel Distance Determines how far the spider travels
For example, if u want the spider to move 6 on the x axis, set it to 6 and it will travel from where it was placed then to the right 6 over the x axis
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 1.0f;

    public float min;
    public float max;

    public float bounce;

    public float travelDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        min = transform.position.x;
        max = transform.position.x + travelDistance;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Ground")
        {
            rb.velocity = new Vector3(0, 0);
            rb.AddForce(new Vector3(0, bounce), ForceMode.Impulse);
        }
    }
}

