//This enemy can track the player
//Still a work in progress, it only follows the player, it will fall off the platforms and locks on the player from the start of the level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 4f;
    Rigidbody rb;

    public float health = 3;
    public float maxHealth = 3;
    [SerializeField] private Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.value = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health / maxHealth;

        
    }

    private void FixedUpdate()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(target);
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
