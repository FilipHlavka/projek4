using UnityEngine;
using UnityEngine.AI;

public abstract class State 
{
    protected NavMeshAgent agent;
    protected Enemy enemy;

    public virtual void InitState(NavMeshAgent agent,Enemy enemy)
    {
        this.agent = agent;
        this.enemy = enemy;
    }

    public abstract State TryToChangeState();

    public abstract void DoStep();
}
