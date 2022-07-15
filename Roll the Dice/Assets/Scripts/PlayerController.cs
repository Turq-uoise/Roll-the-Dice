using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputController input;
    public Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float moveTime;

    private float elapsedTime;
    public bool moving = false;
    public bool movingRight = false;
    public bool movingLeft = false;
    public bool movingUp = false;
    public bool movingDown = false;
    
    public Vector3 currentPos;
    public Vector3 newPos;

    public Quaternion currentRot = Quaternion.Euler(0,0,0);
    public Quaternion newRot = Quaternion.Euler(0,0,0);

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
            currentPos = transform.position;
            newPos = transform.position + new Vector3(1, 0, 0);
            //currentRot = Vector3.up;
            //newRot = Quaternion.Euler(currentRot.x, currentRot.y, currentRot.z -90);
            movingRight = true;
            //moving = true;
        }
    }

    private void MoveLeft()
    {
        if (movingLeft == false && moving == false)
        {
            currentPos = transform.position;
            newPos = transform.position + new Vector3(-1, 0, 0);
            //currentRot = transform.rotation;
            //newRot = currentRot * Quaternion.Euler(0, 0, 90);
            movingLeft = true;
            //moving = true;
        }
    }

    private void MoveDown()
    {
        if (movingDown == false && moving == false)
        {
            currentPos = transform.position;
            newPos = transform.position + new Vector3(0, 0, -1);
            //currentRot = transform.rotation;
            //newRot = currentRot * Quaternion.Euler(0, -90, 0);
            movingDown = true;
            //moving = true;
        }
    }
    private void MoveUp()
    {
        if (movingUp == false && moving == false)
        {
            currentPos = transform.position;
            newPos = transform.position + new Vector3(0, 0, 1);
            //currentRot = transform.rotation;
            //newRot = currentRot * Quaternion.Euler(0, 90, 0);
            movingUp = true;
            //moving = true;
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
        //newPos = transform.position + direction;
        Vector3 rotationCenter = transform.position + direction / 2f + Vector3.down / 2f;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            //transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime / moveTime);
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
}
