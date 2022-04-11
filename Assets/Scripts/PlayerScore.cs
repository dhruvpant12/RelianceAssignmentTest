using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{

    CharacterMovement cc; //reference to character movement.
    
    public int coins; //Score for coins.

    private string _playerPrefHighScore;
    private int highScore;//Highscore saved in PlayerPref
    private int score; //Score for travelling distance.
    private float speed;
    private Vector3 startingPosition;
    private Vector3 currentNode;
    private float timeSpend;


   

    private void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Checking if Highscore PlayerPref exists or not.
        if(PlayerPrefs.HasKey(_playerPrefHighScore))
        {
            highScore = PlayerPrefs.GetInt(_playerPrefHighScore);
        }
        else
        {
            PlayerPrefs.SetInt(_playerPrefHighScore, 0);
        }

        score = 0;
        coins = 0;
        speed = cc.GetSpeed(); //Speed of Player.
        startingPosition = cc.startingposition; //Starting position of player. used to measure distance travelled.


    }

    // Update is called once per frame
    void Update()
    {
        CalculateScore();
        
    }

    void CalculateScore()
    {
        currentNode = cc.GetCurrentPosition(); //present position of player.
        score = CalculateDistance(currentNode);
        SettingHighScore();
    }

    int CalculateDistance(Vector3 currentNode)
    {
        int distanceTravelled = Mathf.Abs((int)(currentNode.z - startingPosition.z)); //Calculate distance travelled from starting.
        timeSpend = distanceTravelled / speed; 
        return distanceTravelled;
    }

    void SettingHighScore()
    {
        if(score>highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(_playerPrefHighScore, highScore);
            //Update HighSCore UI
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetTimeSpend()
    {
        return (int)timeSpend;
    }
}
