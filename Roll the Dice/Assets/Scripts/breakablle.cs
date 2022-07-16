using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablle : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        Physics.IgnoreLayerCollision(3, 8);
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
