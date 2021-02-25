using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    private Rigidbody rb;
    private bool isGrounded;
    public float health = 3;
    public float maxHealth = 3;
    [SerializeField] private Slider healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar.value = health/maxHealth;
    }

    void Update(){
        healthBar.value = health/maxHealth;
    }

   
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector3.up * 50f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 8 && collision.gameObject.tag != "Projectile")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
