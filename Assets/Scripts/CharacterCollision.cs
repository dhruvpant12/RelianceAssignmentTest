using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    CharacterStats character;
    CharacterMovement cc; //Reference to chracter movement.
    PlayerScore playerScore; //reference to player score.

    
   

    private void Awake()
    {
        character = GetComponent<CharacterStats>();
        cc = GetComponent<CharacterMovement>();
        playerScore = GameObject.Find("ScoreObject").GetComponent<PlayerScore>();

    }
    // Start is called before the first frame update
    

    private void OnTriggerEnter(Collider other)
    {
       
        
            if (other.gameObject.CompareTag("Obstacle") ) //If collision with Obstacles.
            {
                Destroy(other.gameObject);
                character.healthOfPlayer--; //If it below 0 , game is over.
                cc.canMove = false; //Stop movement, go to idle state and wait some time.
                StartCoroutine(StopPlayer());
                

            }
        

        if (other.gameObject.CompareTag("Coin"))//If collision with coins.
        {
            Destroy(other.gameObject);
            playerScore.coins++; //Increase Coin score.

        }
    }

     public IEnumerator StopPlayer()
    {
        cc.PlayIdleAnim();
        yield return new WaitForSeconds(2f);
        cc.canMove = true;
    }

}
