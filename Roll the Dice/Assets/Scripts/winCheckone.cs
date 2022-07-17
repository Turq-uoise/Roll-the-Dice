using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class winCheckone : MonoBehaviour
{
    public GameObject correctionText;
    public GameObject winDow;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject redstar;
    public TextMeshProUGUI movement;

    PlayerController playerController;
    public CameraShake cameraShake;
    [SerializeField] GameObject player;

    void Start() 
    { 
        Physics.IgnoreLayerCollision(1, 3);
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        movement.text = "Moves: " + playerController.moveCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "one") 
        {
            winDow.SetActive(true);
            playerController.input.Disable();
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (playerController.moveCount > 4)
                {
                    redstar.SetActive(false);
                }
                if (playerController.moveCount > 5)
                {
                    star3.SetActive(false);
                }
                if (playerController.moveCount > 13)
                {
                    star2.SetActive(false);
                }
                if (playerController.moveCount > 25)
                {
                    star1.SetActive(false);
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (playerController.moveCount > 24)
                {
                    redstar.SetActive(false);
                }
                if (playerController.moveCount > 25)
                {
                    star3.SetActive(false);
                }
                if (playerController.moveCount > 62)
                {
                    star2.SetActive(false);
                }
                if (playerController.moveCount > 99)
                {
                    star1.SetActive(false);
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (playerController.moveCount > 35)
                {
                    redstar.SetActive(false);
                }
                if (playerController.moveCount > 36)
                {
                    star3.SetActive(false);
                }
                if (playerController.moveCount > 51)
                {
                    star2.SetActive(false);
                }
                if (playerController.moveCount > 124)
                {
                    star1.SetActive(false);
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                if (playerController.moveCount > 20)
                {
                    redstar.SetActive(false);
                }
                if (playerController.moveCount > 21)
                {
                    star3.SetActive(false);
                }
                if (playerController.moveCount > 24)
                {
                    star2.SetActive(false);
                }
                if (playerController.moveCount > 36)
                {
                    star1.SetActive(false);
                }
            }
        }

        else 
        {
            correctionText.SetActive(false);
            correctionText.SetActive(true);
        }
    }

    
}
