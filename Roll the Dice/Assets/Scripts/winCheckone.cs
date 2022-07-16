using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winCheckone : MonoBehaviour
{
    public GameObject correctionText;
    public TextMeshProUGUI movement;
    public GameObject winDow;
    PlayerController playerController;
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
        }

        else 
        {
            correctionText.SetActive(false);
            correctionText.SetActive(true);
        }
    }

    
}
