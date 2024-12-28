using UnityEngine;
using UnityEngine.AI;

public abstract class State 
{
    protected NavMeshAgent agent;
    protected Enemy enemy;
    Vector3 whereToGo;

    public virtual void InitState(NavMeshAgent agent,Enemy enemy)
    {
        if (agent != null)
        this.agent = agent;


        this.enemy = enemy;
    }

    public virtual void GoTo(Vector3 where)
    {
        whereToGo = where;
    }

    public abstract State TryToChangeState();

    public abstract void DoStep();
}
