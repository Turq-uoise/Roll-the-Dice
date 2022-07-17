using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    public ParticleSystem teleparticles;
    AudioSource audioSrc;
    public AudioClip portal;

    public GameObject destination;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        Physics.IgnoreLayerCollision(8,9);
        audioSrc = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (playerController.portalled == 0)
        {
            playerController.transform.position = destination.transform.position;
            audioSrc.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSrc.PlayOneShot(portal, 1);
            playerController.portalled++;
            teleparticles.Play();
        }
        
        if (playerController.portalled == 1)
        {
            teleparticles.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Invoke("resetPortal", 0.5f);
    }

    private void resetPortal()
    {
        playerController.portalled = 0;
    }
}
