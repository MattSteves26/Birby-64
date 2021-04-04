using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime = .7f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyProjectile", lifeTime);
        
    }

    void LateUpdate()
    {
        if(transform.rotation.y < 0){
            rb.velocity += Vector3.left * speed * Time.deltaTime;
        }
        else if(transform.rotation.y > 0){
            rb.velocity += Vector3.right * speed * Time.deltaTime;
        }   
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player"){
            return;
        }
        else if(collision.gameObject.tag == "Enemy"){
            collision.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            DestroyProjectile();
        }
        else {
            DestroyProjectile();
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}

