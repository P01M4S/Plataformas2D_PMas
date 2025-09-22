using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D _rigidBody;
    public Animator _animator;
    public InputAction moveAction;
    public Vector2 _moveInput;
    public InputAction jumpAction;
    public InputAction attackAction;
    public float _playerSpeed = 5;
    public float _jumpForce = 3.5f;
    public GroundSensor groundSensor;
    public Transform _sensorPosition;
    public Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    public bool _alreaduLanded = true;
    public InputAction _interactAction;
    public Vector2 _interactionZone = new Vector2(1, 1);


    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
        attackAction = InputSystem.actions["Attack"];
        groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();
        _interactAction = InputSystem.actions["Interact"];
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

        if (_interactAction.WasPerformedThisFrame())
        {
            Interact();
        }

        Movement();

        _animator.SetBool("IsJumping", !isGrounded());

    }

    void Interact()
    {
        //Debug.Log("haciendo cosas");
        Collider2D[] interactable = Physics2D.OverlapBoxAll(transform.position, _interactionZone, 0);
        foreach (Collider2D item in interactable)
        {
            if (item.gameObject.tag == "Star")
            {
                Debug.Log("Tocada");
            }
        }
    }

    void Jump()
    {
        _rigidBody.AddForce(transform.up * Mathf.Sqrt(_jumpForce * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
        //_animator.SetBool("IsJumping", true);
        //_alreaduLanded = false;
    }

    void Movement()
    {
        if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRuning", true);
        }
        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRuning", true);
        }
        else
        {
            _animator.SetBool("IsRuning", false);
        }
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
                //_alreaduLanded = true;
                return true;
            }
        }
        //_alreaduLanded = false;
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _interactionZone);
    }

}