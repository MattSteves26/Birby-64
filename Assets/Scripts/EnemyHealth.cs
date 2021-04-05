using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float health = 2;
    private float maxHealth;
    
    
    void Start()
    {
        maxHealth = health;
        healthBar.value = health/maxHealth;
    }

    void Update()
    {
        healthBar.value = health/maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}