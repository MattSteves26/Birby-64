//This is a script for the enemy movement universally.
//It will allow the enemy to move along a slpine in a lerp or follow the player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyMovement : MonoBehaviour
{
    public EndOfPathInstruction endOfPathInstruction;
    private bool knockback = false;
    private float knockbackTimer;
    private float knockbackTimerDelta = 1.2f;
    private float tDist = 0;
    private float ms;//local movespeed
    [SerializeField] private bool isSpline = true;
    [SerializeField] private bool chasePlayer = false;


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damage = 1;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private PathCreator pc;
    [SerializeField] private Transform modelChild;


    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        ms = moveSpeed;
        modelChild = this.gameObject.transform.GetChild(0);
    }
    
    void FixedUpdate()
    {
        if(isSpline){
            if(knockbackTimer > Time.time +.7){ //move transform backwards quickly for .5 sec
                ms = -3 * moveSpeed;
            }
            else if(knockbackTimer > Time.time){ //stop movement for .7 sec
                ms = 0;
            }
            else{ //reset to normal movement
                ms = moveSpeed; 
                knockback = false;
            }
            tDist += ms * Time.deltaTime;
            Vector3 newPos = pc.path.GetPointAtDistance(tDist, endOfPathInstruction);
            //Debug.Log("Bee newpos: " + newPos);
            if(!knockback){
                transform.LookAt(newPos);}
            transform.position = newPos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<playerHealth>().TakeDamage(damage);
            knockbackTimer = Time.time + knockbackTimerDelta;
            knockback = true;

        } 
    }
    public bool V3Equal(Vector3 a, Vector3 b){
        return Vector3.SqrMagnitude(a - b) < 0.001;
    }
}
