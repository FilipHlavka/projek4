using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    private NavMeshAgent agent;
   

    private State currentState;

    public State CurrentState
    {
        get { return currentState; }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        ChangeState(new IdleState());
    }
    
    private void ChangeState(State newState)
    {
        currentState = newState;
        currentState.InitState(agent);
        Debug.Log("Changed State -> " + newState.GetType());
    }

    private void Update()
    {
        CurrentState.DoStep();

        var newState = CurrentState.TryToChangeState();
        if (newState != null)
        {
            ChangeState(newState);
        }
    }

}
