using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeSix : MonoBehaviour
{
    public GameObject bridge;
    public CameraShake cameraShake;
    private bool active = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "six" && active == false)
        {
            bridge.SetActive(true);
            StartCoroutine(cameraShake.Shake(.15f, .2f));
            active = true;
        }
    }
}
