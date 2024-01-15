using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject mainMenuUI;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 10"))
        {
            SceneManager.LoadScene("MENU");
            Debug.Log("Open Menu...");
        }
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game...");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ChoixVaisseau");
        Debug.Log("Start Game...");
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
        Debug.Log("Open Instructions...");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MENU");
        Debug.Log("Open Menu...");
    }
}

