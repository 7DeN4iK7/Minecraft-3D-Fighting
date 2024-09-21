using UnityEngine;

[RequireComponent(typeof(SteveInput))]
[RequireComponent(typeof(Rigidbody))]
public class SteveMovement : MonoBehaviour
{
    [SerializeField] private Animator bodyAnimator;
    
    [SerializeField] private float normalSpeed;
    [SerializeField] private float crouchSpeed;

    [SerializeField] private float jumpGravity;
    [SerializeField] private float downfallGravity;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    
    private bool _grounded;
    private bool _hitted;
    
    private bool _facingRight = true;
    private float _currentSpeed;

    private float _horizontalVelocity;
    private float _verticalVelocity;
    
    private Rigidbody _body;
    private SteveInput _steveInput;
    
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Crouch = Animator.StringToHash("Crouch");
    private static readonly int UsePressed = Animator.StringToHash("UsePressed");

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundChecker.position + Vector3.down * 1f, new Vector3(0.4f, 0.02f, 0.1f));
    }

    public bool ReadyToJump() => IsGrounded() && _steveInput.JumpIsPressed;

    public bool IsGrounded()
    {
        _grounded = Physics.BoxCast(groundChecker.position, new Vector3(0.4f, 0.02f, 0.1f),
            Vector3.down, Quaternion.identity, 1.01f, groundMask, QueryTriggerInteraction.Ignore);
        return _grounded;
    }

    private void _reset()
    {
        _grounded = false;
        _hitted = false;
        _horizontalVelocity = 0;
        _verticalVelocity = 0;

        bodyAnimator.SetFloat(Moving, 0);
        bodyAnimator.SetFloat(Crouch, 0);
    }

    private void UpdateInfo()
    {
        UpdateGravity();
        if (_steveInput.MoveValue != 0)
            OnMoveStarted(_steveInput.MoveValue);

        OnMove(_steveInput.MoveValue);

        if (_steveInput.DownIsPressed)
            OnDownPressed();
    }
    
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _steveInput = GetComponent<SteveInput>();
        _currentSpeed = normalSpeed;
    }

    public void GetHitFrom(bool fromForward, int effectMultiplyer)
    {
        _horizontalVelocity = effectMultiplyer * (fromForward ? -3 : 3);
        _verticalVelocity = jumpForce * effectMultiplyer;
        _body.velocity = new Vector3(0, _verticalVelocity, _horizontalVelocity);
     
        
        _hitted = true;
    }

    public void EndHit()
    {
        _hitted = false;
        _horizontalVelocity = _steveInput.MoveValue * _currentSpeed;
    }
    
    private void OnEnable()
    {
        UpdateInfo();


        _steveInput.MoveStarted += OnMoveStarted;
        _steveInput.MoveCanceled += OnMoveCanceled;
        _steveInput.MovePerformed += OnMove;
        
        _steveInput.UseButtonPressed += UseButtonPressed;
        _steveInput.UseButtonCanceled += UseButtonCanceled;
        
        _steveInput.DownPressed += OnDownPressed;
        _steveInput.DownCanceled += OnDownCanceled;
        
        _steveInput.UpPressed += OnUpPressed;
        _steveInput.UpCanceled += OnUpCanceled;
    }

    private void OnDisable()
    {
        _reset();
        _body.velocity = Vector3.zero;

        _steveInput.MoveStarted -= OnMoveStarted;
        _steveInput.MoveCanceled -= OnMoveCanceled;
        _steveInput.MovePerformed -= OnMove;
        
        _steveInput.UseButtonPressed -= UseButtonPressed;
        _steveInput.UseButtonCanceled -= UseButtonCanceled;
        
        _steveInput.DownPressed -= OnDownPressed;
        _steveInput.DownCanceled -= OnDownCanceled;
        
        _steveInput.UpPressed -= OnUpPressed;
        _steveInput.UpCanceled -= OnUpCanceled;
    }

    public void Jump()
    {
        _verticalVelocity = jumpForce;
    }

    public void HandleJumping()
    {
        if (_steveInput.JumpIsPressed && _verticalVelocity > 0)
        {
            _verticalVelocity -= jumpGravity;
        }
        else
        {
            _verticalVelocity -= downfallGravity;
        }
    }

    public void UpdateGravity()
    {
        if (IsGrounded() && !_hitted)
            _verticalVelocity = -0.01f;
        else
            _verticalVelocity -= downfallGravity;
    }

    public void ApplyVelocity()
    {
        _body.velocity = new Vector3(0, _verticalVelocity, _horizontalVelocity);
    }

    private void UseButtonPressed()
    {
        //bodyAnimator.SetBool(UsePressed, true);
    }

    private void UseButtonCanceled()
    {
        //bodyAnimator.SetBool(UsePressed, false);
    }

    private void OnDownPressed()
    {
        /*if (!_upPressed)
        {
            _currentAttack = downAttack;
            headAim.position = _currentAttack.position;
        }*/

        bodyAnimator.SetFloat(Crouch, 1);
        _currentSpeed = crouchSpeed;
        OnMove(_steveInput.MoveValue);
    }

    private void OnDownCanceled()
    {
        /*if (!_upPressed)
        {
            _currentAttack = forwardAttack;
            headAim.position = _currentAttack.position;
        }*/

        bodyAnimator.SetFloat(Crouch, 0);
        _currentSpeed = normalSpeed;
        OnMove(_steveInput.MoveValue);
    }

    private void OnMove(float horizontalInput)
    {
        if (!_hitted)
            _horizontalVelocity = _currentSpeed * _steveInput.MoveValue;
        if (_facingRight && horizontalInput < 0 || !_facingRight && horizontalInput > 0)
        {
            _facingRight = !_facingRight;
            transform.Rotate(0, 180, 0);
        }
    }


    private void OnMoveCanceled(float horizontalInput)
    {
        _horizontalVelocity = 0;
        bodyAnimator.SetFloat(Moving, 0);
    }

    private void OnMoveStarted(float horizontalInput)
    {
        bodyAnimator.SetFloat(Moving, 1);
    }

    private void OnUpPressed()
    {
        //_currentAttack = upAttack;
        //headAim.position = _currentAttack.position;
    }

    private void OnUpCanceled()
    {
        /*if (_downPressed)
        {
            //_currentAttack = downAttack;
            //headAim.position = _currentAttack.position;
        }
        else
        {
            //_currentAttack = forwardAttack;
            //headAim.position = _currentAttack.position;
        }*/
    }
}