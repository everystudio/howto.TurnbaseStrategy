using UnityEngine;

public class StateMachineBase<T> : MonoBehaviour where T : StateMachineBase<T>
{
    private StateBase<T> currentState;
    [SerializeField] private string debugStateName;

    public void ChangeState(StateBase<T> nextState)
    {
        if (currentState != null)
        {
            currentState.OnExitState();
        }
        currentState = nextState;
        currentState.OnEnterState();
        debugStateName = currentState.ToString();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}