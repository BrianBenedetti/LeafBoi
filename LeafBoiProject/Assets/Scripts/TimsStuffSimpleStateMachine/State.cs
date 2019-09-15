using UnityEngine;

public abstract class State : IState
{
    public virtual void OnEnter(IState previous)
    {
        Debug.Log("State name entered");
    }

    public virtual void OnExit(IState exit)
    {
        Debug.Log("State name exit");
    }

    public virtual void OnUpdate()
    {
        Debug.Log("State name update");
    }
}
