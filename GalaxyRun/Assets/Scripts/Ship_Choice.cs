using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ship_Choice : MonoBehaviour
{
    [SerializeField]
    GameObject ship;
    [SerializeField]
    GameObject rightarrow;
    [SerializeField]
    GameObject leftarrow;
    [SerializeField]
    Image valider;
    [SerializeField]
    GameObject[] ships;
    int actual_ship;
    static GameObject ship_to_keep;
    // Start is called before the first frame update
    void Start()
    {
        actual_ship = 4;
    }

    // Update is called once per frame
    void Update()
    {
        ship_to_keep = ships[actual_ship];
        if (Mathf.Abs(Input.GetAxis("Yaw")) > 0.5)
            ship.transform.Rotate(new Vector3(0, Input.GetAxis("Yaw") * 0.6f, 0));

        if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            actual_ship++;
            if (actual_ship > ships.Length - 1)
            {
                actual_ship = 0;
            }
            Destroy(ship);
            GameObject new_ship = Instantiate(ships[actual_ship], ship.transform.position, ship.transform.rotation);
            ship = new_ship;
            leftarrow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
        }

        if (Input.GetKeyUp("joystick button 2") || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftarrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        /*for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("Button " + i + " was pressed!");
            }
        }*/

        if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            actual_ship--;
            if (actual_ship < 0)
            {
                actual_ship = ships.Length - 1;
            }
            Destroy(ship);
            GameObject new_ship = Instantiate(ships[actual_ship], ship.transform.position, ship.transform.rotation);
            ship = new_ship;
            rightarrow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);

        }
        if (Input.GetKeyUp("joystick button 3") || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightarrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Sélectionné");
            valider.GetComponent<Graphic>().color = new Color(0, 0, 1, 1);

        }
        if (Input.GetKeyUp("joystick button 0") || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            valider.GetComponent<Graphic>().color = new Color(1, 1, 1, 1);
            DontDestroyOnLoad(ship_to_keep);
            Debug.Log(ship_to_keep.name);
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }

    public static GameObject GetSelectedShip()
    {
        return ship_to_keep;
    }
}
