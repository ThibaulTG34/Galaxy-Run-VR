using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouvementJoueur : MonoBehaviour
{
    public float Bulletspeed = 200f;
    public float Rotatespeed = 0.09f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public GameObject cam;
    public float sensi = 30f;
    private Vector3 mouvement = Vector3.zero;
    CharacterController player;

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

    public float throttleIncrement = 0.1f;
    [SerializeField]
    public float maxThrottle = 200f;
    public float responsiveness = 10f;
    public float speed = 7f;
    private float throttle = 25f, pitch, yaw, roll;
    Rigidbody rb;

    [SerializeField]
    GameObject ship;

    public int mode;

    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    void Awake()
    {
        mode = 0;
        rb = GetComponent<Rigidbody>();
        Destroy(ship);
        GameObject new_ship = Ship_Choice.GetSelectedShip();
        ship = Instantiate(new_ship, ship.transform.position, ship.transform.rotation);
        ship.transform.parent = gameObject.transform;
        Debug.Log("apres selection : " + ship.tag);
        string str = ship.name;
        //Debug.Log("cam_pos : " + transform.Find(str+"/cam_pos").position);
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
        player = GetComponent<CharacterController>();
        GameObject[] a = GameObject.FindGameObjectsWithTag("ammo");
        foreach (GameObject g in a)
        {
            ammo.Add(g);
        }

        rocket_ammo = GameObject.FindGameObjectWithTag("rocket");
    }

    void Update()
    {
        /*mouvement = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));
       
        mouvement = transform.TransformDirection(mouvement);
        mouvement *= speed;
        player.gameObject.transform.Rotate(mouvement);
        player.Move(Vector3.forward * speed);*/

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
                rBullet1.velocity = (bullet1_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed / 2.0f);

                GameObject newBullet2 = Instantiate(bullet, bullet2_pos.position, bullet2_pos.rotation) as GameObject;
                Rigidbody rBullet2 = newBullet2.GetComponent<Rigidbody>();
                rBullet2.isKinematic = false;
                rBullet2.velocity = bullet2_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed / 2.0f;

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
                rBullet1.velocity = (bullet1_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed / 2.0f);

                GameObject newBullet2 = Instantiate(rocket, bullet2_pos.position, bullet2_pos.rotation) as GameObject;
                Rigidbody rBullet2 = newBullet2.GetComponent<Rigidbody>();
                rBullet2.isKinematic = false;
                rBullet2.velocity = bullet2_pos.TransformDirection(Vector3.forward) * Bulletspeed * speed / 2.0f;

                rocketSound.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrottle * throttle * speed*3f, (ForceMode)mode);
        rb.AddTorque(transform.up * yaw * responseModifier * 8f);
        //rb.AddForce(-transform.up * pitch * responseModifier * 10f);
        rb.AddTorque(transform.right * pitch * responseModifier * 6f);
        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch*4f, 0, 0));
        //rb.MoveRotation(deltaRotation);
        rb.AddTorque(transform.forward * roll * responseModifier * 4f);
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
