using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//There are two jump functions in this code, needs to be fixed
public class player : MonoBehaviour{

    // Initial Variables
    private Rigidbody rb;
    private float horizontlInput;
    [SerializeField] float distToGround;
    [SerializeField] private bool lockZAxis = false;
    // SerializeField allows the varible to be set in Unity. I'm initializing these varibles here, because if they're initialized in start, then the values we set in unity is reset.
    [SerializeField] private int jumpCount = 1;
    [SerializeField] private int maxJumps = 6;
    [SerializeField] private float jumpforce = 1000;
    [SerializeField] private float gravity = 1750;
    [SerializeField] private float fastFallSpeed = 3;
    [SerializeField] private float jumpGravity = 1000;
    [SerializeField] private float movespeed = 5f; 
    [SerializeField] private float terminal = -10; 
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    
    //Jump vars
    public float jumpTimer;
    [SerializeField] public float jumpDeltaTime = 0.3f;      //how long to wait between jumps
    [SerializeField] public float fallMultiplier = 2f;
    
    public Transform modelchild;

    //UI
    public Canvas DeadPlayer;
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > jumpTimer && jumpCount < maxJumps){
            //Factor of weakening jumps
            float jumpFactor = ((float)maxJumps - (float)jumpCount)/ (float)maxJumps;
            //jumpFactor = jumpFactor / ((float) jumpCount+ 1);
            Debug.Log(jumpFactor);
            rb.AddForce(new Vector3(0,-rb.velocity.y,0));
            //like jumping off a platform, reset vertical speed
            rb.AddForce(Vector3.up * jumpforce * jumpFactor);
            jumpTimer = Time.time + jumpDeltaTime;
            jumpCount += 1;
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

        //make the player fall faster along with having a terminal velocity
        if (rb.velocity.y > terminal) {
            if(rb.velocity.y < fastFallSpeed){
                
                rb.AddForce(new Vector3(0, -1.0f, 0)* rb.mass* gravity * Time.deltaTime);  
            }
            else {
                rb.AddForce(new Vector3(0, -1.0f, 0)* rb.mass* jumpGravity * Time.deltaTime);  
            }
        }
        
        //Still not perfect ground detection :/
        if(Physics.Raycast(groundCheckTransform.position, -Vector3.up, distToGround)) //check if the player is on the ground, so we can control double jumping
        {
            jumpCount = 0;
            
        }    
        
        // speed up the fall of the jump to make it not feel as "floaty" (work in progress)
        /*if(rb.velocity.y > -10){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }*/

    }
   
    //COLLISION
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Enemy"){
            Destroy(gameObject);
            DeadPlayer.enabled = true;
            //Destroy(collision.gameObject); 
            
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){ //if the player hits a coin, give them a double jump
            Destroy(other.gameObject);
            maxJumps += 1;
        }
    }


}
