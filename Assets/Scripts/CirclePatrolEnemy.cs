//This Enemy is the Patrol enemy that will be used for circle levels
//This Can go in a circle and keeps going in a circle
//This goes in a circle, but how it gets placed in the scene stills needs to be tested more
//Prototype at the moment
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirclePatrolEnemy : MonoBehaviour
{
    private Rigidbody rb;
    public float health = 3;
    public float maxHealth = 3;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int damage = 1;

    public float timeCounter = 0;

    public float speed;
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        width = 30;
        height = 20;

        rb = GetComponent<Rigidbody>();
        healthBar.value = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        float x = Mathf.Cos(timeCounter) * width;
        float y = 0;
        float z = Mathf.Sin(timeCounter) * height;

        transform.position = new Vector3(x, y, z);
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
