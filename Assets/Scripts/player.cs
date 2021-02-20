using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour{

    private Rigidbody rb;
    private bool jumpKeyWasPressed;
    private float horizontlInput ;
    //SerializeField allows the varible to be set in Unity. I'm initializing these varibles here, because if they're initialized in start, then the values we set in unity is reset.
    [SerializeField] private int jumpCount = 1;
    [SerializeField] private float movespeed = 2f; 
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start(){

        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update(){

        //Jump and movement update, this makes it so the movement is set to occur at the next physics update, rather than this frame update.
        if (Input.GetKeyDown(KeyCode.Space)){

            jumpKeyWasPressed=true; 
        }
        horizontlInput = Input.GetAxis("Horizontal");

    
        //RESET
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level2");
        }
    }

    //Called once every physics update
    private void FixedUpdate()
    {
        //rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.velocity = new Vector3(horizontlInput * movespeed, rb.velocity.y, 0);

        //make the player fall faster
        if(rb.velocity.y < 3){
            
            rb.AddForce(new Vector3(0, -1.0f, 0)*rb.mass*10);  
        }
        
        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) //check if the player is on the ground, so we can control double jumping
        {
            if(jumpCount == 1)
            {
                return;
            }
            
        }    
        //Jump and movement commands
        if (jumpKeyWasPressed){
            rb.AddForce(Vector3.up * 500);
            jumpKeyWasPressed = false;
            jumpCount = 1;
        }

    }

    //CLICK TO KILL PLAYER
    private void OnMouseDown(){

        Destroy(gameObject);
    }

    //COLLISION
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Destroy(collision.gameObject); 
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9) //if the player hits a coin, give them a double jump
        {
            Destroy(other.gameObject);
            jumpCount = 2;
        }
    }


}
