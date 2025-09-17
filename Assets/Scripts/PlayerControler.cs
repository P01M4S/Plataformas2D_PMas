using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D _rigidBody;
    public InputAction moveAction;
    public Vector2 _moveInput;
    public InputAction jumpAction;
    public InputAction attackAction;
    public float _playerSpeed = 5;
    public float _jumpForce = 3.5f;
    public GroundSensor groundSensor;
    public Transform _sensorPosition;
    public Vector2 _sensorSize = new Vector2(0.5f, 0.5f);


    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
        attackAction = InputSystem.actions["Attack"];
        groundSensor = GetComponentInChildren<GroundSensor>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        _moveInput = moveAction.ReadValue<Vector2>();
        Debug.Log(_moveInput);

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerSpeed * Time.deltaTime;

        if (jumpAction.WasPressedThisFrame() && isGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        _rigidBody.AddForce(transform.up * Mathf.Sqrt(_jumpForce * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        _rigidBody.linearVelocity = new Vector2(_moveInput.x * _playerSpeed, _rigidBody.linearVelocityY);
    }

    bool isGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);
    }

}