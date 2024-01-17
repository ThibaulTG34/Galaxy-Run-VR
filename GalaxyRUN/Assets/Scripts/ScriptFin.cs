using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptFin : MonoBehaviour
{
    GameObject ship;
    public TMP_Text text_fin;
    void Start()
    {
        Vector3 pos = new(0f, -76.9000015f, 511.200012f);
        GameObject new_ship = Ship_Choice.GetSelectedShip();
        ship = Instantiate(new_ship, pos, Quaternion.identity);
    }

    void Update()
    {
        text_fin.text = "Votre score: "+MouvementJoueur.minutes+":"+MouvementJoueur.secondes;
    }
}
