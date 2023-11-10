using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouvementJoueur : MonoBehaviour
{
    //declaration des variables
    public float speed = 0.0001f;
    public float Rotatespeed = 0.005f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public Camera cam;
    public float sensi = 30f;
    private Vector3 mouvement = Vector3.zero;
    CharacterController player;
    public Animator animator;

    void Start()
    {
        //recuperation du composant CharacterController
        player = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mouvement = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));
       
        //recuperation des valeurs des axes horizontal et vertical 
        //et stockage dans un Vector3
        //mouvement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //tient compte de la rotation du joueur
        mouvement = transform.TransformDirection(mouvement);
        //on multiplie le vector3 par la vitesse de deplacement
        mouvement *= speed;
        //si la touche ESPACE est pressee
        /*if (Input.GetButton("Jump"))
        {
            //on dit que notre point sur l'axe y augmente de la valeur de jumpSpeed 
            mouvement.y = jumpSpeed;
        }*/
        
        //ici on effectue la rotation de notre joueur s'il glisse la souris a gauche ou a droite
        //transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * speed * sensi);

        player.gameObject.transform.Rotate(mouvement * Rotatespeed);
        player.Move(Vector3.forward * speed);
    }
}
