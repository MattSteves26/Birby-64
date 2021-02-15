using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour{

    Rigidbody rb;
    public float movespeed;
    // Start is called before the first frame update
    void Start(){

        rb = GetComponent<Rigidbody>();
        movespeed = 2f;
        
    }

    // Update is called once per frame
    void Update(){


        if (Input.GetKeyDown(KeyCode.Space)){

            rb.AddForce(Vector3.up * 500);
            //Destroy(gameObject);
        }

        //MOVEMENT
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * 100);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(Vector3.back * 100);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * 100);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector3.left * 100);
        }*/
        transform.Translate(movespeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, movespeed * Input.GetAxis("Vertical") * Time.deltaTime);

        //RESET
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level2");
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
}
