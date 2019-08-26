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
    private bool _dashing;

    private float _facingAngle;
    private float _playerRotation;
    private float _dashTime;

    [SerializeField]
    protected float _jumpHeight;

    [SerializeField]
    protected float _dashLimit;

    [SerializeField]
    protected float _maxFallVelocity;

    [SerializeField]
    protected float _dashMult;

    private void FixedUpdate()
    {
        if (!_dashing)
        {
            _rg.velocity = new Vector3(_moveAxis.x * speed, _rg.velocity.y, _moveAxis.y * speed);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, PlayerFacing(), Time.deltaTime * smoothing);

        if (_dashing)
        {
            _dashTime -= Time.deltaTime;
            _rg.velocity = transform.right * speed * _dashMult;
            if (_dashTime < 0)
            {
                _dashing = false;
                _rg.velocity = Vector3.zero;
                _dashTime = _dashLimit;
            }
        }

        //If the player holds the button for gliding the players max fall velocity will be reduced (Should probably change this to add a force, will work out specifics later after discussing) 
        if (_gliding)
        {
            if (!(_rg.velocity.y > -1f))
            {
                _rg.velocity += Vector3.down * 1.5f * Physics.gravity.y * Time.deltaTime;
            }
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
        _dashTime = _dashLimit;

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

    //Handles what happens when any of the buttons that entail a dash are pressed.
    private void HandleDash()
    {
        if (_canDash)
        {
            _dashing = true;
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
            _rg.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * 2f * _jumpHeight));
            _secondJump = false;
        }

        if (_grounded)
        {
            _rg.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * 1.35f * _jumpHeight));
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
