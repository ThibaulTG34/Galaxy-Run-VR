using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listeCheckPoints = new List<GameObject>();
    [SerializeField] public static GameObject Respawn;
    [SerializeField] GameObject progressHoldClick;
    int nextCheckpoint = 0;
    Vector3 positionInitiale;
    public GameObject mj;
    [SerializeField]
    ParticleSystem speedEffect;

    public Canvas respawn_text;
    private bool respawned;

    // Start is called before the first frame update
    void Start()
    {
        GameObject new_ship = Ship_Choice.GetSelectedShip();
        string str = new_ship.name;
        Debug.Log("ddddddd : "+str);
        progressHoldClick = transform.Find(str + "(Clone)/Respawn/prgressHoldClick").gameObject;
        Respawn = transform.Find(str+ "(Clone)/Respawn").gameObject;

        progressHoldClick.GetComponent<RectTransform>().localScale = new Vector3(0, progressHoldClick.transform.localScale.y, progressHoldClick.transform.localScale.z);
        Respawn.SetActive(false);

        for (int i = 0; i < listeCheckPoints.Count; i++)
        {
            listeCheckPoints[i].SetActive(false);
        }
        speedEffect.Stop();
        listeCheckPoints[0].SetActive(true);

        respawn_text.gameObject.SetActive(false);

    }

    void Update()
    {

        /*for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("Button " + i + " was pressed!");
            }
        }*/

        if(Input.GetKeyDown("joystick button 0") && respawned)
        {
            respawn_text.gameObject.SetActive(false);
        }

        if(progressHoldClick.GetComponent<RectTransform>().localScale.x >= (float)0.6)
        {
            transform.position = listeCheckPoints[nextCheckpoint].transform.position;
            Respawn.SetActive(false);
            mj.GetComponent<MouvementJoueur>().enabled = false;
            respawn_text.gameObject.SetActive(true);
            respawned = true;
        }

        if (Input.GetKey(KeyCode.R) ||Input.GetKey("joystick button 2"))
        {
            progressHoldClick.GetComponent<RectTransform>().localScale += new Vector3((float)0.002, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp("joystick button 2"))
        {
            progressHoldClick.GetComponent<RectTransform>().localScale = new Vector3(0, progressHoldClick.transform.localScale.y, progressHoldClick.transform.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "zone")
        {
            Respawn.SetActive(true);
        }
        else
        {
            speedEffect.Play(false);
            mj.GetComponent<MouvementJoueur>().speed /= 10f;
            mj.GetComponent<MouvementJoueur>().mode = 1;

            StartCoroutine(SpeedDecrementation());
            if (nextCheckpoint == listeCheckPoints.Count - 1)
            {
                other.gameObject.SetActive(false);
                mj.GetComponent<MouvementJoueur>().enabled = false;
            }
            else
            {
                positionInitiale = listeCheckPoints[nextCheckpoint].transform.position;
                listeCheckPoints[nextCheckpoint].SetActive(false);
                nextCheckpoint++;
                listeCheckPoints[nextCheckpoint].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "zone")
        {
            Respawn.SetActive(false);
        }
    }

    IEnumerator SpeedDecrementation()
    {
        yield return new WaitForSeconds(0.5f);
        mj.GetComponent<MouvementJoueur>().mode = 0;
        mj.GetComponent<MouvementJoueur>().speed *= 10f;
    }
}
