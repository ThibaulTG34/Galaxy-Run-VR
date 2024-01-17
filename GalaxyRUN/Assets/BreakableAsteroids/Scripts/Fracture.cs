using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;
    public AudioSource boom;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            boom.Play();
            Instantiate(fractured, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
