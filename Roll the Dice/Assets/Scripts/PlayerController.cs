using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] private InputController input;
    [HideInInspector] public Rigidbody rb;

    [SerializeField] private float speed;
    [HideInInspector] private float gravity = 5;

    [HideInInspector] public bool moving = false;
    [HideInInspector] public bool movingRight = false;
    [HideInInspector] public bool movingLeft = false;
    [HideInInspector] public bool movingUp = false;
    [HideInInspector] public bool movingDown = false;

    [HideInInspector] public Quaternion currentRot = Quaternion.Euler(0,0,0);
    [HideInInspector] public Quaternion newRot = Quaternion.Euler(0,0,0);

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
    }

    IEnumerator Roll(Vector3 direction)
    {
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
        movingDown = false;
        movingLeft = false;
        movingRight = false;
        movingUp = false;
        moving = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravity);
    }
}
