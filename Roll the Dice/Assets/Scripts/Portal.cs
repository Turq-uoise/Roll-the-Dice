using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;

    public GameObject destination;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (playerController.portalled == 0)
        {
            playerController.transform.position = destination.transform.position;
            playerController.portalled++;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (playerController.portalled <= 2)
        {
            playerController.portalled++;
        }

        if (playerController.portalled == 3)
        {
            playerController.portalled = 0;
        }
    }
}
