using System.Collections.Generic;

public class StateMachine
{
    StateNode current;
    Dictionary<IState, StateNode> nodes = new();
    HashSet<ITransition> anyTransitionsFrameUpdate = new();
    HashSet<ITransition> anyTransitionsFixedUpdate = new();
 
    public void Update()
    {
        var transition = GetFrameUpdateTransition();
        if (transition != null)
            ChangeState(transition.To);
        
        current.State?.Update();
    }

    public void FixedUpdate()
    {
        var transition = GetFixedUpdateTransition();
        if (transition != null)
            ChangeState(transition.To);
        
        current.State?.FixedUpdate();
    }

    public void SetState(IState state)
    {
        current = GetOrAddNode(state);
        current.State?.OnEnter();
    }

    public void ChangeState(IState state)
    {
        if (state == current.State) return;

        var previousState = current.State;
        var nextState = GetOrAddNode(state).State;
        
        previousState?.OnExit();
        nextState?.OnEnter();
        current = nodes[state];
    }

    ITransition GetFrameUpdateTransition()
    {
        foreach (var transition in anyTransitionsFrameUpdate)
        {
            if (transition.Condition.Evaluate())
                return transition;
        }

        foreach (var transition in current.FrameUpdateTransitions)
        {
            if (transition.Condition.Evaluate())
                return transition;
        }
        
        return null;
    }

    ITransition GetFixedUpdateTransition()
    {
        foreach (var transition in anyTransitionsFixedUpdate)
        {
            if (transition.Condition.Evaluate())
                return transition;
        }

        foreach (var transition in current.FixedUpdateTransitions)
        {
            if (transition.Condition.Evaluate())
                return transition;
        }
        
        return null;
    }

    public void AddTransition(IState from, IState to, IPredicate condition, bool frameUpdate = true)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition, frameUpdate);
    }

    public void AddAnyTransition(IState to, IPredicate condition, bool frameUpdate = true)
    {
        if (frameUpdate)
            anyTransitionsFrameUpdate.Add(new Transition(to, condition));
        else
            anyTransitionsFixedUpdate.Add(new Transition(to, condition));
    }

    StateNode GetOrAddNode(IState state)
    {
        var node = nodes.GetValueOrDefault(state);

        if (node == null)
        {
            node = new StateNode(state);
            nodes.Add(state, node);
        }

        return node;
    }

    class StateNode
    {
        public IState State { get; }
        public HashSet<ITransition> FrameUpdateTransitions { get; }
        public HashSet<ITransition> FixedUpdateTransitions { get; }

        public StateNode(IState state)
        {
            State = state;
            FrameUpdateTransitions = new HashSet<ITransition>();
            FixedUpdateTransitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState to, IPredicate condition, bool frameUpdate = true)
        {
            if (frameUpdate)
                FrameUpdateTransitions.Add(new Transition(to, condition));
            else
                FixedUpdateTransitions.Add(new Transition(to, condition));
        }
    }
}