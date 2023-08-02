using UnityEngine;

public class StateBase<T> where T : StateMachineBase<T>
{
    protected T machine;
    public StateBase(T machine) { this.machine = machine; }
    public virtual void OnEnterState() { }
    public virtual void OnUpdate() { }
    public virtual void OnExitState() { }
    public void ChangeState(StateBase<T> nextState)
    {
        machine.ChangeState(nextState);
    }
}