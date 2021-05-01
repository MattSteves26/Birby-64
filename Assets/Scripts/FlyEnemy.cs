/*﻿
The way this file works is state the starting position where you want to fly to stay
The best way to do this is to make the area where you place it in the scene the same as the starting position
Then use the length and width to decide the size of the figure-8
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public float timeCounter = 0;

    public float speed;
    public float width;
    public float height;

    public Vector3 startPos;

    [SerializeField] private int damage = 1;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        width = 4;
        height = 4;

        Vector3 startPos = transform.position;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = startPos.x + Mathf.Cos(timeCounter) * width;
        float y = startPos.y + Mathf.Sin(timeCounter * 2) * height / 2 ;
        float z = startPos.z;

        transform.position = new Vector3(x, y, z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerHealth>().TakeDamage(damage);

        }
    }
}
