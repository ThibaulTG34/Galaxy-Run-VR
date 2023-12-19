using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class decompteScript : MonoBehaviour
{
    int nb = 3;
    public TMP_Text decompte;
    public GameObject mj;

    private void Start()
    {
        mj.GetComponent<MouvementJoueur>().enabled = false;
    }

    void Update()
    {
        if (nb > -1)
            StartCoroutine(decompteCoroutine());
        else
        {
            decompte.text = "Go !";
            mj.GetComponent<MouvementJoueur>().enabled = true;
            Destroy(gameObject);
        }
    }

    IEnumerator decompteCoroutine()
    {
        yield return new WaitForSeconds(1f);
        decompte.text = nb.ToString();
        nb -= 1;
        StopAllCoroutines();
    }
}
