using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouvementJoueur : MonoBehaviour
{
    public float Bulletspeed = 200f;
    public float Rotatespeed = 0.09f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public GameObject cam;
    public float sensi = 30f;

    public GameObject bullet;
    public GameObject rocket;
    public Transform bullet1_pos;
    public Transform bullet2_pos;
    public AudioSource bulletSound;
    public AudioSource rocketSound;
    public List<GameObject> ammo = new List<GameObject>();
    int i = 1;
    bool reload = false;
    bool reload_rocket = false;
    GameObject rocket_ammo;

    [SerializeField]
    public float speed = 7f;
    private float pitch, yaw, roll;
    Rigidbody rb;

    [SerializeField]
    GameObject ship;

    [SerializeField]
    private float AmbientSpeed = 2f;
    [SerializeField]
    private float RotationSpeed = 40f;

    public int mode;

    public TMP_Text timer;
    public static float time;

    public static string minutes;
    public static string secondes;

    void Awake()
    {
        mode = 0;
        time = 0;
        rb = GetComponent<Rigidbody>();
        Destroy(ship);
        GameObject new_ship = Ship_Choice.GetSelectedShip();
        ship = Instantiate(new_ship, ship.transform.position, ship.transform.rotation);
        ship.transform.parent = gameObject.transform;
        Debug.Log("apres selection : " + ship.name);
        string str = ship.name;
        Transform cam_pos = transform.Find(str + "/cam_pos");
        cam.transform.position = cam_pos.position;
        bullet1_pos = transform.Find(str + "/bullet1_pos");
        bullet2_pos = transform.Find(str + "/bullet2_pos");
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");
    }

    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("ammo");
        foreach (GameObject g in a)
        {
            ammo.Add(g);
        }

        rocket_ammo = GameObject.FindGameObjectWithTag("rocket");


    }

    void Update()
    {

        minutes = ((int)time / 60).ToString();
        secondes = (time % 60).ToString("N0");

        if((time % 60) < 10)
            timer.text = minutes + ":0" + secondes;
        else
            timer.text = minutes + ":" + secondes;


        time += Time.deltaTime; //on decremente le timer

        HandleInputs();

        if (i == ammo.Count)
        {
            reload = true;
            StartCoroutine(Rechargement());
            i = 1;
        }

        if (reload_rocket)
        {
            StartCoroutine(RechargementRocket());
        }

        if (Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(0))
        {
            GameObject objet;
            if (!ChangeWeapons.swapWeapons && !reload)
            {
                //objet = bullet;
                ammo[ammo.Count - i].SetActive(false);
                i += 1;

                GameObject newBullet1 = Instantiate(bullet, bullet1_pos.position, bullet1_pos.rotation) as GameObject;
                newBullet1.transform.Rotate(new Vector3(0, 0, 0));
                Rigidbody rBullet1 = newBullet1.GetComponent<Rigidbody>();
                rBullet1.isKinematic = false;
                rBullet1.velocity = (bullet1_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed);

                GameObject newBullet2 = Instantiate(bullet, bullet2_pos.position, bullet2_pos.rotation) as GameObject;
                Rigidbody rBullet2 = newBullet2.GetComponent<Rigidbody>();
                rBullet2.isKinematic = false;
                rBullet2.velocity = bullet2_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed;

                bulletSound.Play();
            }
            else if (ChangeWeapons.swapWeapons && !reload_rocket)
            {
                //objet = rocket;
                rocket_ammo.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 15);
                reload_rocket = true;

                GameObject newBullet1 = Instantiate(rocket, bullet1_pos.position, bullet1_pos.rotation) as GameObject;
                newBullet1.transform.Rotate(new Vector3(0, 0, 0));
                Rigidbody rBullet1 = newBullet1.GetComponent<Rigidbody>();
                rBullet1.isKinematic = false;
                rBullet1.velocity = (bullet1_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed);

                GameObject newBullet2 = Instantiate(rocket, bullet2_pos.position, bullet2_pos.rotation) as GameObject;
                Rigidbody rBullet2 = newBullet2.GetComponent<Rigidbody>();
                rBullet2.isKinematic = false;
                rBullet2.velocity = bullet2_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed;

                rocketSound.Play();
            }
        }
    }
    private void FixedUpdate()
    {

        Quaternion AddRot = Quaternion.identity;
        roll = Input.GetAxis("Roll") * (Time.deltaTime * RotationSpeed);
        pitch = Input.GetAxis("Pitch") * (Time.deltaTime * RotationSpeed);
        yaw = Input.GetAxis("Yaw") * (Time.deltaTime * RotationSpeed);
        AddRot.eulerAngles = new Vector3(pitch, yaw, roll);
        rb.rotation *= AddRot;
        Vector3 AddPos = Vector3.forward;
        AddPos = rb.rotation * AddPos;
        rb.position += AddPos * (AmbientSpeed);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Rechargement()
    {
        StartCoroutine(Wait());
        foreach (GameObject a in ammo)
        {
            a.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        reload = false;
    }

    IEnumerator RechargementRocket()
    {
        yield return new WaitForSeconds(2f);

        rocket_ammo.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 15);
        reload_rocket = false;
    }
}
