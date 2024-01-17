using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR;

public class MainMenuScript : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject xrRig;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public Camera main;

    public Button start;
    public Button instruction;
    public Button quit;

    public GameObject cursor;
    public GameObject canvas_instructions;
    public XRNode centerEye;
    public LineRenderer line;

    int obj = 0;
    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        canvas_instructions.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("joystick button 0") && SceneManager.GetActiveScene().name == "Logo")
        {
            SceneManager.LoadScene("MENU");
            Debug.Log("Open Menu...");
        }

        if (SceneManager.GetActiveScene().name == "MENU")
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            Vector3 point2D = main.WorldToScreenPoint(main.transform.position);
            m_PointerEventData.position = point2D;

            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                //cursor.transform.position = results[i].worldPosition;
                /*line.SetPosition(0, main.transform.position);
                line.SetPosition(1, results[i].worldPosition);*/
                //Debug.Log("ici");

                if (results[i].gameObject.name == "Start")
                {
                    results[i].gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0, 0, 0);
                    instruction.GetComponent<Image>().color = new Color(255, 255, 255);
                    quit.GetComponent<Image>().color = new Color(255, 255, 255);
                    obj = 1;
                }
                else if (results[i].gameObject.name == "Instructions")
                {
                    results[i].gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0, 0, 0);
                    start.GetComponent<Image>().color = new Color(255, 255, 255);
                    quit.GetComponent<Image>().color = new Color(255, 255, 255);
                    obj = 2;
                }
                else if (results[i].gameObject.name == "Quit")
                {
                    results[i].gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0, 0, 0);
                    start.GetComponent<Image>().color = new Color(255, 255, 255);
                    instruction.GetComponent<Image>().color = new Color(255, 255, 255);
                    obj = 3;
                }
                /*else if (results[i].gameObject.name == "Retour")
                {
                    results[i].gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0, 0, 0);
                    start.GetComponent<Image>().color = new Color(255, 255, 255);
                    instruction.GetComponent<Image>().color = new Color(255, 255, 255);
                    quit.GetComponent<Image>().color = new Color(255, 255, 255);
                    obj = 4;
                }*/
            }

            switch(obj)
            {
                case 1:
                    if (Input.GetKey("joystick button 0"))
                        StartGame();
                    break;
                case 2:
                    if (Input.GetKey("joystick button 0"))
                        OpenInstructions();
                    break;
                case 3:
                    if (Input.GetKey("joystick button 0"))
                        QuitGame();
                    break;
                default:
                    break;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game...");
    }

    /*public void Back()
    {
        canvas_instructions.SetActive(false);
        mainMenuUI.SetActive(true);
    }*/
    public void StartGame()
    {
        SceneManager.LoadScene("ChoixVaisseau");
        Debug.Log("Start Game...");
    }

    public void OpenInstructions()
    {
        /*SceneManager.LoadScene("Instructions");
        Debug.Log("Open Instructions...");*/
        canvas_instructions.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MENU");
        Debug.Log("Open Menu...");
    }
}

