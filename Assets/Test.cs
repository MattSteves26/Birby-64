using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour{


    //Initial Variables
    Rigidbody rb;
    public float movespeed;

    //Jump vars
    public float jumpTimer;
    public float jumpDeltaTime = 0.3f;      //how long to wait between jumps
    public float fallMultiplier = 2f;



    // Start is called before the first frame update
    void Start(){

        rb = GetComponent<Rigidbody>();
        movespeed = 5f;
        
    }

    // Update is called once per frame
    void Update(){

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > jumpTimer){
            rb.AddForce(Vector3.up * 450);
            jumpTimer = Time.time + jumpDeltaTime;
         }
        
        //speed up the fall of the jump to make it not feel as "floaty" (work in progress)
        if(rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        

        //Movement
        transform.Translate(movespeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, movespeed * Input.GetAxis("Vertical") * Time.deltaTime);


    }




    //COLLISION
    private void OnCollisionEnter(Collision collision)
    {
           //Die when touching an enemy
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Destroy(collision.gameObject);
        }
    }
}
