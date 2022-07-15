using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winCheckone : MonoBehaviour
{
    void Start() { Physics.IgnoreLayerCollision(1, 3); }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "one") { Debug.Log("Win!"); }

        else { Debug.Log("You need the numbers to touch!"); }
    }
}
