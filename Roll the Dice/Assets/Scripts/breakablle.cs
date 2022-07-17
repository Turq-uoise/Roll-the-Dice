using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablle : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 spawn;
    public CameraShake cameraShake;
    public float elapsed = 0;
    public MeshRenderer breakables;
    AudioSource audioSrc;
    public AudioClip brickbreak;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spawn = this.gameObject.transform.position;
        Physics.IgnoreLayerCollision(3, 8);
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(cameraShake.Shake(.15f, .1f));
        rb.detectCollisions = false;
        breakables.enabled = false;
        audioSrc.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSrc.PlayOneShot(brickbreak, 1);
    }

    private void Update()
    {

    }

    public void Respawn()
    {
        rb.detectCollisions = true;
        breakables.enabled = false;
        this.gameObject.transform.position = spawn;
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
