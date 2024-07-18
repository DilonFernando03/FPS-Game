using System.Collections;
using System.Collections.Generic;
using InteractableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField]
    private GameObject pauseMenuUI, diedMenuUI, mainMenuUI, gameScreenUI, gameOverScreenUI, introText, domeHealText, SettingsMenuUI, crosshair, allGoldCollectedMessage, fullHealthText;
    [SerializeField]
    private DomeHealScript[] domeHealScripts;

    [SerializeField]
    private Player player;
    private int delay = 3;
    private bool messageDisplayed = false;
    private bool goalMessageDisplayed = false;

    [SerializeField]
    private CoinScript coinScript;


    void Start()
    {
        //disable all menus (and game screen) on startup, except the main menu
        pauseMenuUI.SetActive(false);
        diedMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        fullHealthText.SetActive(false);
        gameScreenUI.SetActive(false);
        gameOverScreenUI.SetActive(false);
        SettingsMenuUI.SetActive(false);

    }
    void Update()
    {
        //pause the game until the main menu is navigated through
        if (mainMenuUI.activeSelf == true)
        {
            Time.timeScale = 0f;
            isGamePaused = true;
            Cursor.visible = true;
        }

        //handles the pause menu
        if(Input.GetKeyDown(KeyCode.Escape) && (mainMenuUI.activeSelf == false) && (gameOverScreenUI.activeSelf == false) && (SettingsMenuUI.activeSelf == false))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        //displays full health on the health bar
        if (player.displayFullHealthText == true)
        {
            StartCoroutine(DisplayFullHealthText());
        }

        //displays a message for the healing domes
        foreach (DomeHealScript domeHealScript in domeHealScripts)
        {
            if (domeHealScript.playerInDome == true && messageDisplayed == false)
            {
                StartCoroutine(DisplayDomeHealText());
            }
        }

        //displays all the gold has been collected
        if (player.objectiveComplete == true && goalMessageDisplayed == false)
        {
            StartCoroutine(DisplayAllGoldCollectedText());
        }
    }

    //method resumes the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        crosshair.SetActive(true);
        gameScreenUI.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.visible = false;
    }

    //method pauses the game
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameScreenUI.SetActive(false);
        crosshair.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.visible = true;
    }

    //method quits the game
    public void Quit()
    {
        Application.Quit();
    }

    //method restarts the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //method starts the gameplay
    public void GameStart()
    {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.visible = false;
        gameScreenUI.SetActive(true);
        StartCoroutine(DisplayIntroText());
    }

    //method displays the settings menu
    public void EnableSettingsMenu()
    {
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameScreenUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
    }

    //method goes back from settings menu into the pause menu
    public void Return()
    {
        Pause();
        SettingsMenuUI.SetActive(false);
    }

    //method enables the dead menu when player dies
    public void EnableDeadMenu()
    {
        pauseMenuUI.SetActive(false);
        gameScreenUI.SetActive(false);
        diedMenuUI.SetActive(true);
        crosshair.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.visible = true;
    }

    //method enables the game over menu when the player wins
    public void DisplayGameOverUI()
    {
        pauseMenuUI.SetActive(false);
        diedMenuUI.SetActive(false);
        mainMenuUI.SetActive(false);
        crosshair.SetActive(false);
        gameScreenUI.SetActive(false);
        gameOverScreenUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.visible = true;
    }

    //displays the full health text for a number of seconds
    IEnumerator DisplayFullHealthText()
    {
        fullHealthText.SetActive(true);
        yield return new WaitForSeconds(delay);
        fullHealthText.SetActive(false);
        player.displayFullHealthText = false;
    }

    //displays the intro text for a number of seconds
    IEnumerator DisplayIntroText()
    {
        introText.SetActive(true);
        yield return new WaitForSeconds(delay);
        introText.SetActive(false);
    }

    //displays the dome heal text for a number of seconds
    IEnumerator DisplayDomeHealText()
    {
        domeHealText.SetActive(true);
        yield return new WaitForSeconds(delay);
        domeHealText.SetActive(false);
        messageDisplayed = true;
    }

    //displays the gold collected text for a number of seconds
    IEnumerator DisplayAllGoldCollectedText()
    {
        allGoldCollectedMessage.SetActive(true);
        yield return new WaitForSeconds(delay);
        allGoldCollectedMessage.SetActive(false);
        goalMessageDisplayed = true;
    }

    
}