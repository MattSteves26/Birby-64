using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    public Canvas EndLevel;
    [SerializeField] private int healthBoost = 1;
    [SerializeField] private float speedDeltaTime = 4; 



    
    public void OnTriggerEnter(Collider other){
        
        string oTag = other.gameObject.tag;

        switch(oTag)
        {
            case "Interactable_Health":
                Destroy(other.gameObject);
                GetComponent<playerHealth>().TakeDamage(-healthBoost);
                break;
            case "Interactable_Reset_Jump":
                Destroy(other.gameObject);
                GetComponent<playerMovement>().jumpsLeft = GetComponent<playerMovement>().maxJumps;
                break;
            case "Interactable_Speed":
                Destroy(other.gameObject);
                GetComponent<playerMovement>().moveSpeed *= 3f;
                GetComponent<playerMovement>().speedTimer = Time.time + speedDeltaTime;
                GetComponent<playerMovement>().speedBoost = true;
                break;

            case "Finish":
                EndLevel.enabled = true;
                break;
            default:
                Debug.Log("ERROR:NO TAG");
                break;
        }
        
        /*if(other.gameObject.layer == 9){ //if the player hits a coin, give them a double jump
            Destroy(other.gameObject);
            GetComponent<playerHealth>().TakeDamage(-healthBoost);
        }
        else if(other.gameObject.tag == "Finish")
        {
            EndLevel.enabled = true;
        }*/
    }
}
