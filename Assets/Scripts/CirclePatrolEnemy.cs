//This code is not used in final Version
//This code simulates a circle
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

    public float x;
    public float y;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.value = health / maxHealth;
        transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        x = Mathf.Cos(timeCounter) * width;
        
        z = Mathf.Sin(timeCounter) * height;

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
