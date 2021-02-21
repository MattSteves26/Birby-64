using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour{

    // Initial Variables
    private Rigidbody rb;
    private bool jumpKeyWasPressed;
    private float horizontlInput;
    [SerializeField] private bool lockZAxis = false;
    // SerializeField allows the varible to be set in Unity. I'm initializing these varibles here, because if they're initialized in start, then the values we set in unity is reset.
    [SerializeField] private int jumpCount = 1;
    [SerializeField] private float movespeed = 5f; 
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    
    //Jump vars
    public float jumpTimer;
    [SerializeField] public float jumpDeltaTime = 0.3f;      //how long to wait between jumps
    [SerializeField] public float fallMultiplier = 2f;
    
    public Transform modelchild;
    
    // Start is called before the first frame update
    void Start(){
        modelchild = this.gameObject.transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update(){
        float XAxisInput = Input.GetAxis("Horizontal");
        float ZAxisInput = Input.GetAxis("Vertical");
        // Jump and movement update, this makes it so the movement is set to occur at the next physics update, rather than this frame update.
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > jumpTimer && jumpCount > 0){
            rb.AddForce(Vector3.up * 450);
            jumpTimer = Time.time + jumpDeltaTime;
         }
        
        // speed up the fall of the jump to make it not feel as "floaty" (work in progress)
        if(rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        
        // Movement
        if (!lockZAxis) {
            transform.Translate(movespeed * XAxisInput * Time.deltaTime, 0f, movespeed * ZAxisInput * Time.deltaTime);
        } 
        else {
            transform.Translate(movespeed * XAxisInput  * Time.deltaTime, 0f, 0);
        }
        
        // Change rotation
        if (XAxisInput > 0) {
            modelchild.localRotation = Quaternion.Euler(0,110,0);
        }
        else if (XAxisInput < 0) {
            modelchild.localRotation = Quaternion.Euler(0,-110,0);
        } 
            
        // RESET
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level2");
        }
    }

    // Called once every physics update
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
   
    //COLLISION
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Enemy"){
            Destroy(gameObject);
            //Destroy(collision.gameObject); 
            
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){ //if the player hits a coin, give them a double jump
            Destroy(other.gameObject);
            jumpCount = 2;
        }
    }


}
