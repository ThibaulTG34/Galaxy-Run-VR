using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapons : MonoBehaviour
{
    public Image bullet;
    public Image rocket;
    private bool swapWeapons;

    void Start()
    {
        swapWeapons = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("joystick button 1"))
        {
            swapWeapons = !swapWeapons;
        }

        if(!swapWeapons)
        {
            rocket.color = new Color(255, 255, 255, 255);
            bullet.color = new Color(255, 255, 0, 255);
        }
        else
        {
            bullet.color = new Color(255, 255, 255, 255);
            rocket.color = new Color(255, 255, 0, 255);

        }
    }
}
