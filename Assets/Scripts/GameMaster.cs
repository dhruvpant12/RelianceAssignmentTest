using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject player;
    CharacterStats character;
    PlayerScore playerScore;


    public GameObject HUD;  //Main HUD which shows various score and has pause option.
    public Text distanceTravelledText;
    public Text highscoreText;
    public Text healthOfPlayerText;
    public Text timeText;
    public Text coinText;

    public GameObject PauseMenuCanvas; //Canvas when game is paused.
    

    public GameObject ResultCanvas; //When player dies , this canvas shows up.
    public Text resultDistanceTravelledText;
    public Text resultHighscoreText;    
    public Text resultTimeText;
    public Text resultCoinText;

    public bool isPaused; //Player enabled pause.

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        HUD.SetActive(true); //active
        PauseMenuCanvas.SetActive (false); //pause is disabled.
        ResultCanvas.SetActive(false); //resultcanvas is disabled.
        character = player.GetComponent<CharacterStats>();
        playerScore = GameObject.Find("ScoreObject").GetComponent<PlayerScore>();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(character.healthOfPlayer==0) //Game ending condition.
        {
            //Game is over.
            DisplayResultCanvas();

        }

        DisplayHUD();

    }

    
    void DisplayHUD() //Function for showing Main HUD
    {
        distanceTravelledText.text = "Distance :" + playerScore.GetScore().ToString();
        highscoreText.text = "HighScore :" + playerScore.GetHighScore().ToString();
        healthOfPlayerText.text = "Lives :" + character.healthOfPlayer.ToString();
        timeText.text = "Time : " + playerScore.GetTimeSpend().ToString();
        coinText.text = "Coin :" + playerScore.coins.ToString();
    }

    void DisplayResultCanvas()//Function to show Canvas when game is over.
    {
        Time.timeScale = 0;
        HUD.SetActive(false);
        ResultCanvas.SetActive(true);
        resultDistanceTravelledText.text = "Distance :" + playerScore.GetScore().ToString();
        resultHighscoreText.text = "HighScore :" + playerScore.GetHighScore().ToString();         
        resultTimeText.text = "Time : " + playerScore.GetTimeSpend().ToString();
        resultCoinText.text = "Coin :" + playerScore.coins.ToString();

    }

    public void PauseMenu()
    {
        if(!isPaused) //If game is not paused
        {
            Time.timeScale = 0;  //pause the game
            isPaused = true;
            PauseMenuCanvas.SetActive(true); //Enable pause menu
        }
        else          //If game is  paused
        {
            Time.timeScale = 1;  //resume game
            isPaused = false;

            PauseMenuCanvas.SetActive(false);//disable pause menu

        }
        


    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
