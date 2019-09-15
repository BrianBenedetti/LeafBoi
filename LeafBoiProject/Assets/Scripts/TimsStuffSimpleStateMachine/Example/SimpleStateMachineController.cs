using UnityEngine;

public class SimpleStateMachineController : MonoBehaviour
{
    private ExampleStateMachine stateMachine 
        = new ExampleStateMachine();

    protected NormalExample normal = new NormalExample();
    protected BlightExample blight = new BlightExample();


    private void Awake()
    {
        stateMachine.AddState(normal);
        stateMachine.AddState(blight);

        stateMachine.ChangeState(normal);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
