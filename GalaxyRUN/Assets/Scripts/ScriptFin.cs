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
        Vector3 pos = new(12f, 142f, -104f);
        GameObject new_ship = Ship_Choice.GetSelectedShip();
        ship = Instantiate(new_ship, pos, Quaternion.identity);
        ship.transform.localScale *= 23f;
        ship.transform.Rotate(0, 45, 0);
    }

    void Update()
    {
        text_fin.text = "Votre score: "+MouvementJoueur.minutes+":"+MouvementJoueur.secondes;
    }
}
