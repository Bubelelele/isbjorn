using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private Animator animator; 

    [Header("Movement")]
    [SerializeField] private float movementMultiplier = 30.0f;
    [SerializeField] private float airMovementMultiplier = 1.25f;
    [SerializeField] private float runMovementMultiplier = 2.0f;
    [SerializeField] private float rollMovementMultiplier = 0.5f;
    [SerializeField] private bool walkingDownSlope;
    
    private Rigidbody _rigidbody;
    private Transform _mainCameraTransform;
    private Transform _playerTransform;
    private Vector3 _playerPosition;
    private Vector3 _playerMovement;
    private bool _animationPlaying;
    private float _animationPlayingTimer;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool playerIsGrounded = true;
    [SerializeField] [Range(0.0f, 1.8f)] private float groundCheckRadiusMultiplier = 0.9f;
    [SerializeField] [Range(-0.95f, 1.05f)] private float groundCheckDistance = 0.05f;
    
    private RaycastHit _groundCheckHit;

    [Header("Gravity")]
    [SerializeField] private float currentFallGravity;
    [SerializeField] private float minimumFallGravity = -100.0f;
    [SerializeField] private float maximumFallGravity = -500.0f;
    [SerializeField] [Range(-100.0f, -5.0f)] private float incrementAmount = -20.0f;
    [SerializeField] private float incrementFrequency = 0.05f;
    [SerializeField] private float playerFallTimer;
    [SerializeField] private float groundedGravity;
    [SerializeField] private float maxSlopeAngle = 47.5f;

    private CapsuleCollider _capsuleCollider;

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
    [SerializeField] private bool jumpWasPressedLastFrame;

    [Header("Rolling")]
    [SerializeField] private bool playerGoingUpRamp;
    [SerializeField] private float playerRollTimer;
    [SerializeField] private float playerRollGravityIncrementFrequency = 1.0f;
    [SerializeField] private float playerRollGravityIncrementAmount = -50.0f;
    public float shootForce = 75000f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (Camera.main != null) _mainCameraTransform = Camera.main.transform;
        _playerTransform = _rigidbody.transform;

        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        PlayerLook();
        _playerMovement = GetMoveInput();
        playerIsGrounded = PlayerGroundCheck();
        _playerMovement = CalculatePlayerMovement();
        _playerMovement = HandleSlopes();
        _playerMovement = PlayerRun();
        _playerMovement.y = PlayerFallGravity();
        _playerMovement.y = PlayerJump();
        
        // Keeps movement independent of mass during development. (?)
        _playerMovement *= _rigidbody.mass;

        _rigidbody.AddRelativeForce(_playerMovement, ForceMode.Force);
    }

    private void Update()
    {
        Debug.Log(playerGoingUpRamp);
        
        PlayerRolling();
        PlayerSlash();
        PlayerRoar();
        PlayerSmell();
    }

    private Vector3 GetLookDirection()
    {
        _playerPosition = _rigidbody.position;
        var mainCameraPosition = _mainCameraTransform.position;

        var lookDirection = _playerPosition - new Vector3(mainCameraPosition.x, _playerPosition.y, mainCameraPosition.z);

        return lookDirection.normalized;
    }

    private void PlayerLook()
    {
        if (_playerMovement != Vector3.zero) _playerTransform.forward = GetLookDirection();
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(input.MoveInput.x, 0.0f, input.MoveInput.y);
    }
    
    // Temp function
    public void ShootUpRamp()
    {
        _rigidbody.AddForce(_playerTransform.forward * shootForce, ForceMode.Impulse);
    }

    // Need to implement a state machine.
    private Vector3 CalculatePlayerMovement()
    {
        animator.SetBool("IsWalking", input.MoveIsPressed);

        //if (!input.MoveIsPressed || !input.RollIsPressed) momentumMode = false;
            
        var calculatedPlayerMovement = _playerMovement * movementMultiplier;

        if (!playerIsGrounded && !input.RollIsPressed && !input.RunIsPressed)
        {
            _playerMovement = calculatedPlayerMovement * airMovementMultiplier;
        }
        else if (playerIsGrounded && input.RollIsPressed && !input.RunIsPressed)
        {
            _playerMovement = calculatedPlayerMovement * rollMovementMultiplier;
        }
        else
        {
            _playerMovement = calculatedPlayerMovement;
        }
            
        //if (!momentumMode) return _playerMovement;

        return _playerMovement; //* momentumModeMultiplier;
    }

    private Vector3 PlayerRun()
    {
        animator.SetBool("IsRunning", input.RunIsPressed);
            
        var playerRunMovement = _playerMovement;

        if (!input.MoveIsPressed || !input.RunIsPressed) return playerRunMovement;
            
        playerRunMovement *= runMovementMultiplier;

        return playerRunMovement;
    }
    // Haven't gone in depth on how it works yet.
    private Vector3 HandleSlopes()
    {
        var calculatedPlayerMovement = _playerMovement;
        var playerUp = _playerTransform.up;
        if (playerIsGrounded)
        {
            var localGroundCheckHitNormal = _playerTransform.InverseTransformDirection(_groundCheckHit.normal);
            var groundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, playerUp);
                
            if (groundSlopeAngle == 0.0f)
            {
                if (input.MoveIsPressed)
                {
                    float rayHeightFromGround = 0.1f;
                    float rayCalculatedRayHeight = _playerPosition.y - _capsuleCollider.bounds.extents.y + rayHeightFromGround;
                    Vector3 rayOrigin = new Vector3(_playerPosition.x, rayCalculatedRayHeight, _playerPosition.z);
                    if (Physics.Raycast(rayOrigin, _playerTransform.TransformDirection(calculatedPlayerMovement), out var raycastHit, 0.75f))
                    {
                        if (Vector3.Angle(raycastHit.normal, playerUp) > maxSlopeAngle)
                        {
                            calculatedPlayerMovement.y = -movementMultiplier;
                        }
                    }
                    
                }

                if (calculatedPlayerMovement.y == 0.0f)
                {
                    calculatedPlayerMovement.y = groundedGravity;
                }
            }
            else
            {
                var slopeAngleRotation = Quaternion.FromToRotation(playerUp, localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRotation * calculatedPlayerMovement;
                    
                var relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, playerUp) - 90.0f;

                playerGoingUpRamp = !(relativeSlopeAngle > 0);

                calculatedPlayerMovement += calculatedPlayerMovement * (relativeSlopeAngle / 90.0f);
                    
                if (groundSlopeAngle < maxSlopeAngle)
                {
                    if (input.MoveIsPressed)
                    {
                        calculatedPlayerMovement.y += groundedGravity;
                    }
                }
                else
                {
                    var calculatedSlopeGravity = groundSlopeAngle * -0.2f;
                    if (calculatedSlopeGravity < calculatedPlayerMovement.y)
                    {
                        calculatedPlayerMovement.y = calculatedSlopeGravity;
                    }
                }
            }
        }
        return calculatedPlayerMovement;
    }
    private bool PlayerGroundCheck()
    {
        var sphereCastRadius = _capsuleCollider.radius * groundCheckRadiusMultiplier;
        var sphereCastTravelDistance = _capsuleCollider.bounds.extents.y - sphereCastRadius + groundCheckDistance;
        
            
        return Physics.SphereCast(_playerPosition, sphereCastRadius, Vector3.down, out _groundCheckHit, sphereCastTravelDistance, groundLayerMask);
    }
    // Since it was modified a bit to accommodate slopes, I'm unsure how it ties with HandleSlopes().
    private float PlayerFallGravity()
    {
        var gravity = _playerMovement.y;
            
        // Rolling gravity.
        if (playerIsGrounded && input.RollIsPressed)
        {
            _rigidbody.drag = 0.0f;
            playerRollTimer -= Time.fixedDeltaTime;
            if (playerRollTimer < 0.0f)
            {
                if (currentFallGravity > maximumFallGravity)
                {
                    currentFallGravity += playerRollGravityIncrementAmount;
                }
                playerRollTimer = playerRollGravityIncrementFrequency;
            }
            gravity = currentFallGravity;
        }
        // Grounded gravity.
        else if (playerIsGrounded)
        {
            _rigidbody.drag = 20.0f;
            currentFallGravity = minimumFallGravity;
        }
        // Basically falling gravity.
        else
        {
            _rigidbody.drag = 20.0f;
            playerFallTimer -= Time.fixedDeltaTime;
            if (playerFallTimer < 0.0f)
            {
                if (currentFallGravity > maximumFallGravity)
                {
                    currentFallGravity += incrementAmount;
                }
                playerFallTimer = incrementFrequency;
            }
            gravity = currentFallGravity;
        }
        return gravity;
    }

    // Skipped explanation in the tutorial on how this works in detail.
    private float PlayerJump()
    {
        animator.SetBool("IsJumping", playerIsJumping);
        animator.SetBool("IsFalling", !playerIsGrounded);
            
        var calculatedJumpInput = _playerMovement.y;

        SetJumpCounter();
        SetCoyoteTimeCounter();
        SetJumpBufferTimeCounter();

        if (jumpBufferTimeCounter > 0.0f && !playerIsJumping && coyoteTimeCounter > 0.0f)
        {
            // Can cache this statement since it's also in HandleSlopes().
            if (Vector3.Angle(_playerTransform.up, _groundCheckHit.normal) < maxSlopeAngle)
            {
                calculatedJumpInput = initialJumpForce;
                // if (_groundCheckHit.collider.CompareTag("Walrus")) calculatedJumpInput = initialJumpForce * 10.0f;
                playerIsJumping = true;
                jumpBufferTimeCounter = 0.0f;
                coyoteTimeCounter = 0.0f;
            }
        }
        else if (input.JumpIsPressed && playerIsJumping && !playerIsGrounded && jumpTimeCounter > 0.0f)
        {
            calculatedJumpInput = initialJumpForce * continualJumpForceMultiplier;
        }
        else if (playerIsJumping && playerIsGrounded)
        {
            playerIsJumping = false;
        }

        return calculatedJumpInput;
    }
    private void SetJumpCounter()
    {
        if (playerIsJumping && !playerIsGrounded)
        {
            jumpTimeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            jumpTimeCounter = jumpTime;
        }
    }
    private void SetCoyoteTimeCounter()
    {
        if (playerIsGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }
    private void SetJumpBufferTimeCounter()
    {
        if (!jumpWasPressedLastFrame && input.JumpIsPressed)
        {
            jumpBufferTimeCounter = jumpBufferTime;
        }
        else if (jumpBufferTimeCounter > 0.0f)
        {
            jumpTimeCounter -= Time.fixedDeltaTime;
        }
            
        jumpWasPressedLastFrame = input.JumpIsPressed;
    }
    private void PlayerRoar()
    {
        PlayRoarAnimation();
    }
    private void PlayerSlash()
    {
        // Logic that ties with enemies goes here.

        PlaySlashAnimation();
    }
    private void PlayerSmell()
    {
        // Logic Kevin created goes here.
            
        PlaySmellAnimation();
    }
    private void PlayRoarAnimation()
    {
        if (input.Roaring && !_animationPlaying)
        {
            animator.SetTrigger("Roar");
            _animationPlayingTimer = animator.GetCurrentAnimatorStateInfo(0).length;
            GetComponent<AudioSource>().Play();
        }
            
        _animationPlayingTimer -= Time.deltaTime;
        _animationPlaying = true;
        if (_animationPlayingTimer < 0) _animationPlaying = false;
    }
    private void PlaySlashAnimation()
    {
        if (input.Slashing && !_animationPlaying)
        {
            animator.SetTrigger("Slash");
            _animationPlayingTimer = animator.GetCurrentAnimatorStateInfo(0).length;
        }
            
        _animationPlayingTimer -= Time.deltaTime;
        _animationPlaying = true;
        if (_animationPlayingTimer < 0) _animationPlaying = false;
    }
    private void PlaySmellAnimation()
    {
        if (input.Smelling && !_animationPlaying)
        {
            animator.SetTrigger("Sniff");
            _animationPlayingTimer = animator.GetCurrentAnimatorStateInfo(0).length;
        }
            
        _animationPlayingTimer -= Time.deltaTime;
        _animationPlaying = true;
        if (_animationPlayingTimer < 0) _animationPlaying = false;
    }
    private void PlayerRolling()
    {
        animator.SetBool("IsRolling", input.RollIsPressed);
    }
}