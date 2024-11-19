using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private float scanCooldown = 3f;
    private float timer = 0;
    bool idle = false;
    float smallestDistance = float.MaxValue;

    public override void InitState(NavMeshAgent agent, Enemy enemy)
    {
        base.InitState(agent, enemy);
        ChangeTarget();

        
    }

    public void ChangeTarget()
    {
        smallestDistance = float.MaxValue;
        Movement closestUnit = null;
        foreach (var m in MovementController.instance.units)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < 35)
            {
                if (Vector3.Distance(m.transform.position, enemy.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(m.transform.position, enemy.transform.position);
                    closestUnit = m;
                }

            }
        }
        enemy.playerToAttack = closestUnit;
        enemy.shouldAttackPlayer = true;
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
        ChangeTarget();
        idle = true;
        // enemak bude utocit na nejblizsi jednotku, pokud na nej budou utocit unity s celkovim poctem zivotu veci nez 1 a pul zivotu enemy, enemy zavola o pomoc
        foreach (var m in MovementController.instance.units)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < 35)
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
            return new IdleState();

        }

        return null;
    }


}
