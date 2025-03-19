using UnityEngine;
using UnityEngine.AI;

public class StationAttackState : State
{
    bool idle = false;
    float smallestDistance = float.MaxValue;
    private float scanCooldown = 1.5f;
    private float timer = 0;

    public override void InitState(NavMeshAgent agent, Enemy enemy)
    {
        base.InitState(agent, enemy);
        ChangeTarget();


    }

    public override void DoStep()
    {
        timer += Time.deltaTime;
        if (timer >= scanCooldown)
        {
            timer = 0f;
            ScanForUnits();
            if(enemy.playerToAttack != null && enemy != null)
            enemy.CallForHelp(enemy.playerToAttack.transform.position);
        }
    }


    public void ChangeTarget()
    {
        smallestDistance = float.MaxValue;
        Movement closestUnit = null;
        foreach (var m in MovementController.instance.units)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < enemy.range)
            {
                if (Vector3.Distance(m.transform.position, enemy.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(m.transform.position, enemy.transform.position);
                    closestUnit = m;
                }

            }
        }

        foreach (var m in StationController.instance.stations)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < enemy.range)
            {
                if (Vector3.Distance(m.transform.position, enemy.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(m.transform.position, enemy.transform.position);
                    closestUnit = m;
                }

            }
        }

       // Debug.Log("nooo: " + closestUnit);
        enemy.playerToAttack = closestUnit;
        enemy.shouldAttackPlayer = true;
    }

    public void ScanForUnits()
    {
        ChangeTarget();
        idle = true;
       
        foreach (var m in MovementController.instance.units)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < enemy.range)
            {
                idle = false;
                break;
            }
        }
        foreach (var m in StationController.instance.stations)
        {

            if (Vector3.Distance(m.transform.position, enemy.transform.position) < enemy.range)
            {
                idle = false;
                break;
            }
        }

    }

    public override State TryToChangeState()
    {
        if (idle)
        {
            enemy.shouldAttackPlayer = false;
            enemy.noHelpNeeded();
            return new StationIdleState();

        }

        return null;
    }
}
