using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Respawner;
    PlayerController playerController;
    [SerializeField] GameObject player;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        GameObject.FindGameObjectWithTag("Music").GetComponent<playMusic>().PlayMusic();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.Reset();
        }
    }
}
