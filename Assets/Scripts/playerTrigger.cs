using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerTrigger : MonoBehaviour
{
    public Canvas EndLevel;
    [SerializeField] private int healthBoost = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float speedDeltaTime = 4; 
    [SerializeField] Text scoreText;
    [SerializeField] Text deadScoreText;
    int score;
    


    public void Start(){
        score = 0;
        scoreText.text = "Score: " + score;
    }
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
            case "ScoreZone":
                Destroy(other.gameObject);
                score++;
                scoreText.text = "Score: " + score;
                deadScoreText.text = "Score: " + score;
                break;
            case "Finish":
                if (LevelUnLock.unlockLevel < 8)
                {
                    LevelUnLock.unlockLevel++;
                    if(LevelUnLock.unlockLevel == 5)
                        LevelUnLock.unlockLevel++;
                }
                    
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
    void OnCollisionEnter(Collision collision)
    {
        int cLayer = collision.gameObject.layer;
         switch(cLayer)
         {
            case 9: // thorns layer
                GetComponent<playerHealth>().TakeDamage(damage);
                break;
            default:
                break;
         }
    }
}
