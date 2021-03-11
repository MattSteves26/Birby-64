using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_PlayerMovement : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public FB_GameManager game;

    public Rigidbody rb;
    public Transform modelChild;
    public Vector3 startPos;

    private bool jumpKeyWasPressed = false;

    [SerializeField] private float jumpForce = 13;
    [SerializeField] private float jumpTimer = 0f; 
    [SerializeField] private float jumpDeltaTime = 0.3f;    
    
    
    void start()
    {
        modelChild = this.gameObject.transform.GetChild(0);
        rb = GetComponent<Rigidbody>();     
        startPos = modelChild.position;  
        game = FB_GameManager.Instance;
    }

    void OnEnable()
    {
        FB_GameManager.OnGameStarted += OnGameStarted;
        FB_GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        FB_GameManager.OnGameStarted -= OnGameStarted;
        FB_GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    public void OnGameStarted()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }
    
    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        //transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }

    void Update()
    {
        if(game.GameOver) return;
        
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > jumpTimer){
            jumpKeyWasPressed = true;
            jumpTimer = Time.time + jumpDeltaTime;
            
        }
        
    }

    void FixedUpdate()
    {
        if(game.GameOver) return;

        if(jumpKeyWasPressed){
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpKeyWasPressed = false;
        }
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored(); //event sent to FB_GameManager;
        }
        if(coll.gameObject.tag == "DeadZone")
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            OnPlayerDied(); //event sent to FB_GameManager
        }
    }
}
