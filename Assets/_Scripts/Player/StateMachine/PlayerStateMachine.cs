using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private bool isOnSlope;
    [SerializeField] private float continualJumpForceMultiplier = 0.1f;
    [SerializeField] private float jumpTime = 0.175f;
    [SerializeField] private float jumpTimeCounter;
    [SerializeField] private bool playerIsJumping;
    [SerializeField] private float maximumJumpHeight = 1.0f;
    [SerializeField] private float maximumJumpTime = 0.5f;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool groundAngleRollable;
    [SerializeField] private bool isRolling;
    [SerializeField] private float groundSlopeAngle;
    [SerializeField] [Range(-90f, 0f)] private float maxRollableSlopeAngle = -45f;
    [SerializeField] private float rollMultiplier = 80.0f;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _state;
    private Rigidbody _rigidbody;
    private Transform _mainCameraTransform;
    private Transform _playerTransform;
    private Vector3 _playerPosition;
    private Vector3 _playerMovement;
    private bool _animationPlaying;
    private float _animationPlayingTimer;
    private CapsuleCollider _capsuleCollider;
    private Quaternion _slopeAngleRotation;
    private Vector3 _globalForward;
    private float _relativeSlopeAngle;
    private Transform _bearTransform;
    private float _fallAnimationTimer = 0.5f;
    public CharacterController characterController;
    public RollingScript rollingScript;
    public SlopeDetection slopeDetection;
    public MovementScript movementScript;
    public JumpingScript jumpingScript;
    public GameObject groundCheck;
    public GameObject rotationPivot;
    public GameObject rollPivot;
    public bool landedOnWalrus;
    public LayerMask walrusLayerMask;

    // Getters and setters.
    public float RollMultiplier
    {
        get => rollMultiplier;
        set => rollMultiplier = value;
    }

    public Vector3 GlobalForward
    {
        get => _globalForward;
        set => _globalForward = value;
    }

    public float GroundSlopeAngle { get => groundSlopeAngle; set => groundSlopeAngle = value; }
    public bool IsFalling
    {
        get => isFalling;
        set => isFalling = value;
    }

    public float MaximumJumpHeight
    {
        get => maximumJumpHeight;
        set => maximumJumpHeight = value;
    }

    public float MaximumJumpTime
    {
        get => maximumJumpTime;
        set => maximumJumpTime = value;
    }

    public bool IsOnSlope { get => isOnSlope; set => isOnSlope = value; }
    public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }
    public float JumpTimeCounter { get => jumpTimeCounter; set => jumpTimeCounter = value; }
    public float JumpTime
    {
        get => jumpTime;
        set => jumpTime = value;
    }

    public float ContinualJumpForceMultiplier
    {
        get => continualJumpForceMultiplier;
        set => continualJumpForceMultiplier = value;
    }

    public Rigidbody Rigidbody
    {
        get => _rigidbody;
        set => _rigidbody = value;
    }

    public bool PlayerIsJumping { get => playerIsJumping; set => playerIsJumping = value; }
    public float IncrementAmount { get => incrementAmount; set => incrementAmount = value; }
    public float MaximumGravity
    {
        get => maximumGravity;
        set => maximumGravity = value;
    }

    public float IncrementFrequency
    {
        get => incrementFrequency;
        set => incrementFrequency = value;
    }

    public float PlayerMovementZ { get => _playerMovement.z; set => _playerMovement.z = value; }
    public float PlayerMovementX { get => _playerMovement.x; set => _playerMovement.x = value; }
    public float PlayerMovementY { get => _playerMovement.y; set => _playerMovement.y = value; }
    public Vector3 PlayerMovement { get => _playerMovement; set => _playerMovement = value; }
    public bool GroundAngleRollable { get => groundAngleRollable; set => groundAngleRollable = value; }
    public bool IsRolling { get => isRolling; set => isRolling = value; }
    public float MaxRollableSlopeAngle { get => maxRollableSlopeAngle; set => maxRollableSlopeAngle = value; }
    public float FallAnimationTimer { get => _fallAnimationTimer; set => _fallAnimationTimer = value; }
    public Transform PlayerTransform
    {
        get => _playerTransform;
        set => _playerTransform = value;
    }

    public Vector3 PlayerPosition
    {
        get => _playerPosition;
        set => _playerPosition = value;
    }

    public Quaternion SlopeAngleRotation { get => _slopeAngleRotation; set => _slopeAngleRotation = value; }
    public float RelativeSlopeAngle { get => _relativeSlopeAngle; set => _relativeSlopeAngle = value; }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Organised variables.
    [Header("Ground Check")]
    [SerializeField] [Range(0.0f, 1.8f)] private float sphereRadiusMultiplier = 0.9f;
    [SerializeField] [Range(-0.95f, 1.05f)] private float groundCheckDistance = 0.05f;
    [SerializeField] private LayerMask groundLayerMask;
    private RaycastHit _groundCheckHit;
    
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.5f;
    [SerializeField] private float runMultiplier = 2.0f;
    private Vector3 _movementVector;
    private const float Drag = 25.0f;

    [Header("Gravity")]
    [SerializeField] private float currentGravity;
    [SerializeField] private float minimumGravity = 0.0f;
    [SerializeField] private float maximumGravity = -100.0f;
    [SerializeField] [Range(-10.0f, -1.0f)] private float incrementAmount = -1.0f;
    [SerializeField] private float incrementFrequency = 0.05f;
    [SerializeField] private float playerInAirTimer;
    [SerializeField] private float appliedGravity;
    
    [Header("Jump")]
    [SerializeField] private float initialVelocity = 10.0f;
    [SerializeField] [Range(1.0f, 10.0f)] private float riseDecrementAmount = 1.0f;
    [SerializeField] [Range(-10.0f, -1.0f)] private float fallIncrementAmount = -1.0f;

    [Header("Jump Timers")]
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float coyoteTimer;
    // [SerializeField] private float jumpBufferTime = 0.2f;
    // [SerializeField] private float jumpBufferTimer;
    
    // Organised getters and setters.
    // General
    public PlayerInput Input { get; private set; }
    public Animator Animator { get; private set; }
    // Ground Check
    public RaycastHit GroundCheckHit
    {
        get => _groundCheckHit;
        set => _groundCheckHit = value;
    }

    public bool PlayerIsGrounded { get; private set; }
    // Movement
    public Vector3 MovementDirection { get; set; }
    public Vector3 MovementVector { get => _movementVector; set => _movementVector = value; }
    public float MovementVectorX { get => _movementVector.x; set => _movementVector.x = value; }
    public float MovementVectorY { get => _movementVector.y; set => _movementVector.y = value; }
    public float MovementVectorZ { get => _movementVector.z; set => _movementVector.z = value; }
    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public float RunMultiplier
    {
        get => runMultiplier;
        set => runMultiplier = value;
    }

    public float CounterDragMultiplier
    {
        get => Drag * 2.0f;
        set => throw new NotImplementedException();
    }

    // Gravity
    public float CurrentGravity { get => currentGravity; set => currentGravity = value; }
    public float MinimumGravity
    {
        get => minimumGravity;
        set => minimumGravity = value;
    }

    public float PlayerInAirTimer { get => playerInAirTimer; set => playerInAirTimer = value; }
    public float AppliedGravity { get => appliedGravity; set => appliedGravity = value; }
    // Jump
    public float RiseDecrementAmount
    {
        get => riseDecrementAmount;
        set => riseDecrementAmount = value;
    }

    public float FallIncrementAmount
    {
        get => fallIncrementAmount;
        set => fallIncrementAmount = value;
    }

    public float InitialVelocity
    {
        get => initialVelocity;
        set => initialVelocity = value;
    }

    // public bool JumpIsQueued { get; set; }
    // public bool JumpWasPressedLastFrame { get; set; }
    // Jump Timers
    public float CoyoteTimer { get => coyoteTimer; set => coyoteTimer = value; }
    public float CoyoteTime
    {
        get => coyoteTime;
        set => coyoteTime = value;
    }

    // public float JumpBufferTimer { get => jumpBufferTimer; set => jumpBufferTimer = value; }
    // public float JumpBufferTime => jumpBufferTime;
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        InitializeVariables();
        _state = new PlayerStateFactory(this);
        _currentState = _state.Grounded();
        _currentState.EnterState();
    }

    private void FixedUpdate()
    {
        PlayerIsGrounded = PlayerGroundCheck();
        _movementVector = new Vector3(Input.MoveInput.x, 0.0f, Input.MoveInput.y);
        _currentState.UpdateStates();
        _movementVector = Vector3.ProjectOnPlane(_movementVector, _groundCheckHit.normal);
        Debug.DrawRay(_playerPosition, _movementVector, Color.red);
        _rigidbody.AddRelativeForce(_movementVector * CounterDragMultiplier, ForceMode.Force);
    }

    private void Update()
    {
        CursorLockToggle();
        MovementDirection = MoveInput();
        PlayerLookRelativeToCamera();
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Just for Tormod and Edvart to greybox levels.

    private void PlayerLookRelativeToCamera()
    {
        var forward = _mainCameraTransform.forward.normalized;
        var right = _mainCameraTransform.right.normalized;
        forward.y = 0;
        right.y = 0;
    
        var relativeForwardLookDirection = _movementVector.z * forward;
        var relativeRightLookDirection = _movementVector.x * right;
    
        var lookDirection = relativeForwardLookDirection + relativeRightLookDirection;

        if (!Input.MoveIsPressed) return;
        // _bearTransform.forward = Vector3.Slerp(_bearTransform.forward, lookDirection, rotationSpeed * Time.deltaTime);
    }
    private void InitializeVariables()
    {
        Input = FindObjectOfType<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        Animator = GetComponentInChildren<Animator>();
        _rigidbody.drag = Drag;

        // Just for Tormod and Edvart to greybox levels.
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
        _playerTransform = _rigidbody.transform;
        _bearTransform = _playerTransform.GetChild(0).GetChild(0);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Working as intended, values not yet tweaked.
    private bool PlayerGroundCheck()
    {
        _playerPosition = _rigidbody.position;
        var sphereCastRadius = _capsuleCollider.radius * sphereRadiusMultiplier;
        var sphereCastTravelDistance = _capsuleCollider.bounds.extents.y - sphereCastRadius + groundCheckDistance;
        return Physics.SphereCast(_playerPosition + _capsuleCollider.center, sphereCastRadius, Vector3.down, out _groundCheckHit, sphereCastTravelDistance, groundLayerMask);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_capsuleCollider == null) return;
        var sphereCastRadius = _capsuleCollider.radius * sphereRadiusMultiplier;
        var sphereCastTravelDistance = new Vector3(0.0f, _capsuleCollider.bounds.extents.y - sphereCastRadius + groundCheckDistance, 0.0f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(_playerPosition + _capsuleCollider.center - sphereCastTravelDistance, sphereCastRadius);
    }
    
    // Working as intended with values tweaked.
    private Vector3 MoveInput()
    {
        return new Vector3(Input.MoveInput.x, 0.0f, Input.MoveInput.y);
    }
    private void CursorLockToggle()
    {
        switch (Cursor.lockState == CursorLockMode.Locked)
        {
            case true when Keyboard.current.escapeKey.wasPressedThisFrame:
                Cursor.lockState = CursorLockMode.None;
                break;
            case false when Mouse.current.leftButton.wasPressedThisFrame:
                Invoke(nameof(LockCursor), 0.2f);
                break;
        }
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}