using UnityEngine;
using UnityEngine.AI;

public class StationIdleState : State
{
    bool attack = false;

    public override void DoStep()
    {
       ScanForUnits();
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
        {
            Debug.Log("Station AttackState");

            return new StationAttackState();
        }

        return null;
    }
}
