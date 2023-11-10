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

        player.gameObject.transform.Rotate(mouvement * Rotatespeed);
        player.Move(Vector3.forward * speed);
    }
}
