using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float smoothing;
    public float speed;
    public float jumpForce;

    public bool pause;
    public bool InDialogue;

    public GameObject interactable;
    public bool normalState = false;
    public bool blightState = false;
    public bool natureState = false;
    public bool cinematicState = false;

    public Text interact;

    private Vector2 _moveAxis;
    private Vector2 _playerAxis;
    private Rigidbody _rb;
    private InputMaster _controls;

    private bool _grounded = true;
    private bool _secondJump = true;
    [SerializeField]
    private bool _gliding = false;
    [SerializeField]
    private bool _canDash = true;
    private bool _dashing;

    private float _facingAngle;
    private float _playerRotation;
    private float _dashTime;
    private float _magSpeed;

    [SerializeField]
    protected float _jumpHeight;

    [SerializeField]
    protected float _dashLimit;

    [SerializeField]
    protected float _maxFallVelocity;

    [SerializeField]
    protected float _dashMult;

    [SerializeField]
    protected Animator anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        _dashTime = _dashLimit;

        _rb = GetComponent<Rigidbody>();

        //Setting controls based and provides them with functionality for their specified buttons
        _controls = new InputMaster();

        _controls.Player.Jump.performed += context => HandleJump();

        _controls.Player.Movement.performed += context => _moveAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.performed += context => _playerAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.performed += context => HandleTurn();
        _controls.Player.Movement.canceled += context => _moveAxis = Vector2.zero;

        _controls.Player.Interact.performed += context => HandleInteraction();

        _controls.Player.Glide.performed += context => HandleGlide();
        _controls.Player.Glide.canceled += context => EndGlide();

        _controls.Player.Dash.performed += context => HandleDash();

        _controls.Player.Pause.performed += context => HandlePause();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetBool("Pause", pause);

        if (!pause && !cinematicState)
        {
            //Unfreezes player if not paused and only goes through these methods during unpaused state
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            AnimatorHandler();

            if (!anim.GetBool("Idle"))
            {
                PlayerFacing();
            }
            MovementHandler();
        }
        else {
            //Freezes player based on constraints during pause
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        if (interactable != null)
        {
            interact.gameObject.SetActive(true);
        }
        else {
            interact.gameObject.SetActive(false);
        }
    }

    //Changes animation states when needed, depending on the current state of the player.
    private void AnimatorHandler()
    {
        //Setting Velocity to different values
        if (_rb.velocity.y < -0.01f)
        {
            anim.SetBool("Falling", true);
        }

        //Setting the Speed to different values to be used by the blend trees
        _magSpeed = new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude;
        _magSpeed = Mathf.Abs(_magSpeed);

        anim.SetFloat("Speed", _magSpeed);

        //Setting the idle boolean according to whether movement is occurring
        if (_magSpeed > 0.1f)
        {
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
        }

        //Setting grounded and gliding values based on booleans that are handled at other stages in the script
        anim.SetBool("isGrounded", _grounded);
        anim.SetBool("Gliding", _gliding);

        if (_grounded)
        {
            anim.SetBool("Falling", false);
        }
    }

    //Handles the movement of the player depending on their current state and any actions that are taking place.
    private void MovementHandler()
    {
        //Handles how the dash works, so if dash is pressed player is rocketed forward by a set amount of speed that wont cause them to clip into walls
        if (!_dashing)
        {
            _rb.velocity = new Vector3(_moveAxis.x * speed, _rb.velocity.y, _moveAxis.y * speed);
        }

        if (_dashing)
        {
            _dashTime -= Time.deltaTime;
            _rb.velocity = transform.right * speed * _dashMult;
            if (_dashTime < 0)
            {
                _dashing = false;
                _rb.velocity = Vector3.zero;
                _dashTime = _dashLimit;
            }
        }

        //If the player holds the button for gliding the players max fall velocity will be reduced (Should probably change this to add a force, will work out specifics later after discussing) 
        if (_gliding)
        {
            if (!(_rb.velocity.y > -1f))
            {
                _rb.velocity += Vector3.down * 1.5f * Physics.gravity.y * Time.deltaTime;
            }
        }

        //Makes fall feel faster in order to be able to distinguish between normal fall and glide
        if (!_gliding && _rb.velocity.y < 0.1)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * 1.5f * (2.5f - 1) * Time.deltaTime;
        }
    }

    private void HandleTurn()
    {
        PlayerFacing();
    }

    //Handles what happens when any of the buttons that entail a dash are pressed. Sets the pause bool to true
    private void HandlePause()
    {
        if (pause)
        {
            pause = false;
        }
        else
        {
            pause = true;
        }
    }

    //Handles what happens when any of the buttons that entail a dash are pressed.
    private void HandleDash()
    {
        if (_canDash && natureState)
        {
            _dashing = true;
            anim.SetTrigger("Dash");
            _canDash = false;
        }
    }

    //Handles what happens when any of the buttons that entail a glide are pressed.
    private void HandleGlide()
    {
        if (!_grounded && natureState)
        {
            _gliding = true;
        }
        else
        {
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
        anim.SetTrigger("Interaction");

        if (interactable != null)
        {
            Interaction interactableObject = interactable.GetComponent<Interaction>();

            print("Interacting with " + interactable.name);

            if (interactable.GetComponent<Interaction>().NPC)
            {
                interactable.GetComponent<NPCDialogueTrigger>().TriggerDialogue();
            }

            if (interactable.GetComponent<Interaction>().Button)
            {
                ButtonManager button = interactable.GetComponent<ButtonManager>();
                button.interactionHandler();
            }

            if (interactable.GetComponent<Interaction>().Instrument)
            {

            }

        }
        else
        {
            print("Nothing to interact with.");
        }
    }


    //Checks if player is in contact with ground do decide whether they can jump again.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != null)
        {
            _grounded = true;
            _secondJump = true;
            _gliding = false;
            _canDash = true;
            anim.SetBool("Falling", false);
        }
    }

    //Handles what happens when any of the buttons that entail a jump are pressed.
    private void HandleJump()
    {
        if (!_grounded && _secondJump && blightState)
        {
            anim.SetTrigger("DoubleJump");
            _rb.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * 2f * _jumpHeight));
            _secondJump = false;
        }

        if (_grounded)
        {
            anim.SetTrigger("Jump");
            _rb.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * 1.35f * _jumpHeight));
            _grounded = false;
        }
    }

    //Returns the quaternion angle that the player should be facing when they move.
    private void PlayerFacing()
    {
        if (_playerAxis.magnitude > 0.1f) { _facingAngle = Mathf.Atan2(-_playerAxis.y, _playerAxis.x) * Mathf.Rad2Deg + 360; }
        Quaternion target = Quaternion.Euler(transform.rotation.x, _facingAngle, transform.rotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothing);
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
