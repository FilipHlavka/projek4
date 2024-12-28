using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

public class IdleState : State
{
    Vector3 targetPoint;
    Vector3 checkPosition = new Vector3(0, 0, 0);

    Vector3 lastPosition;
    Quaternion lastRotation;
    private float scanCooldown = 1.5f;
    private float timer = 0;
    float generateCooldown = 0;
    bool attack = false;

    public override void InitState(NavMeshAgent agent, Enemy enemy)
    {
        base.InitState(agent,enemy);
       
        SendEnemyToNextPoint();
    }

    private Vector3 GenerateRandomPoint()
    {
        // dát rozmìry terénu
        
        return new Vector3(Random.Range(0, MovementController.instance.tr.terrainData.size.x), 0, Random.Range(0, MovementController.instance.tr.terrainData.size.z));
    }

    public override void DoStep()
    {

        timer += Time.deltaTime;
        if (timer >= scanCooldown)
        {
            timer = 0f;
            ScanForUnits();
        }

        checkPosition.x = agent.transform.position.x;
        checkPosition.z = agent.transform.position.z;

        if (Vector3.Distance(targetPoint, checkPosition) < 1 || IsNotMoving())
        {
            SendEnemyToNextPoint();
        }

        lastRotation = agent.transform.rotation;
        lastPosition = agent.transform.position;
    }

    public void ScanForUnits()
    {
        // enemak bude utocit na nejblizsi jednotku, pokud na nej budou utocit unity s celkovim poctem zivotu veci nez 1 a pul zivotu enemy, enemy zacne zdrhat
        foreach(var m in MovementController.instance.units)
        {
            if(Vector3.Distance(m.transform.position,enemy.transform.position) < enemy.range)
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

    

    private void SendEnemyToNextPoint()
    {
        if (generateCooldown <= 0)
        {
            targetPoint = GenerateRandomPoint();
           // Debug.Log(targetPoint);
            agent.SetDestination(targetPoint);
            generateCooldown = 1;
        }
        else
        {
            generateCooldown -= Time.deltaTime;
        }
    }

    private bool IsNotMoving()
    {
        return Quaternion.Angle(lastRotation, agent.transform.rotation) < 1
            && Vector3.Distance(lastPosition, agent.transform.position) < 0.0000001;
    }

    public override State TryToChangeState()
    {
       
        if(attack)
           return new AttackState();

        return null;
    }

   
}
