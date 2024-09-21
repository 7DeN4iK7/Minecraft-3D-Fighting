using UnityEngine;

public interface IState
{
    void OnEnter();
    void OnExit();
    void FixedUpdate();
    void Update();
}

public abstract class BaseSteveState : IState
{
    protected readonly Animator animator;
    protected float stateTimer;

    protected BaseSteveState(Animator animator)
    {
        this.animator = animator;
    }

    public float GetStateTimer()
    {
        return stateTimer;
    }
    
    public virtual void OnEnter()
    {
        stateTimer = 0;
    }

    public virtual void OnCollisionEnter()
    {

    }

    public virtual void OnExit()
    {
    }

    public virtual void FixedUpdate()
    {
        stateTimer += Time.fixedDeltaTime;
    }

    public virtual void Update()
    {
        
    }
}
