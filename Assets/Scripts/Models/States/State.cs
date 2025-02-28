public abstract class State
{
    public abstract void OnStartState(Core.Creature Creature);  // Вызывается из Creature из Start/ChangeState
    public abstract void OnUpdate();                            // Вызывается из Creature из Update
    public abstract void OnEndState();                          // Вызывается из Creature из ChangeState
}

public interface IStateMachine
{
    public void ChangeState(State NewState);
}