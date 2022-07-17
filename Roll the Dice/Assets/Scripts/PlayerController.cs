using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public InputController input;
    [HideInInspector] public Rigidbody rb;

    public GameObject Respawner;
    public GameObject winDow;
    public GameObject bridge;
    public GameObject pauseMenu;
    AudioSource audioSrc;
    public AudioClip move;

    [SerializeField] private float speed;
    [HideInInspector] private float gravity = 5;
    [HideInInspector] public int moveCount = 0;

    [HideInInspector] public bool moving = false;
    [HideInInspector] public bool movingRight = false;
    [HideInInspector] public bool movingLeft = false;
    [HideInInspector] public bool movingUp = false;
    [HideInInspector] public bool movingDown = false;
    [HideInInspector] public bool timescale = false;
    public int portalled = 0;

    private void Awake()
    {
        input = new InputController();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        input.Ground.MoveUp.performed += _ => MoveUp();
        input.Ground.MoveDown.performed += _ => MoveDown();
        input.Ground.MoveLeft.performed += _ => MoveLeft();
        input.Ground.MoveRight.performed += _ => MoveRight();
        input.Ground.Reset.performed += _ => Reset();
        input.Ground.Pause.performed += _ => Pause();
        audioSrc = GetComponent<AudioSource>();
        Application.targetFrameRate = 120;
    }

    public void Pause()
    {
        timescale = !timescale;
        if (timescale == true)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Reset()
    {
        //winDow.SetActive(false);
        //input.Enable();

        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    bridge.SetActive(false);
        //}

        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    breakable.Respawn();
        //}

        //moving = false;
        //moveCount = 0;

        //rb.Sleep();
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        //transform.position = Respawner.transform.position;
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            winDow.SetActive(false);
            input.Enable(); moving = false;
            moveCount = 0;
            rb.Sleep();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = Respawner.transform.position;
        }
    }

    private void MoveRight()
    {
        if (movingRight == false && moving == false)
        {
            movingRight = true;
        }
    }

    private void MoveLeft()
    {
        if (movingLeft == false && moving == false)
        {
            movingLeft = true;
        }
    }

    private void MoveDown()
    {
        if (movingDown == false && moving == false)
        {
            movingDown = true;
        }
    }
    private void MoveUp()
    {
        if (movingUp == false && moving == false)
        {
            movingUp = true;
        }
    }

    void Update()
    {
        if (moving == true)
        {
            return;
        }

        if (movingRight == true)
        {
            audioSrc.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSrc.PlayOneShot(move, 1);
            StartCoroutine(Roll(Vector3.right));
        }
        else if (movingLeft == true)
        {
            audioSrc.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSrc.PlayOneShot(move, 1);
            StartCoroutine(Roll(Vector3.left));
        }
        else if (movingUp == true)
        {
            audioSrc.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSrc.PlayOneShot(move, 1);
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (movingDown == true)
        {
            audioSrc.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSrc.PlayOneShot(move, 1);
            StartCoroutine(Roll(Vector3.back));
        }
        Vector3 tmp = transform.position;
        tmp.x = Mathf.Round(tmp.x);
        tmp.y = 0.5f;
        tmp.z = Mathf.Round(tmp.z);
    }

    IEnumerator Roll(Vector3 direction)
    {
        moveCount++;
        moving = true;
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2f + Vector3.down / 2f;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle; 
            yield return null;
        }
        Vector3 tmp = transform.position;
        tmp.x = Mathf.Round(tmp.x);
        tmp.y = 0.5f;
        tmp.z = Mathf.Round(tmp.z);
        transform.position = tmp;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
        movingUp = false;
        moving = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravity);
        if (transform.position.y < 0.4)
        {
            moving = true;
        }
    }

    public void SetScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}