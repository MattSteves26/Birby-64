using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BansheeGz.BGSpline.Components;


public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform modelChild;
    public  bool isGrounded;
    public bool speedBoost= false;
    public int jumpsLeft = 6;
    public int maxJumps = 6;
    public float speedTimer = 0f; 
    public float moveSpeed = 300f; 
    
    private float xdist = 0;
    private float XAxisInput;
    private float ZAxisInput;
    private float leftAngle = 230;
    private float rightAngle = -40;
    private bool jumpKeyWasPressed = false;

    [SerializeField] private bool lockZAxis = false;
    [SerializeField] private float jumpForce = 13;
    [SerializeField] private float jumpTimer = 0f; 
    [SerializeField] private float jumpDeltaTime = 0.3f;    
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] BGCcMath spline;
    [SerializeField] private float splinespeed = 0.1f;

    public Animation anim;

    void start()
    {
        modelChild = this.gameObject.transform.GetChild(0);

        rb = GetComponent<Rigidbody>();       
    }

    void Update()
    {
        XAxisInput = Input.GetAxis("Horizontal");
        ZAxisInput = Input.GetAxis("Vertical");
        
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > jumpTimer){
            jumpKeyWasPressed = true;
            jumpTimer = Time.time + jumpDeltaTime;
            
        }
        
        //spline movement
        if (spline) {
            xdist += XAxisInput * splinespeed;
            Vector3 tangent;
            Vector3 newpos = spline.CalcPositionAndTangentByDistance(xdist, out tangent);
            transform.position = new Vector3(newpos.x, transform.position.y, newpos.z);
            transform.rotation = Quaternion.LookRotation(tangent);   
        }

        // Change rotation and do animation
        if (XAxisInput > 0) {
            //Would change to walking animation here, not ready yet, so just marking it here
            anim.Play();
            modelChild.localRotation = Quaternion.Euler(0,leftAngle,0);
        }
        else if (XAxisInput < 0) {
            anim.Play();
            modelChild.localRotation = Quaternion.Euler(0,rightAngle,0);
        }
        else {
            anim.Stop(); //Currently stops at random point, change to idle animation once that is ready
        }
        
    }

    void FixedUpdate()
    {
        //Reset Player Speed
        if(speedBoost && Time.time > speedTimer){
            moveSpeed *= (float)(1.0/3.0);
            speedBoost = false;
        }
        //not spline mmovement
        if (!spline && lockZAxis){
            rb.velocity = new Vector3(XAxisInput * moveSpeed * Time.deltaTime, rb.velocity.y, 0);
        }
        else if (!spline &&!lockZAxis){
            rb.velocity = new Vector3(XAxisInput * moveSpeed * Time.deltaTime, rb.velocity.y, ZAxisInput * moveSpeed * Time.deltaTime);
        }

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) //check if the player is on the ground, so we can control double jumping
        {
            if(jumpsLeft <= 0)
            {
                return;
            }
                        
        }
        else if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0){
            jumpsLeft = maxJumps;
            isGrounded = true;
        }

        if(jumpKeyWasPressed){
            float jumpFactor = ((float)jumpsLeft/(float)maxJumps);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce * jumpFactor, ForceMode.Impulse);
            jumpKeyWasPressed = false;
            jumpsLeft -= 1;
            isGrounded = false;
        }
    }
}