public interface IState
{
    void OnEnter(IState previous);
    void OnUpdate();
    void OnExit(IState exit);
}
