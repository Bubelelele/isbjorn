using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerInput _input;
    
    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float movementMultiplier = 80.0f;
    [SerializeField] private float runMultiplier = 1.75f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool playerIsGrounded;
    [SerializeField] [Range(0.0f, 1.8f)] private float groundCheckRadiusMultiplier = 0.9f;
    [SerializeField] [Range(-0.95f, 1.05f)] private float groundCheckDistance = 0.05f;
    [SerializeField] private bool isOnSlope;
    
    [Header("Jumping")]
    [SerializeField] private float initialJumpForce = 750.0f;
    [SerializeField] private float continualJumpForceMultiplier = 0.1f;
    [SerializeField] private float jumpTime = 0.175f;
    [SerializeField] private float jumpTimeCounter;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferTimeCounter;
    [SerializeField] private bool playerIsJumping;
    [SerializeField] private float maximumJumpHeight = 1.0f;
    [SerializeField] private float maximumJumpTime = 0.5f;
    [SerializeField] private bool isFalling;

    [Header("Gravity")]
    [SerializeField] private float currentFallGravity;
    [SerializeField] private float maximumGravity = -500.0f;
    [SerializeField] [Range(-100.0f, -5.0f)] private float incrementAmount = -20.0f;
    [SerializeField] private float incrementFrequency = 0.05f;
    [SerializeField] private float playerFallTimer;
    [SerializeField] private float groundedGravity = -1.0f;
    [SerializeField] private float gravity = -9.8f;

    [Header("Rolling")]
    [SerializeField] private bool groundAngleRollable;
    [SerializeField] private bool isRolling;
    [SerializeField] private float groundSlopeAngle;
    [SerializeField] [Range(-90f, 0f)] private float maxRollableSlopeAngle = -45f;
    [SerializeField] private float drag = 10.0f;
    [SerializeField] private float rollMultiplier = 80.0f;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _state;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Transform _mainCameraTransform;
    private Transform _playerTransform;
    private Vector3 _playerPosition;
    private Vector3 _playerMovement;
    private bool _animationPlaying;
    private float _animationPlayingTimer;
    private RaycastHit _groundCheckHit;
    private CapsuleCollider _capsuleCollider;
    private Quaternion _slopeAngleRotation;
    private Vector3 _globalForward;
    private float _relativeSlopeAngle;
    private Transform _bearTransform;

    // Getters and setters.
    public float RollMultiplier => rollMultiplier;
    public Vector3 GlobalForward => _globalForward;
    public float Drag
    {
        get => drag;
        set => drag = value;
    }

    public float GroundSlopeAngle
    {
        get => groundSlopeAngle;
        set => groundSlopeAngle = value;
    }

    public bool IsFalling => isFalling;
    public float MaximumJumpHeight => maximumJumpHeight;
    public float MaximumJumpTime => maximumJumpTime;
    public float Gravity
    {
        get => gravity;
        set => gravity = value;
    }

    public bool IsOnSlope
    {
        get => isOnSlope;
        set => isOnSlope = value;
    }

    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }
    public PlayerInput Input => _input;
    public float JumpBufferTime => jumpBufferTime;
    public float JumpBufferTimeCounter
    {
        get => jumpBufferTimeCounter;
        set => jumpBufferTimeCounter = value;
    }
    public float CoyoteTimeCounter
    {
        get => coyoteTimeCounter;
        set => coyoteTimeCounter = value;
    }
    public float JumpTimeCounter
    {
        get => jumpTimeCounter;
        set => jumpTimeCounter = value;
    }
    public float JumpTime => jumpTime;
    public float CoyoteTime => coyoteTime;
    public float ContinualJumpForceMultiplier => continualJumpForceMultiplier;
    public float InitialJumpForce
    {
        get => initialJumpForce;
        set => initialJumpForce = value;
    }

    public bool PlayerIsGrounded
    {
        get => playerIsGrounded;
        set => playerIsGrounded = value;
    }
    public Rigidbody Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public bool PlayerIsJumping
    {
        get => playerIsJumping;
        set => playerIsJumping = value;
    }
    public float PlayerFallTimer
    {
        get => playerFallTimer;
        set => playerFallTimer = value;
    }
    public float CurrentFallGravity
    {
        get => currentFallGravity;
        set => currentFallGravity = value;
    }
    public float GroundedGravity => groundedGravity;
    public float IncrementAmount => incrementAmount;
    public float MaximumGravity => maximumGravity;
    public float IncrementFrequency => incrementFrequency;
    public float PlayerMovementZ
    {
        get => _playerMovement.z;
        set => _playerMovement.z = value;
    }

    public float PlayerMovementX
    {
        get => _playerMovement.x;
        set => _playerMovement.x = value;
    }

    public float PlayerMovementY
    {
        get => _playerMovement.y;
        set => _playerMovement.y = value;
    }

    public Vector3 PlayerMovement
    {
        get => _playerMovement;
        set => _playerMovement = value;
    }

    public bool GroundAngleRollable
    {
        get => groundAngleRollable;
        set => groundAngleRollable = value;
    }

    public bool IsRolling
    {
        get => isRolling;
        set => isRolling = value;
    }
    
    public float MaxRollableSlopeAngle {
        get => maxRollableSlopeAngle;
        set => maxRollableSlopeAngle = value;
    }

    public Transform PlayerTransform => _playerTransform;
    public float MovementMultiplier => movementMultiplier;
    public float RunMultiplier => runMultiplier;
    public RaycastHit GroundCheckHit => _groundCheckHit;
    public Vector3 PlayerPosition => _playerPosition;
    public Quaternion SlopeAngleRotation
    {
        get => _slopeAngleRotation;
        set => _slopeAngleRotation = value;
    }

    public float RelativeSlopeAngle
    {
        get => _relativeSlopeAngle;
        set => _relativeSlopeAngle = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerTransform = _rigidbody.transform;
        _bearTransform = _playerTransform.GetChild(0).GetChild(0);
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
        _animator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        _input = FindObjectOfType<PlayerInput>();
        SetupJumpVariables();
        
        _state = new PlayerStateFactory(this);
        _currentState = _state.Grounded();
        _currentState.EnterState();
    }

    private void FixedUpdate()
    {
        PlayerIsGrounded = PlayerGroundCheck();
        _playerMovement = MoveDirection();
        _currentState.UpdateStates();
        
        _rigidbody.AddRelativeForce(_playerMovement, ForceMode.Force);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Invoke(nameof(LockCursor), 0.2f);
        else if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            Cursor.lockState = CursorLockMode.None;
        }
        
        PlayerLookRelativeToCamera();
        Debug.DrawRay(_playerPosition, _playerTransform.TransformDirection(_playerMovement), Color.red);
        Debug.Log(GroundSlopeAngle);
    }
    
    private void SetupJumpVariables()
    {
        var timeToApex = maximumJumpTime * 0.5f;
        currentFallGravity = -2 * maximumJumpHeight / Mathf.Pow(timeToApex, 2);
        initialJumpForce = 1000 * maximumJumpHeight / timeToApex;
    }

    private Vector3 MoveDirection()
    {
        return new Vector3(_input.MoveInput.x, 0.0f, _input.MoveInput.y);
    }
    
    private void PlayerLookRelativeToCamera()
    {
        _globalForward = _mainCameraTransform.forward.normalized;
        var right = _mainCameraTransform.right.normalized;
        _globalForward.y = 0;
        right.y = 0;

        var relativeForwardLookDirection = MoveDirection().z * _globalForward;
        var relativeRightLookDirection = MoveDirection().x * right;

        var lookDirection = relativeForwardLookDirection + relativeRightLookDirection;

        if (_input.MoveIsPressed && !_input.RollIsPressed)
        {
            _playerTransform.forward = _globalForward;
            _bearTransform.forward = Vector3.Slerp(_bearTransform.forward, lookDirection, rotationSpeed * Time.deltaTime);
        }
        else if (_input.RollIsPressed)
            _bearTransform.forward = _playerTransform.forward = _globalForward;
    }

    private bool PlayerGroundCheck()
    {
        _playerPosition = _rigidbody.position;
        
        var sphereCastRadius = _capsuleCollider.radius * groundCheckRadiusMultiplier;
        var sphereCastTravelDistance = _capsuleCollider.bounds.extents.y - sphereCastRadius + groundCheckDistance;

        return Physics.SphereCast(_playerPosition + _capsuleCollider.center, sphereCastRadius, Vector3.down, out _groundCheckHit, sphereCastTravelDistance, groundLayerMask);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
}