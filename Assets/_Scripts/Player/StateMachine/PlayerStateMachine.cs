using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;
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
    private CapsuleCollider _capsuleCollider;
    private Quaternion _slopeAngleRotation;
    private Vector3 _globalForward;
    private float _relativeSlopeAngle;
    private Transform _bearTransform;
    private float _fallAnimationTimer = 0.5f;
    
    // Getters and setters.
    public float RollMultiplier => rollMultiplier;
    public Vector3 GlobalForward => _globalForward;
    public float Drag { get => drag; set => drag = value; }
    public float GroundSlopeAngle { get => groundSlopeAngle; set => groundSlopeAngle = value; }
    public bool IsFalling => isFalling;
    public float MaximumJumpHeight => maximumJumpHeight;
    public float MaximumJumpTime => maximumJumpTime;
    public bool IsOnSlope { get => isOnSlope; set => isOnSlope = value; }
    public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }
    public float JumpBufferTime => jumpBufferTime;
    public float JumpBufferTimeCounter { get => jumpBufferTimeCounter; set => jumpBufferTimeCounter = value; }
    public float CoyoteTimeCounter { get => coyoteTimeCounter; set => coyoteTimeCounter = value; }
    public float JumpTimeCounter { get => jumpTimeCounter; set => jumpTimeCounter = value; }
    public float JumpTime => jumpTime;
    public float CoyoteTime => coyoteTime;
    public float ContinualJumpForceMultiplier => continualJumpForceMultiplier;
    public float InitialJumpForce { get => initialJumpForce; set => initialJumpForce = value; }
    public Rigidbody Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public bool PlayerIsJumping { get => playerIsJumping; set => playerIsJumping = value; }
    public float IncrementAmount => incrementAmount;
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
    private Vector3 _movementVector;
    [SerializeField] private float movementSpeed = 30.0f;
    [SerializeField] private float runMultiplier = 1.75f;
    
    [Header("Gravity")]
    [SerializeField] private float currentGravity;
    [SerializeField] private float minimumGravity = -100.0f;
    [SerializeField] private float maximumGravity = -500.0f;
    [SerializeField] [Range(-35.0f, -5.0f)] private float incrementAmount = -20.0f;
    [SerializeField] private float incrementFrequency = 0.05f;
    [SerializeField] private float playerFallTimer;
    [SerializeField] private float gravity;

    // Organised getters and setters.
    public PlayerInput Input { get; private set; }
    // Ground Check
    public RaycastHit GroundCheckHit => _groundCheckHit;
    public bool PlayerIsGrounded { get; private set; }
    // Movement
    public Vector3 MovementDirection { get; set; }
    public float MovementVectorX { get => _movementVector.x; set => _movementVector.x = value; }
    public float MovementVectorY { get => _movementVector.y; set => _movementVector.y = value; }
    public float MovementVectorZ { get => _movementVector.z; set => _movementVector.z = value; }
    public float MovementSpeed => movementSpeed;
    public float RunMultiplier => runMultiplier;
    // Gravity
    public float CurrentGravity { get => currentGravity; set => currentGravity = value; }
    public float MinimumGravity => minimumGravity;
    public float PlayerFallTimer { get => playerFallTimer; set => playerFallTimer = value; }
    public float Gravity { get => gravity; set => gravity = value; }

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
        Debug.DrawRay(_playerPosition, _movementVector, Color.red);
        Debug.Log(_movementVector);
        _rigidbody.AddRelativeForce(_movementVector, ForceMode.Force);
    }

    private void Update()
    {
        CursorLockToggle();
        MovementDirection = MoveInput();
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void SetupJumpVariables()
    {
        var timeToApex = maximumJumpTime * 0.5f;
        currentGravity = -2 * maximumJumpHeight / Mathf.Pow(timeToApex, 2);
        initialJumpForce = 1000 * maximumJumpHeight / timeToApex;
    }

    // private void PlayerLookRelativeToCamera()
    // {
    //     _globalForward = _mainCameraTransform.forward.normalized;
    //     var right = _mainCameraTransform.right.normalized;
    //     _globalForward.y = 0;
    //     right.y = 0;
    //
    //     var relativeForwardLookDirection = MoveDirection().z * _globalForward;
    //     var relativeRightLookDirection = MoveDirection().x * right;
    //
    //     var lookDirection = relativeForwardLookDirection + relativeRightLookDirection;
    //
    //     if (Input.MoveIsPressed && !Input.RollIsPressed)
    //     {
    //         _playerTransform.forward = _globalForward;
    //         _bearTransform.forward = Vector3.Slerp(_bearTransform.forward, lookDirection, rotationSpeed * Time.deltaTime);
    //     }
    //     else if (Input.RollIsPressed)
    //         _bearTransform.forward = _playerTransform.forward = _globalForward;
    // }
    
    private void InitializeVariables()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerTransform = _rigidbody.transform;
        _bearTransform = _playerTransform.GetChild(0).GetChild(0);
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
        _animator = GetComponentInChildren<Animator>();
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        Input = FindObjectOfType<PlayerInput>();
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