using UnityEngine;
using UnityEngine.AI;

public class HelpState : State
{
    private float timer = 0;
    private float scanCooldown = 1.5f;
    bool attack = false;
    public override void InitState(NavMeshAgent agent, Enemy enemy)
    {
        base.InitState(agent, enemy);
        Debug.Log("jdu na pomoc, jsem: " + enemy.gameObject.name);


    }

    public override void GoTo(Vector3 where)
    {
       
        agent.SetDestination(where);
    }
    public override void DoStep()
    {
        timer += Time.deltaTime;
        if (timer >= scanCooldown)
        {
            timer = 0f;
            ScanForUnits();
        }
    }

    public void ScanForUnits()
    {
        // enemak bude utocit na nejblizsi jednotku, pokud na nej budou utocit unity s celkovim poctem zivotu veci nez 1 a pul zivotu enemy, enemy zacne zdrhat
        foreach (var m in MovementController.instance.units)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < enemy.range)
            {
                attack = true;
                break;
            }
        }
        foreach (var m in StationController.instance.stations)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < enemy.range)
            {
                attack = true;
                break;
            }
        }

    }


    public override State TryToChangeState()
    {

        if (attack)
            return new AttackState();

        return null;
    }
}
