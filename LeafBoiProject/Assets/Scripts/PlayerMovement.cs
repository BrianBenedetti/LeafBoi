using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float smoothing;
    public float speed;
    public float jumpForce;

    private Vector2 _moveAxis;
    private Vector2 _playerAxis;
    private Rigidbody _rg;
    private InputMaster _controls;

    private bool _grounded = true;
    private bool _secondJump = true;
    [SerializeField]
    private bool _gliding = false;
    private bool _canDash = true;

    private float _facingAngle;
    private float _playerRotation;
    private float _dashTimer;

    [SerializeField]
    protected float _maxFallVelocity;

    [SerializeField]
    protected Transform _dashPoint;

    private void FixedUpdate()
    {
        _rg.velocity = new Vector3(_moveAxis.x * speed, _rg.velocity.y, _moveAxis.y * speed);

        transform.rotation = Quaternion.Slerp(transform.rotation, PlayerFacing(), Time.deltaTime * smoothing);

        //If the player holds the button for gliding the players max fall velocity will be reduced (Should probably change this to add a force, will work out specifics later after discussing) 
        if (_gliding)
        {
            _rg.velocity = new Vector3(_rg.velocity.x, -0.99f * _maxFallVelocity, _rg.velocity.z);
        }

        //Makes fall feel faster in order to be able to distinguish between normal fall and glide
        if (!_gliding && _rg.velocity.y < 0)
        {
            _rg.velocity += Vector3.up * Physics.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
    }

    //Sets up the controls to call specific functions or set certain values at the beginning of the scene
    private void Awake()
    {
        _rg = GetComponent<Rigidbody>();

        _controls = new InputMaster();

        _controls.Player.Jump.performed += context => HandleJump();

        _controls.Player.Movement.performed += context => _moveAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.performed += context => _playerAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.canceled += context => _moveAxis = Vector2.zero;

        _controls.Player.Interact.performed += context => HandleInteraction();

        _controls.Player.Glide.performed += context => HandleGlide();
        _controls.Player.Glide.canceled += context => EndGlide();

        _controls.Player.Dash.performed += context => HandleDash();
    }

    private void HandleDash()
    {
        if (_canDash)
        {
            
        }
    }

    //Handles what happens when any of the buttons that entail a glide are pressed.
    private void HandleGlide()
    {
        if (!_grounded)
        {
            _gliding = true;
        }
        else {
            _gliding = false;
        }
    }

    //Handles what happens when any of the buttons that entail a glide are let go.
    private void EndGlide()
    {
        _gliding = false;
    }

    //Handles what happens when any of the buttons that entail an interaction are pressed.
    private void HandleInteraction()
    {
        Debug.Log("Player is Interacting with something!");
    }


    //Checks if player is in contact with ground do decide whether they can jump again.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _grounded = true;
            _secondJump = true;
            _gliding = false;
        }
    }

    //Handles what happens when any of the buttons that entail a jump are pressed.
    private void HandleJump()
    {
        if (!_grounded && _secondJump)
        {
            _rg.AddForce(new Vector3(0, jumpForce, 0));
            _secondJump = false;
        }

        if (_grounded)
        {
            _rg.AddForce(new Vector3(0, jumpForce, 0));
            _grounded = false;
        }   
    }

    //Returns the quaternion angle that the player should be facing when they move.
    private Quaternion PlayerFacing()
    {
        _facingAngle = Mathf.Atan2(-_playerAxis.y, _playerAxis.x) * Mathf.Rad2Deg + 360;
        Quaternion target = Quaternion.Euler(transform.rotation.x, _facingAngle, transform.rotation.z);
        return target;
    }

    //Enables controls when this object is enabled
    private void OnEnable()
    {
        _controls.Enable();
    }

    //Disables controls when this object is enabled
    private void OnDisable()
    {
        _controls.Disable();
    }
}
