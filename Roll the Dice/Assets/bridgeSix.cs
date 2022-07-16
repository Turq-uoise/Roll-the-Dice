using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeSix : MonoBehaviour
{
    public GameObject bridge;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "six")
        {
            bridge.SetActive(true);
        }
    }
}
