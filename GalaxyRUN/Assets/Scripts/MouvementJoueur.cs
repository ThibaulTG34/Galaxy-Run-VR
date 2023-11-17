using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouvementJoueur : MonoBehaviour
{
    //declaration des variables
    public float speed = 0.0001f;
    public float Bulletspeed = 20f;
    public float Rotatespeed = 0.09f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public Camera cam;
    public float sensi = 30f;
    private Vector3 mouvement = Vector3.zero;
    CharacterController player;
    public GameObject bullet;
    public GameObject bullet1_pos;
    public GameObject bullet2_pos;

    void Start()
    {
        player = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouvement = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));
       
        mouvement = transform.TransformDirection(mouvement);
        mouvement *= speed;
        player.gameObject.transform.Rotate(mouvement);
        player.Move(Vector3.forward * speed * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            GameObject newBullet1 = Instantiate(bullet, bullet1_pos.transform.position, Quaternion.identity) as GameObject;
            newBullet1.transform.Rotate(new Vector3(90, 0, 0));
            Rigidbody rBullet1 = newBullet1.GetComponent<Rigidbody>();
            rBullet1.velocity = bullet1_pos.transform.TransformDirection(-cam.transform.forward) * Bulletspeed;

            GameObject newBullet2 = Instantiate(bullet, bullet2_pos.transform.position, Quaternion.identity) as GameObject;
            newBullet2.transform.Rotate(new Vector3(90, 0, 0));
            Rigidbody rBullet2 = newBullet2.GetComponent<Rigidbody>();
            rBullet2.velocity = bullet2_pos.transform.TransformDirection(-cam.transform.forward) * Bulletspeed;

        }
    }



}
