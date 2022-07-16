using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablle : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 spawn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spawn = this.gameObject.transform.position;
        Physics.IgnoreLayerCollision(3, 8);
    }

    void Start()
    {
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void Respawn()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        this.gameObject.transform.position = spawn;
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
