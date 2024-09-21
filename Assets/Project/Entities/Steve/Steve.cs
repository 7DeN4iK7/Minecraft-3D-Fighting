using System;
using Project.StateMachine.SteveStates;
using UnityEngine;

[RequireComponent(typeof(SteveMovement), typeof(SteveInput), typeof(AttackAndUseModule))]
[RequireComponent(typeof(SteveHealthModule), typeof(Animator))]
public class Steve : MonoBehaviour
{
    private StateMachine _stateMachine;
    private SteveMovement _steveMovement;
    private SteveInput _steveInput;
    private Animator _animator;
    private AttackAndUseModule _attackAndUseModule;
    private SteveHealthModule _healthModule;

    private DeadState _deadState;
    private NeutralState _neutralState;
    private LavaDeathState _lavaDeathState;

    public bool IsDead => _healthModule.IsDead;

    private void Awake()
    {
        _steveInput = GetComponent<SteveInput>();
        _steveMovement = GetComponent<SteveMovement>();
        _animator = GetComponent<Animator>();
        _attackAndUseModule = GetComponent<AttackAndUseModule>();
        _healthModule = GetComponent<SteveHealthModule>();
        
        _stateMachine = new StateMachine();

        _neutralState = new NeutralState(_steveMovement);
        JumpState jumpState = new JumpState(_steveMovement);
        _deadState = new DeadState(_steveMovement);
        _lavaDeathState = new LavaDeathState(_steveMovement, _animator);

        _healthModule.Died += Die;

        At(_neutralState, jumpState, new FuncPredicate(() => _steveMovement.ReadyToJump()), false);
        At(jumpState, _neutralState, new FuncPredicate(() => _steveMovement.IsGrounded()), false);
        
        _stateMachine.SetState(_neutralState);
    }

    public Action Died;

    public void Die()
    {
        Died?.Invoke();
        Debug.Log(gameObject.name + " - Died");
        _stateMachine.ChangeState(_deadState);
    }

    private void DieInLava()
    {
        _stateMachine.ChangeState(_lavaDeathState);
    }

    public void Respawn()
    {
        _healthModule.Clear();
        _stateMachine.ChangeState(_neutralState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        DeathTrigger trigger = other.GetComponentInChildren<DeathTrigger>();

        if (trigger && !IsDead)
        {
            Debug.Log(gameObject.name + " dying in lava!");
            DieInLava();
        }
    }

    private void At(IState from, IState to, IPredicate condition, bool frameUpdate = true) => _stateMachine.AddTransition(from, to, condition, frameUpdate);
    private void Any(IState to, IPredicate condition, bool frameUpdate = true) => _stateMachine.AddAnyTransition(to, condition, frameUpdate);
}
