using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listeCheckPoints = new List<GameObject>();
    [SerializeField] Canvas Respawn;
    [SerializeField] GameObject progressHoldClick;
    int nextCheckpoint = 0;
    Vector3 positionInitiale;
    public GameObject mj;
    [SerializeField]
    ParticleSystem speedEffect;

    // Start is called before the first frame update
    void Start()
    {
        progressHoldClick.GetComponent<RectTransform>().localScale = new Vector3(0, progressHoldClick.transform.localScale.y, progressHoldClick.transform.localScale.z);
        Respawn.enabled = false;

        for (int i = 0; i < listeCheckPoints.Count; i++)
        {
            listeCheckPoints[i].SetActive(false);
        }
        speedEffect.Stop();
        listeCheckPoints[0].SetActive(true);

    }

    void Update()
    {

        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("Button " + i + " was pressed!");
            }
        }

        float distance = Vector3.Distance(listeCheckPoints[nextCheckpoint].transform.position, gameObject.transform.position);
        if (distance > 40)
        {
            Respawn.enabled = true;
        }
        /*if (distance > 80 || (float)(progressHoldClick.GetComponent<RectTransform>().localScale.x) >= (float)0.6)
        {
            gameObject.transform.position = new Vector3(positionInitiale.x,transform.position.y,positionInitiale.z);
            Respawn.enabled = false;
            progressHoldClick.GetComponent<RectTransform>().localScale = new Vector3(0, progressHoldClick.transform.localScale.y, progressHoldClick.transform.localScale.z);
        }*/
        
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

        speedEffect.Play(false);
        mj.GetComponent<MouvementJoueur>().speed *= 10f;

        if (nextCheckpoint == listeCheckPoints.Count - 1)
        {
            other.gameObject.SetActive(false);
            mj.GetComponent<MouvementJoueur>().enabled = false;
        }
        else
        {
            positionInitiale = listeCheckPoints[nextCheckpoint].transform.position;
            other.gameObject.SetActive(false);
            nextCheckpoint++;
            listeCheckPoints[nextCheckpoint].SetActive(true);
        }
    }
}
