using UnityEngine;

public class AttackAndUseModule : MonoBehaviour
{
    private SteveInput _steveInput;
    private Animator _handAnimator;
    private static readonly int HandAnimation = Animator.StringToHash("UsePressed");
    private bool _inProcess;
    private bool _lookingUp, _lookingDown;

    [SerializeField] private LayerMask damagableMask;
    
    [SerializeField] private Transform forwardAttack;
    [SerializeField] private Transform downAttack;
    [SerializeField] private Transform upAttack;

    private Transform _currentAttackPos;
    private IDamagable _selected;
    private IDamagable _thisDamagable;

    private void Awake()
    {
        _steveInput = GetComponent<SteveInput>();
        _handAnimator = GetComponent<Animator>();
        _currentAttackPos = forwardAttack;
        _thisDamagable = GetComponentInParent<IDamagable>();
    }

    private void OnValidate()
    {
        _currentAttackPos = forwardAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(_currentAttackPos.position, new Vector3(0.96f, 0.96f, 0.96f));
    }
    
    private void Start()
    {
        _steveInput.UseButtonPressed += Use;
        _steveInput.UseButtonCanceled += CancelProcess;
        _steveInput.AttackButtonPressed += Use;
        _steveInput.DownPressed += LookDown;
        _steveInput.DownCanceled += LookDownCanceled;
        _steveInput.UpPressed += LookUp;
        _steveInput.UpCanceled += LookUpCanceled;
    }

    private void OnDestroy()
    {
        _steveInput.UseButtonPressed -= Use;
        _steveInput.UseButtonCanceled -= CancelProcess;
        _steveInput.AttackButtonPressed -= Use;
        _steveInput.DownPressed -= LookDown;
        _steveInput.DownCanceled -= LookDownCanceled;
        _steveInput.UpPressed -= LookUp;
        _steveInput.UpCanceled -= LookUpCanceled;
    }

    private void FixedUpdate()
    {
        var colliders = Physics.OverlapBox(_currentAttackPos.position, new Vector3(0.48f, 0.48f, 0.48f), Quaternion.identity, damagableMask);
        float minMagnidude = Mathf.Infinity;
        _selected = null;
        foreach (var collider in colliders)
        {
            IDamagable damagable = collider.GetComponentInParent<IDamagable>();
            
            if (damagable != null && damagable != _thisDamagable)
            {
                float magnitude = (transform.position - collider.transform.position).sqrMagnitude;
                if (magnitude < minMagnidude)
                {
                    _selected = damagable;
                    minMagnidude = magnitude;
                }
            }
        }
    }

    private void LookUp()
    {
        _lookingUp = true;
        CheckAttackPosition();
    }

    private void LookUpCanceled()
    {
        _lookingUp = false;
        CheckAttackPosition();
    }

    private void LookDown()
    {
        _lookingDown = true;
        CheckAttackPosition();
    }

    private void LookDownCanceled()
    {
        _lookingDown = false;
        CheckAttackPosition();
    }

    private void CheckAttackPosition()
    {
        if (_lookingUp)
            _currentAttackPos = upAttack;
        else if (_lookingDown)
            _currentAttackPos = downAttack;
        else
            _currentAttackPos = forwardAttack;
    }

    private void CancelProcess()
    {
        _inProcess = false;
        _handAnimator.SetBool(HandAnimation, false);
    }

    private void Use()
    {
        if (_inProcess) return;
        _inProcess = true;
        _handAnimator.SetBool(HandAnimation, true);
    }

    private void Attack()
    {
        _selected?.TakeDamageFrom(1, transform.position);
    }
    
    private void StartAttacking()
    {
        if (_inProcess) return;
        _inProcess = true;
        _handAnimator.SetBool(HandAnimation, true);
    }
}
