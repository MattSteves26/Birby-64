using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    public Canvas EndLevel;
    [SerializeField] private int healthBoost = 1;

    
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){ //if the player hits a coin, give them a double jump
            Destroy(other.gameObject);
            GetComponent<playerHealth>().TakeDamage(-healthBoost);
        }
        else if(other.gameObject.tag == "Finish")
        {
            EndLevel.enabled = true;
        }
    }
}
