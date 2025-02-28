public abstract class State
{
    public abstract void OnStartState(Core.Creature Creature);  // ���������� �� Creature �� Start/ChangeState
    public abstract void OnUpdate();                            // ���������� �� Creature �� Update
    public abstract void OnEndState();                          // ���������� �� Creature �� ChangeState
}

public interface IStateMachine
{
    public void ChangeState(State NewState);
}