using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using BansheeGz.BGSpline.Components;
using PathCreation;

public class playerMovement : MonoBehaviour
{
    public bool isGrounded;
    public bool speedBoost= false;

    public int jumpsLeft = 6;
    public int maxJumps = 6;

    public float moveSpeed = 300f;
    public float speedTimer = 0f;  

    private bool jumpKeyWasPressed = false;
   
    private float tDist = 0; // for spline movement
    private float XAxisInput;
    private float ZAxisInput;
    private float leftAngle = -40;
    private float rightAngle = 40;

    [SerializeField] private bool lockZAxis = false;
    [SerializeField] private bool isSpline;

    [SerializeField] private float jumpForce = 13;
    [SerializeField] private float jumpTimer = 0f; 
    [SerializeField] private float jumpDeltaTime = 0.3f;    
    
    [SerializeField] private float splinespeed = 2f;
    public float splineLeftAngle = -60;
    public float splineRightAngle = 230;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform modelChild;
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private PathCreator pc;

    public Animator anim;
 

    void start()
    {
        modelChild = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
        XAxisInput = Input.GetAxis("Horizontal");
        ZAxisInput = Input.GetAxis("Vertical");
        
        anim.SetFloat("Xinput", Mathf.Abs(XAxisInput));
        
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > jumpTimer){
            jumpKeyWasPressed = true;
            jumpTimer = Time.time + jumpDeltaTime;
            anim.SetBool("Jumped", true);
        }
        else {
            anim.SetBool("Jumped", false);
        }

        // Change rotation
        if (XAxisInput > 0 && !isSpline) {
            modelChild.localRotation = Quaternion.Euler(0,rightAngle,0);
            transform.localRotation = Quaternion.Euler(0,180,0);
        }
        else if (XAxisInput < 0 && !isSpline) {
            modelChild.localRotation = Quaternion.Euler(0,leftAngle,0);
            transform.localRotation = Quaternion.Euler(0,0,0);

        }
        else if (XAxisInput > 0) {
            modelChild.localRotation = Quaternion.Euler(0,splineLeftAngle, 0);
        }
        else if (XAxisInput < 0) {
           modelChild.localRotation = Quaternion.Euler(0,splineRightAngle, 0);
        }

    }

    void FixedUpdate()
    {
        //Reset Player Speed after speed boost
        if(speedBoost && Time.time > speedTimer){
            moveSpeed *= (float)(1.0/3.0);
            speedBoost = false;
        }

        //spline movement
        if(isSpline){
            tDist = pc.path.GetClosestDistanceAlongPath(transform.position);
            tDist += XAxisInput * splinespeed * Time.deltaTime;
            //Debug.Log("Tdist: " + tDist);
            Vector3 newpos = pc.path.GetPointAtDistance(tDist);
            newpos.y = transform.position.y;
            //Debug.Log("newpos: " + newpos);
            transform.rotation = pc.path.GetRotationAtDistance(tDist);
            rb.MovePosition(newpos);    
        }

        //not spline mmovement
        if (!isSpline && lockZAxis){
            rb.velocity = new Vector3(XAxisInput * moveSpeed * Time.deltaTime, rb.velocity.y, 0);
        }
        else if (!isSpline &&!lockZAxis){
            rb.velocity = new Vector3(XAxisInput * moveSpeed * Time.deltaTime, rb.velocity.y, ZAxisInput * moveSpeed * Time.deltaTime);
        }

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) //check if the player is on the ground, so we can control double jumping
        {
            if(jumpsLeft <= 0)
            {
                return;
                anim.SetBool("Ground", false);
            }
                        
        }
        else if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0){
            jumpsLeft = maxJumps;
            isGrounded = true;
            anim.SetBool("Ground", true);
        }

        if(jumpKeyWasPressed){
            float jumpFactor = ((float)jumpsLeft/(float)maxJumps);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce * jumpFactor, ForceMode.Impulse);
            jumpKeyWasPressed = false;
            jumpsLeft -= 1;
            isGrounded = false;
            anim.SetBool("Ground", false);
        }
    }
    public bool V3Equal(Vector3 a, Vector3 b){
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }
}