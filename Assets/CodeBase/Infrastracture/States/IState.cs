namespace CodeBase.Infrastracture.States
{
    public interface IState
    {
        void Enter(object param = null);
        void Exit(object param = null);
    }
}
