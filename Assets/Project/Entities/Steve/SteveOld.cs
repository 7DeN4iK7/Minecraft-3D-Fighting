using System;
using UnityEngine;

public class SteveOld : MonoBehaviour, IDamagable
{
    [SerializeField] private Animator fxAnimator;
    
    [SerializeField] private LayerMask playerMask;
    
    [SerializeField] private Transform forwardAttack;
    [SerializeField] private Transform downAttack;
    [SerializeField] private Transform upAttack;
    
    [SerializeField] private Transform headAim;

    [SerializeField] private SteveMovement steveMovement;

    [SerializeField] private HealthBar healthBar; 

    private Transform _currentAttack;
    private SteveInput _steveInput;
    private IDamagable _selected;

    private bool _isHurt;
    private bool _upPressed;
    private bool _downPressed;
    
    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int DiedHash = Animator.StringToHash("Died");

    public Action Died { get; set; }

    [field: SerializeField] public int maxHealth { get; set; }
    public int currentHealth { get; set; }

    public void Clear()
    {
        currentHealth = 0;
        Heal(maxHealth);
    }
    
    public void Heal(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        healthBar.Reset();
        healthBar.SetHealth(currentHealth / (float)maxHealth);
    }

    public void Diaed()
    {
        Died?.Invoke();
        fxAnimator.SetBool(DiedHash, false);
    }
    
    public void TakeDamageFrom(int damage, Vector3 fromWorldPoint, int effectMultiplyer)
    {
        if (_isHurt) return;
        
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth / (float)maxHealth);
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " Died");
            fxAnimator.SetBool(DiedHash, true);
        }
        else
        {
            steveMovement.GetHitFrom((transform.position.z - fromWorldPoint.z) < 0, effectMultiplyer);
            fxAnimator.SetBool(HurtHash, true);
            _isHurt = true;
        }
    }

    public void EndHurting()
    {
        fxAnimator.SetBool(HurtHash, false);
        steveMovement.EndHit();
        _isHurt = false;
    }

    private void Awake()
    {
        _currentAttack = forwardAttack;
        currentHealth = maxHealth;
        _steveInput = GetComponent<SteveInput>();
    }

    private void Start()
    {
        _steveInput.UpPressed += OnUpPressed;
        _steveInput.UpCanceled += OnUpCanceled;

        _steveInput.DownPressed += OnDownPressed;
        _steveInput.DownCanceled += OnDownCanceled;
    }

    private void OnDestroy()
    {
        _steveInput.UpPressed -= OnUpPressed;
        _steveInput.UpCanceled -= OnUpCanceled;
        
        _steveInput.DownPressed -= OnDownPressed;
        _steveInput.DownCanceled -= OnDownCanceled;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(_currentAttack.position, new Vector3(0.98f, 0.98f, 0.98f));
    }

    private void OnValidate()
    {
        _currentAttack = forwardAttack;
        headAim.position = _currentAttack.position;
    }

    public void Attack()
    {
        _selected?.TakeDamageFrom(1, transform.position);
    }

    private void FixedUpdate()
    {
        var colliders = Physics.OverlapBox(_currentAttack.position, new Vector3(0.49f, 0.49f, 0.49f), Quaternion.identity, playerMask);
        float minMagnidude = Mathf.Infinity;
        _selected = null;
        foreach (var collider in colliders)
        {
            IDamagable damagable = collider.GetComponentInParent<IDamagable>();
            
            if (damagable != null && damagable != this)
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

    private void OnDownCanceled()
    {
        _downPressed = false;
        if (!_upPressed)
        {
            _currentAttack = forwardAttack;
            headAim.position = _currentAttack.position;
        }
    }

    private void OnDownPressed()
    {
        _downPressed = true;
        if (!_upPressed)
        {
            _currentAttack = downAttack;
            //headAim.position = _currentAttack.position;
        }
    }
    
    private void OnUpPressed()
    {
        _upPressed = true;
        _currentAttack = upAttack;
        headAim.position = _currentAttack.position;
    }

    private void OnUpCanceled()
    {
        _upPressed = false;
        if (_downPressed)
        {
            _currentAttack = downAttack;
            //headAim.position = _currentAttack.position;
        }
        else
        {
            _currentAttack = forwardAttack;
            headAim.position = _currentAttack.position;
        }
    }
}
