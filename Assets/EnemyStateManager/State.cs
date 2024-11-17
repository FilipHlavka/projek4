using UnityEngine;
using UnityEngine.AI;

public abstract class State 
{
    protected NavMeshAgent agent;
   

    public virtual void InitState(NavMeshAgent agent)
    {
        this.agent = agent;
        
    }

    public abstract State TryToChangeState();

    public abstract void DoStep();
}
