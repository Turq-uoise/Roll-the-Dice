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

    [SerializeField] private float speed;
    [HideInInspector] private float gravity = 5;
    [HideInInspector] public int moveCount = 0;

    [HideInInspector] public bool moving = false;
    [HideInInspector] public bool movingRight = false;
    [HideInInspector] public bool movingLeft = false;
    [HideInInspector] public bool movingUp = false;
    [HideInInspector] public bool movingDown = false;
    public int portalled = 0;

    [HideInInspector] public Quaternion currentRot = Quaternion.Euler(0,0,0);
    [HideInInspector] public Quaternion newRot = Quaternion.Euler(0,0,0);

    public breakablle breakable;
    public GameObject breakblock;

    private void Awake()
    {
        input = new InputController();
        rb = GetComponent<Rigidbody>();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            breakable = breakblock.GetComponentInChildren<breakablle>();
        }
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
        Application.targetFrameRate = 120;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            StartCoroutine(Roll(Vector3.right));
        }
        else if (movingLeft == true)
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (movingUp == true)
        {
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (movingDown == true)
        {
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

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}