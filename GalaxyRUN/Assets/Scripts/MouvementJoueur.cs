using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public float throttleIncrement = 0.1f;
    [SerializeField]
    public float maxThrottle = 200f;
    public float responsiveness = 10f;
    public float speed = 7f;
    private float throttle = 20f, pitch, yaw, roll;

    Rigidbody rb;

    [SerializeField]
    GameObject ship;

    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(ship);
        GameObject new_ship = Ship_Choice.GetSelectedShip();
        ship = Instantiate(new_ship, ship.transform.position, ship.transform.rotation);
        ship.transform.parent = gameObject.transform;
        Debug.Log("apres selection : "+ship.name);
        string str = ship.name;
        //Debug.Log("cam_pos : " + transform.Find(str+"/cam_pos").position);
        Transform cam_pos = transform.Find(str+"/cam_pos");
        cam.transform.position = cam_pos.position;
        bullet1_pos = transform.Find(str+"/bullet1_pos");
        bullet2_pos = transform.Find(str+"/bullet2_pos");
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.X))
        {
            Debug.Log("X");
            throttle += throttleIncrement;
        }
        else if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("T");
            throttle -= throttleIncrement;
        }

        //throttle = 20f;
    }

    void Start()
    {
        player = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        /*mouvement = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));
       
        mouvement = transform.TransformDirection(mouvement);
        mouvement *= speed;
        player.gameObject.transform.Rotate(mouvement);
        player.Move(Vector3.forward * speed);*/

        HandleInputs();

        if (Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(0))
        {
            GameObject objet;
            if(!ChangeWeapons.swapWeapons)
            {
                objet = bullet;
            }
            else
            {
                objet = rocket;
            }
            GameObject newBullet1 = Instantiate(objet, bullet1_pos.position, bullet1_pos.rotation) as GameObject;
            newBullet1.transform.Rotate(new Vector3(0, 0, 0));
            Rigidbody rBullet1 = newBullet1.GetComponent<Rigidbody>();
            rBullet1.isKinematic = false;
            rBullet1.velocity = (bullet1_pos.TransformDirection(Vector3.forward) * Bulletspeed);

            GameObject newBullet2 = Instantiate(objet, bullet2_pos.position, bullet2_pos.rotation) as GameObject;
            Rigidbody rBullet2 = newBullet2.GetComponent<Rigidbody>();
            rBullet2.isKinematic = false;
            rBullet2.velocity = bullet2_pos.TransformDirection(Vector3.forward) * Bulletspeed;

            bulletSound.Play();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrottle * throttle * speed);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);
    }



}
