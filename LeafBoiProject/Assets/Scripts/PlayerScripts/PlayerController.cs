using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private InputMaster _controls;

    public float smoothing;
    public float walkSmoothing;
    public float speed;

    public bool pause;
    public bool inDialogue;
    public bool gliding = false;

    public GameObject interactable;
    public int state = 0;

    private Vector2 _moveAxis;
    private Vector2 _playerAxis;
    private Rigidbody _rb;

    private bool _grounded = true;
    [SerializeField]
    protected bool _secondJump = true;
    [SerializeField]
    protected bool _canDash = true;
    [SerializeField]
    protected DialogueManager _dManager;
    [SerializeField]
    protected bool _jumping;
    [SerializeField]
    protected bool _dashing;
    [SerializeField]
    protected bool _dJumping;
    private bool _interacting;
    private bool _moving = false;


    private float _facingAngle;
    private float _playerRotation;
    private float _dashTime;
    private float _magSpeed;
    private float _magYSpeed;
    private bool _blockRotationPlayer;
    private Camera _cam;
    public Vector3 _moveDir;

    [SerializeField] protected float _jumpHeight;
    [SerializeField] protected float _dashLimit;
    [SerializeField] protected float _maxFallVelocity;
    [SerializeField] protected float _dashMult;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected float _jumpDelay;
    [SerializeField] protected float _speedCharge;
    [SerializeField] protected float _idleLimit;
    [SerializeField] protected GroundCheck _check;
    [SerializeField] protected float glideFactor;

    private bool _jumpPrep;
    private float idleTimer;
    private bool idle2;
    private bool stuck;
    private const float OFFSET = -90;

    public void endDialogue()
    {
        _interacting = false;
    }

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

        _controls.Player.Jump.started += context => HandleJump();

        _controls.Player.Movement.performed += context => _moveAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.performed += context => _playerAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.performed += context => _moving = true;
        _controls.Player.Movement.performed += context => HandleTurn();
        _controls.Player.Movement.canceled += context => _moveAxis = Vector2.zero;
        _controls.Player.Movement.canceled += context => _moving = false;

        _controls.Player.Interact.performed += context => HandleInteraction();

        _controls.Player.Glide.performed += context => HandleGlide();
        _controls.Player.Glide.canceled += context => EndGlide();

        _controls.Player.Dash.performed += context => HandleDash();

        _controls.Player.Pause.performed += context => HandlePause();
    }

    private void Start()
    {
        _interacting = false;
        _cam = Camera.main;
    }

    private void FixedUpdate()
    {
        //anim.SetBool("Pause", pause);

        if (!inDialogue)
        {
            //Unfreezes player if not paused and only goes through these methods during unpaused state
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            AnimatorHandler();

            if (_anim.GetFloat("Speed") > 0.1f)
            {
                PlayerFacing();
            }
            MovementHandler();
        }
        else {
            //Freezes player based on constraints during pause
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            AnimatorHandler();
            PlayerFacing(interactable);
        }
    }

    //Changes animation states when needed, depending on the current state of the player.
    private void AnimatorHandler()
    {
        //Setting Velocity to different values
        if (_rb.velocity.y < -0.01f || (_rb.velocity.y > 0.01f && !_jumping))
        {
            _anim.SetBool("Falling", true);
        }

        if (_rb.velocity.y > 0)
        {
            //anim.SetBool("Jumping", true);  
            //anim.SetBool("Falling", false);
        }

        if (_anim.GetBool("Tired") && state != 3)
        {
            state = 0;
        }

        //Setting the Speed to different values to be used by the blend trees
        if (!(new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude == 0) && _moving)
        {
            _magSpeed = Mathf.Lerp(_magSpeed, new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude, _speedCharge);
        }
        else {
            _magSpeed = 0;
        }
        _magSpeed = Mathf.Abs(_magSpeed);

        _anim.SetFloat("Speed", _magSpeed);
        _anim.SetFloat("YSpeed", _magYSpeed);

        if (_magSpeed == 0 && _magYSpeed == 0)
        {
            idleTimer += Time.deltaTime;
        }
        else {
            idleTimer = 0;
        }

        if (idleTimer > _idleLimit)
        {
            idle2 = true;
        }
        else {
            idle2 = false;
        }

        _anim.SetBool("Idle2", idle2);


        //Setting grounded and gliding values based on booleans that are handled at other stages in the script
        _anim.SetBool("Grounded", _grounded);
        _anim.SetBool("Glide", gliding);

    }

    //Handles the movement of the player depending on their current state and any actions that are taking place.
    private void MovementHandler()
    {

        //Handles how the dash works, so if dash is pressed player is rocketed forward by a set amount of speed that wont cause them to clip into walls
        if (!_dashing)
        {
            Vector3 forward = _cam.transform.forward;
            Vector3 right = _cam.transform.right;

            forward.y = 0f;
            right.y = 0f;

            _magYSpeed = _rb.velocity.y;

            forward.Normalize();
            right.Normalize();

            Vector3 inputX = new Vector3(_moveAxis.x, 0f, 0f);
            Vector3 inputZ = new Vector3(0f, 0f, _moveAxis.y);

            _moveDir = forward * _moveAxis.y + right * _moveAxis.x;

            if (!_check.frontStop && !_check.backStop && !_check.rightStop && !_check.leftStop && !stuck)
            {
                _rb.velocity = new Vector3(_moveDir.x * speed, _rb.velocity.y, _moveDir.z * speed);
            }
            else
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            }
        }
        else
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
        if (gliding)
        {
            if (_rb.velocity.y < -1f)
            {
                _rb.velocity += Vector3.down * glideFactor * Physics.gravity.y * Time.deltaTime;
            }
        }

        //Makes fall feel faster in order to be able to distinguish between normal fall and glide
        if (!gliding && _rb.velocity.y < 0.1)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * 2.5f * (2.5f - 1) * Time.deltaTime;
        }

        if (state.Equals(0) || state.Equals(2))
        {
            _secondJump = false;
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
        if (_canDash && state.Equals(2))
        {
            _dashing = true;
            _anim.SetBool("Dash", true);
            _canDash = false;
        }
    }

    //Handles what happens when any of the buttons that entail a glide are pressed.
    private void HandleGlide()
    {
        if (!_grounded && state.Equals(2))
        {
            gliding = true;
        }
        else
        {
            gliding = false;
        }
    }

    //Handles what happens when any of the buttons that entail a glide are let go.
    private void EndGlide()
    {
        gliding = false;
    }

    //Handles what happens when any of the buttons that entail an interaction are pressed.
    private void HandleInteraction()
    {

        if (interactable != null)
        {
            Interaction interactableObject = interactable.GetComponent<Interaction>();

            print("Interacting with " + interactable.name);

            if (interactable.GetComponent<Interaction>().NPC)
            {
                if (!_interacting)
                {
                    interactable.GetComponent<NPCDialogueTrigger>().TriggerDialogue();
                    _interacting = true;
                }
            }

            if (interactable.GetComponent<Interaction>().Button)
            {
                ButtonManager button = interactable.GetComponent<ButtonManager>();
                button.interactionHandler();
                _anim.SetTrigger("Interaction");

                //_INSERT INTERACTION NOISES HERE(Restricted to interactions with buttons)_
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

    private IEnumerator onStuck()
    {
        stuck = true;
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        yield return new WaitForSeconds(Mathf.Epsilon);
        stuck = false;
    }

    public void Grounded()
    {
        if (!_jumpPrep)
        {
            _grounded = true;
            _secondJump = true;
            gliding = false;
            _canDash = true;
            _anim.SetBool("Falling", false);
            _anim.SetBool("Jumping", false);
            _anim.SetBool("Dash", false);
            _anim.SetBool("DoubleJump", false);
        }
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 pos;
        Vector3 rot;

        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];

        rot.x = data.rotation[0];
        rot.y = data.rotation[1];
        rot.z = data.rotation[2];

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
    }

    //Handles what happens when any of the buttons that entail a jump are pressed.
    private void HandleJump()
    {
        if (!pause)
        {
            if (!_grounded && _secondJump && !_interacting && state.Equals(1) && !inDialogue)
            {
                _anim.SetBool("DoubleJump", true);
                _rb.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * 2f * _jumpHeight));
                _secondJump = false;

                //_INSERT DOUBLE JUMP SOUNDS HERE_
                //_INSERT DOUBLE JUMP PARTICLES HERE_
            }

            if (_grounded && !_interacting && !inDialogue)
            {
                _anim.SetBool("Jumping", true);
                _jumpPrep = true;
                StartCoroutine(setGrounded());

                // _INSERT JUMP SOUNDS HERE_
            }

            if (_interacting)
            {
                _dManager.DisplayNextSentence();
            }
        }
    }

    //Returns the quaternion angle that the player should be facing when they move.
    private void PlayerFacing()
    {
        //if (_playerAxis.magnitude > 0.1f) { _facingAngle = Mathf.Atan2(-_playerAxis.y, _playerAxis.x) * Mathf.Rad2Deg + 360; }
        //Quaternion target = Quaternion.Euler(transform.rotation.x, _facingAngle, transform.rotation.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothing);
        if (!inDialogue && _moving)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDir) * Quaternion.Euler(0, OFFSET, 0), Time.deltaTime * smoothing);
        }
    }

    private void PlayerFacing(GameObject lookAt)
    {
        if (lookAt.gameObject.name != "Elder")
        {
            Vector3 lookDir = new Vector3(lookAt.transform.position.x - transform.position.x, 0, lookAt.transform.position.z - transform.position.z);
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * Quaternion.Euler(0, OFFSET, 0), Time.deltaTime * smoothing);
        }
    }

    //Enables controls when this object is enabled
    private void OnEnable()
    {
        _controls.Enable();
    }

    private IEnumerator setGrounded()
    {
        yield return new WaitForSeconds(_jumpDelay);
        _rb.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * 1.35f * _jumpHeight));
        yield return new WaitForSeconds(0.01f);
        _grounded = false;
        _jumpPrep = false;
    }

    //Disables controls when this object is enabled
    private void OnDisable()
    {
        _controls.Disable();
    }
}
