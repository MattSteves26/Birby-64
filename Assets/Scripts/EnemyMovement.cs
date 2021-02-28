using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;
    
    private bool movingRight = true;

    public Transform player;
    public float range = 50.0f;
  
    private bool onRange = false;

    public float distance;

    private Vector3 pos1 = new Vector3(-4, 0, 0);
    private Vector3 pos2 = new Vector3(4, 0, 0);
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
        if (collision.gameObject.layer != 8)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }


}
