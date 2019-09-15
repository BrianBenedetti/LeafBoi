using System.Collections.Generic;

using SaatwikKey = PlayerController.State;
using SaatwikState = LabelledState<PlayerController.State>;

public class ExampleStateMachine : StateMachine<SaatwikState>
{
    public Dictionary<SaatwikKey, SaatwikState> stateDicitionary;

    public SaatwikKey CurrentKey
    {
        get;
        protected set;
    }

    public virtual void AddState(SaatwikKey key, SaatwikState state)
    {
        states.Add(state);
        stateDicitionary.Add(key, state);
    }

    public void ChangeKeyState (SaatwikKey key)
    {
        if ( !stateDicitionary.ContainsKey(key) )
        {
            return;
        }

        SaatwikState newState = stateDicitionary[key];

        base.ChangeState(newState);

    }
}
