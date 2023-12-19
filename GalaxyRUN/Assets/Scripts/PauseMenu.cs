using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject mj;

    void Update()
    {
        if (Input.GetKeyDown("joystick button 10"))
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

    public void Resume()
    {
        if(decompteScript.nb == -1)
            mj.GetComponent<MouvementJoueur>().enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // normal rate
        gameIsPaused = false;
    }

    public void Pause()
    {
        if (decompteScript.nb == -1)
            mj.GetComponent<MouvementJoueur>().enabled = false;
        pauseMenuUI.SetActive(true); // display pause menu
        Time.timeScale = 0f; // freeze game
        gameIsPaused = true;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ChoixVaisseau");
        Time.timeScale = 1f; // normal speed
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

