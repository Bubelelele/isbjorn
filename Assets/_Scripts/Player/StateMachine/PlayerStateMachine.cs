using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // General.
    public static Transform StaticPlayerTransform;
    public PlayerBaseState CurrentState { get; set; }
    public PlayerInput Input { get; private set; }
    public Vector3 MainCameraForward => _mainCameraForward;
    public Transform BearTransform { get; private set; }
    public Animator Animator { get; private set; }
    public bool AnimationEnded { get; set; }
    public AudioSource[] AudioSources { get; private set; }

    // Basic Movement.
    public Vector3 MovementVector { get => _movementVector; set => _movementVector = value; }
    public float MovementSpeed => movementSpeed;
    public float RunMultiplier => runMultiplier;
    public float LookRotationSpeed => lookRotationSpeed;

    // Rolling.
    public float InitialRollingSpeed => initialRollingSpeed;
    public float MaxRollingSpeed => maxRollingSpeed;
    public float AccelerationRate => accelerationRate;
    public float DecelerationRate => decelerationRate;
    public float CurrentRollingSpeed { get => currentRollingSpeed; set => currentRollingSpeed = value; }
    
    // Gravity.
    public const float GroundedGravity = -1.0f;
    public float FallGravity => fallGravity;
    public float MaximumGravity => maximumGravity;
    public float CurrentGravity { get => currentGravity; set => currentGravity = value; }
    
    // Ground Info.
    public bool PlayerIsGrounded => playerIsGrounded;
    public float RelativeSlopeAngle { get; private set; }
    
    // Jumping.
    public float InitialJumpVelocity => initialJumpVelocity;
    public float JumpRiseGravity => jumpRiseGravity;
    public float JumpFallGravity => jumpFallGravity;
    public bool PlayerIsLandingJump { get; set; }
    public bool LandedOnWalrus { get; set; }
    
    // Jump Timers.
    public float JumpBufferTime => jumpBufferTime;
    public float JumpBufferTimer { get => jumpBufferTimer; set => jumpBufferTimer = value; }
    public float CoyoteTime => coyoteTime;
    public float CoyoteTimer { get => coyoteTimer; set => coyoteTimer = value; }
    
    // Sniffing.
    public ShowPath ShowPath { get; private set; }

    #region Inspector

    [Header("General")]
    [SerializeField] private bool isBearThin;
    [field: SerializeField] public MMFeedbacks LandingFeedback { get; private set; }
    [field: SerializeField] public MMFeedbacks RoarFeedback { get; private set; }
    [field: SerializeField] public MMFeedbacks SniffFeedback { get; private set; }
    [field: SerializeField] public MMFeedbacks JumpFeedback { get; private set; }
    [field: SerializeField] public MMFeedbacks WalrusFeedback { get; private set; }
    [field: SerializeField] public MMFeedbacks RollFeedback { get; private set; }
    [field: SerializeField] public ContinuousImpulse RollContinuousImpulse { get; private set; }
    [field: SerializeField] public Vector2 ImpulseStrengthRange { get; private set; } = new(.5f, 1.5f);

    [Header("Basic Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runMultiplier;
    [SerializeField] private float lookRotationSpeed;
    
    [Header("Rolling")]
    [SerializeField] private float initialRollingSpeed;
    [SerializeField] private float maxRollingSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float decelerationRate;
    [SerializeField] private float currentRollingSpeed;

    [Header("Gravity")]
    [SerializeField] private float fallGravity;
    [SerializeField] private float maximumGravity;
    [SerializeField] private float currentGravity;

    [Header("Ground Info")]
    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private bool playerIsGrounded;

    [Header("Jumping")]
    [SerializeField] private float initialJumpVelocity;
    [SerializeField] private float jumpRiseGravity;
    [SerializeField] private float jumpFallGravity;

    [Header("Jump Timers")]
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpBufferTimer;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimer;

    #endregion

    // General.
    private PlayerStateFactory _state;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidbody;
    private Transform _mainCameraTransform;
    private Vector3 _mainCameraForward;

    // Basic Movement.
    private Vector3 _movementVector;
    private const float _drag = 25.0f;
    private const float _counterDragMultiplier = _drag * 2.0f;
    private Quaternion _lastLookRotation;

    // Ground Info.
    private RaycastHit _groundCheckHitInfo;
    
    private void Awake()
    {
        InitializeVariables();
        _state = new PlayerStateFactory(this);
        CurrentState = _state.Grounded();
        CurrentState.EnterState();
    }
    
    private void FixedUpdate()
    {
        playerIsGrounded = GroundCheck();
        _rigidbody.AddRelativeForce(_movementVector * _counterDragMultiplier, ForceMode.Force);
    }
    
    private void Update()
    {
        _movementVector = CurrentState.CurrentSubState.LocksInput ? Vector3.zero : MoveInput();
        ProjectVectorToCameraCoordinateSpace(ref _movementVector);
        LookTowardsMovementVector();
        CurrentState.UpdateStates();
        _movementVector.y = currentGravity;
        ProjectVectorOnPlane(ref _movementVector);
        Debug.DrawRay(transform.position, _movementVector, Color.red);
        Debug.Log(CurrentState + " -> " + CurrentState.CurrentSubState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Walrus")) return;
        LandedOnWalrus = true;
    }

    private void InitializeVariables()
    {
        StaticPlayerTransform = transform;
        Input = FindObjectOfType<PlayerInput>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.drag = _drag;
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
        SelectBear(isBearThin);
        _lastLookRotation = BearTransform.rotation;
        Animator = BearTransform.GetComponent<Animator>();
        AudioSources = transform.GetChild(2).GetComponentsInChildren<AudioSource>();
        ShowPath = FindObjectOfType<ShowPath>();
    }

    private void SelectBear(bool isThin)
    {
        var selectedBear = isThin ? 1 : 0;
        var otherBear = 1 - selectedBear;
        BearTransform = transform.GetChild(selectedBear);
        BearTransform.gameObject.SetActive(true);
        transform.GetChild(otherBear).gameObject.SetActive(false);
    }

    private bool GroundCheck()
    {
        var capsuleColliderRadius = _capsuleCollider.radius;
        var origin = transform.position + new Vector3(0.0f, capsuleColliderRadius, 0.0f);
        const float rayCastTravelDistance = 2.0f;
        var sphereCastRadius = capsuleColliderRadius * 0.9f;
        var sphereCastTravelDistance = capsuleColliderRadius - sphereCastRadius + 0.45f;
        Physics.Raycast(origin, Vector3.down, out _groundCheckHitInfo, rayCastTravelDistance, groundCheckLayerMask);
        return Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out _, sphereCastTravelDistance, groundCheckLayerMask);
    }
    
    private Vector3 MoveInput()
    {
        return new Vector3(Input.MoveInput.x, 0.0f, Input.MoveInput.y);
    }
    
    private void ProjectVectorToCameraCoordinateSpace(ref Vector3 vectorToProject)
    {
        _mainCameraForward = _mainCameraTransform.forward;
        var right = _mainCameraTransform.right;
        _mainCameraForward.y = 0.0f;
        right.y = 0.0f;
        _mainCameraForward.Normalize();
        right.Normalize();
        vectorToProject = vectorToProject.x * right + vectorToProject.z * _mainCameraForward;
    }
    
    private void LookTowardsMovementVector()
    {
        ProjectVectorOnPlane(ref _movementVector);
        var targetLookRotation = _movementVector == Vector3.zero ? _lastLookRotation : Quaternion.LookRotation(_movementVector);
        var currentLookRotation = BearTransform.rotation;
        BearTransform.rotation = Quaternion.Slerp(currentLookRotation, targetLookRotation, lookRotationSpeed * Time.deltaTime);
        _lastLookRotation = currentLookRotation;
    }
    
    private void ProjectVectorOnPlane(ref Vector3 vectorToProject)
    {
        if (!playerIsGrounded || Input.JumpWasPressed) return;
        var localGroundCheckHitInfoNormal = transform.InverseTransformDirection(_groundCheckHitInfo.normal);
        var slopeAngleRotation = Quaternion.FromToRotation(StaticPlayerTransform.up, localGroundCheckHitInfoNormal);
        vectorToProject = slopeAngleRotation * vectorToProject;
        var bearForward = BearTransform.forward;
        bearForward.y = 0.0f;
        RelativeSlopeAngle = Vector3.Angle(localGroundCheckHitInfoNormal, bearForward) - 90.0f;
    }

    public void AnimationEvent()
    {
        CurrentState.CurrentSubState.OnAnimationEvent();
    }

    public void AnimationEndedEvent()
    {
        AnimationEnded = true;
    }
}