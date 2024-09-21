using System;
using UnityEngine;

[RequireComponent(typeof(Steve))]
public class SteveHealthModule : MonoBehaviour, IDamagable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Animator fxAnimator;

    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int DiedHash = Animator.StringToHash("Died");

    private SteveMovement _steveMovement;

    private bool _isHurt;

    public bool IsDead { get; private set; }
    [field: SerializeField] public int maxHealth { get; set; }
    public int currentHealth { get; set; }

    private void Awake()
    {
        _steveMovement = GetComponent<SteveMovement>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        currentHealth -= damage;
        healthBar.SetHealth(Mathf.Clamp(currentHealth / (float)maxHealth, 0, 1));
        if (currentHealth <= 0)
        {
            Died?.Invoke();
            IsDead = true;
        }
    }

    public void TakeDamageFrom(int damage, Vector3 fromWorldPoint, int effectMultiplyer = 1)
    {
        if (_isHurt || IsDead) return;

        currentHealth -= damage;
        healthBar.SetHealth(Mathf.Clamp(currentHealth / (float) maxHealth, 0, 1));
        if (currentHealth <= 0)
        {
            fxAnimator.SetBool(DiedHash, true);
            Died?.Invoke();
            IsDead = true;
        }
        else
        {
            _steveMovement.GetHitFrom((transform.position.z - fromWorldPoint.z) < 0, effectMultiplyer);
            fxAnimator.SetBool(HurtHash, true);
            _isHurt = true;
        }
    }

    public void Die()
    {
        currentHealth = 0;
        healthBar.SetHealth(0);

        Died?.Invoke();
        IsDead = true;
    }
    
    public void EndHurting()
    {
        fxAnimator.SetBool(HurtHash, false);
        _steveMovement.EndHit();
        _isHurt = false;
    }

    public void Clear()
    {
        IsDead = false;
        fxAnimator.SetBool(DiedHash, false);
        currentHealth = 0;
        Heal(maxHealth);
    }

    public void Heal(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        healthBar.Reset();
        healthBar.SetHealth(currentHealth / (float) maxHealth);
    }

    public Action Died { get; set; }
}