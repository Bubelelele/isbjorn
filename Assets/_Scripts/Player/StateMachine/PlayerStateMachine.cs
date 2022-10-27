using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    // Getters and setters.
    public float RollMultiplier => rollMultiplier;
    public Vector3 GlobalForward => _globalForward;
    public float GroundSlopeAngle { get => groundSlopeAngle; set => groundSlopeAngle = value; }
    public bool IsFalling => isFalling;
    public float MaximumJumpHeight => maximumJumpHeight;
    public float MaximumJumpTime => maximumJumpTime;
    public bool IsOnSlope { get => isOnSlope; set => isOnSlope = value; }
    public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }
    public float JumpTimeCounter { get => jumpTimeCounter; set => jumpTimeCounter = value; }
    public float JumpTime => jumpTime;
    public float ContinualJumpForceMultiplier => continualJumpForceMultiplier;
    public Rigidbody Rigidbody => _rigidbody;
    public bool PlayerIsJumping { get => playerIsJumping; set => playerIsJumping = value; }
    public float IncrementAmount { get => incrementAmount; set => incrementAmount = value; }
    public float MaximumGravity => maximumGravity;
    public float IncrementFrequency => incrementFrequency;
    public float PlayerMovementZ { get => _playerMovement.z; set => _playerMovement.z = value; }
    public float PlayerMovementX { get => _playerMovement.x; set => _playerMovement.x = value; }
    public float PlayerMovementY { get => _playerMovement.y; set => _playerMovement.y = value; }
    public Vector3 PlayerMovement { get => _playerMovement; set => _playerMovement = value; }
    public bool GroundAngleRollable { get => groundAngleRollable; set => groundAngleRollable = value; }
    public bool IsRolling { get => isRolling; set => isRolling = value; }
    public float MaxRollableSlopeAngle { get => maxRollableSlopeAngle; set => maxRollableSlopeAngle = value; }
    public float FallAnimationTimer { get => _fallAnimationTimer; set => _fallAnimationTimer = value; }
    public Transform PlayerTransform => _playerTransform;
    public Vector3 PlayerPosition => _playerPosition;
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
    public RaycastHit GroundCheckHit => _groundCheckHit;
    public bool PlayerIsGrounded { get; private set; }
    // Movement
    public Vector3 MovementDirection { get; set; }
    public Vector3 MovementVector { get => _movementVector; set => _movementVector = value; }
    public float MovementVectorX { get => _movementVector.x; set => _movementVector.x = value; }
    public float MovementVectorY { get => _movementVector.y; set => _movementVector.y = value; }
    public float MovementVectorZ { get => _movementVector.z; set => _movementVector.z = value; }
    public float MovementSpeed => movementSpeed;
    public float RunMultiplier => runMultiplier;
    public float CounterDragMultiplier => Drag * 2.0f;
    // Gravity
    public float CurrentGravity { get => currentGravity; set => currentGravity = value; }
    public float MinimumGravity => minimumGravity;
    public float PlayerInAirTimer { get => playerInAirTimer; set => playerInAirTimer = value; }
    public float AppliedGravity { get => appliedGravity; set => appliedGravity = value; }
    // Jump
    public float RiseDecrementAmount => riseDecrementAmount;
    public float FallIncrementAmount => fallIncrementAmount;
    public float InitialVelocity => initialVelocity;
    // public bool JumpIsQueued { get; set; }
    // public bool JumpWasPressedLastFrame { get; set; }
    // Jump Timers
    public float CoyoteTimer { get => coyoteTimer; set => coyoteTimer = value; }
    public float CoyoteTime => coyoteTime;
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
        _currentState.UpdateStates();
        Debug.DrawRay(_playerPosition, MovementVector, Color.red);
        _rigidbody.AddRelativeForce(MovementVector * CounterDragMultiplier, ForceMode.Force);
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
        _globalForward = _mainCameraTransform.forward.normalized;
        var right = _mainCameraTransform.right.normalized;
        _globalForward.y = 0;
        right.y = 0;
    
        var relativeForwardLookDirection = MovementDirection.z * _globalForward;
        var relativeRightLookDirection = MovementDirection.x * right;
    
        var lookDirection = relativeForwardLookDirection + relativeRightLookDirection;
    
        if (Input.MoveIsPressed)
        {
            _playerTransform.forward = _globalForward;
            _bearTransform.forward = Vector3.Slerp(_bearTransform.forward, lookDirection, rotationSpeed * Time.deltaTime);
        }
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

    private Vector3 HandleSlopes()
    {
        var playerUp = PlayerTransform.up;
        var localGroundCheckHitNormal = PlayerTransform.InverseTransformDirection(GroundCheckHit.normal);
        GroundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, playerUp);
        SlopeAngleRotation = Quaternion.FromToRotation(playerUp, localGroundCheckHitNormal);
        return SlopeAngleRotation * MovementVector;
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
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Invoke(nameof(LockCursor), 0.2f);
        else if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}