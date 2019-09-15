using System.Collections.Generic;

public abstract class StateMachine<T> where T : IState
{
    protected List<T> states;

    public T Current
    {
        get;
        protected set;
    }

    public virtual void AddState(T state)
    {
        states.Add(state);
    }

    public virtual void RemoveState(T state)
    {
        states.Remove(state);
    }

    public virtual void ChangeState(T state)
    {
        
        if ( !states.Contains(state) )
        {
            return;
        }
        if (Current != null)
        {
            Current.OnExit(state);
            state.OnEnter(Current);
        }
        Current = state;
    }

    public virtual void Update ()
    {
        if ( Current == null )
        {
            return;
        }
        Current.OnUpdate();
    }

}
