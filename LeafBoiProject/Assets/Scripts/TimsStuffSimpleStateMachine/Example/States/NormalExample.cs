using UnityEngine;
using System.Collections;

public class NormalExample : LabelledState<PlayerController.State>
{
    public NormalExample() : base(PlayerController.State.Normal)
    {
    }

    public override void OnEnter(IState previous)
    {
        // PlayerController.instance.SetLocomationAnimation();
    }

    public override void OnExit(IState exit)
    {
    }

    public override void OnUpdate()
    {
        // update stuffies

        //if ( JumpPressed() )
        {
            //ChangeToJumpState()
            //PlayerController.instance.ChangeToState(StateName);
        }
    }
}
