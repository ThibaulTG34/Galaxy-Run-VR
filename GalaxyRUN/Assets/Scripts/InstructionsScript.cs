using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR;

public class InstructionsScript : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject xrRig;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public Camera main;
    public GameObject canvas_instructions;
    public XRNode centerEye;

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
                 if (results[i].gameObject.name == "Retour")
                {
                    results[i].gameObject.GetComponent<Button>().GetComponent<Image>().color = new Color(0, 0, 0);
                    obj = 5;
                }
            }

            switch (obj)
            {
                case 5:
                    if (Input.GetKey("joystick button 0"))
                        Back();
                    break;
                default:
                    break;
            }
        }
    }

    public void Back()
    {
        canvas_instructions.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
