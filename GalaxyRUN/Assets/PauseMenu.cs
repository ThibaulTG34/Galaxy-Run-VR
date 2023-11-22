using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.JoystickButton1)) // TODO check button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    void Resume()
    {
        Debug.Log("resume...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // normal rate
        //MouvementJoueur.speed = 0.0001f;
        gameIsPaused = false;
    }

    void Pause()
    {
        Debug.Log("pause...");
        pauseMenuUI.SetActive(true); // display pause menu
        Time.timeScale = 0f; // freeze game
        //MouvementJoueur.speed = 0.0f;
        gameIsPaused = true;

    }

    void RestartGame()
    {
        Debug.Log("restarting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // normal speed
    }

    void QuitGame()
    {
        Debug.Log("quitting game...");
        Application.Quit();
    }
}

