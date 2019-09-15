using UnityEngine;
using System.Collections;

public class BlightExample : LabelledState<PlayerController.State>
{
    public BlightExample () : base(PlayerController.State.Blight)
    {
    }

    public override void OnEnter(IState previous)
    {
        base.OnEnter(previous);
    }

    public override void OnExit(IState exit)
    {
        base.OnExit(exit);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
