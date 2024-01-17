using System.Collections;
using UnityEngine;
using TMPro;

public class decompteScript : MonoBehaviour
{
    public static int nb = 3;
    public TMP_Text decompte;
    public GameObject mj;

    private void Start()
    {
        mj.GetComponent<MouvementJoueur>().enabled = false;
    }

    void Update()
    {
        if (nb > -1)
        {
            StartCoroutine(decompteCoroutine());
        }
        else
        {
            decompte.text = "Go !";
            mj.GetComponent<MouvementJoueur>().enabled = true;
            //Destroy(gameObject);
        }

        /*if (PauseMenu.gameIsPaused)
        {
            //StopAllCoroutines();
            decompte.gameObject.SetActive(false);
        }
        else
        {
            //StartCoroutine(decompteCoroutine());
            decompte.gameObject.SetActive(true);
        }*/
    }

    IEnumerator decompteCoroutine()
    {
        if (!PauseMenu.gameIsPaused)
        {
            yield return new WaitForSeconds(1f);
            decompte.text = nb.ToString();
            nb -= 1;
            StopAllCoroutines();
        }
    }
}
