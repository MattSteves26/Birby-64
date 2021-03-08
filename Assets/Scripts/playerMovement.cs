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
    private float XAxisInput;
    private float ZAxisInput;
    private float leftAngle = 110;
    private float rightAngle = -110;
    private bool jumpKeyWasPressed = false;

    [SerializeField] private bool lockZAxis = false;
    [SerializeField] private int jumpsLeft = 6;
    [SerializeField] private int maxJumps = 6;
    [SerializeField] private float jumpForce = 13;
    [SerializeField] private float jumpForceFalling = 20.81f;
    [SerializeField] private float jumpTimer = 0f; 
    [SerializeField] private float jumpDeltaTime = 0.3f; 
    [SerializeField] private float movespeed = 300f; 
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    [SerializeField] BGCcMath spline;
    private float xdist = 0;
    [SerializeField] private float splinespeed = 0.1f;

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

        // Change rotation
        if (XAxisInput > 0) {
            modelChild.localRotation = Quaternion.Euler(0,leftAngle,0);
        }
        else if (XAxisInput < 0) {
            modelChild.localRotation = Quaternion.Euler(0,rightAngle,0);
        }
        
    }

    void FixedUpdate()
    {
        //not spline mmovement
        if (!spline && lockZAxis){
            rb.velocity = new Vector3(XAxisInput * movespeed * Time.deltaTime, rb.velocity.y, 0);
        }
        else if (!spline &&!lockZAxis){
            rb.velocity = new Vector3(XAxisInput * movespeed * Time.deltaTime, rb.velocity.y, ZAxisInput * movespeed * Time.deltaTime);
        }

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) //check if the player is on the ground, so we can control double jumping
        {
            Debug.Log("NotGrounded");
            if(jumpsLeft <= 0)
            {
                return;
            }
                        
        }
        else if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0){
            jumpsLeft = maxJumps;
            //jumpForce = 11;
            jumpForceFalling = 20.81f;
        }

        if(jumpKeyWasPressed){
            float jumpFactor = ((float)jumpsLeft/(float)maxJumps);
            if(rb.velocity.y < 0){
                jumpForceFalling = jumpForce - (rb.velocity.y);
                rb.AddForce(Vector3.up * jumpForceFalling * jumpFactor, ForceMode.Impulse);
            }
            else if(rb.velocity.y >= 0){
                rb.AddForce(Vector3.up * jumpForce * jumpFactor, ForceMode.Impulse);
            }
            jumpKeyWasPressed = false;
            jumpsLeft -= 1;
            //jumpForce -= 1;
        }
    }
}