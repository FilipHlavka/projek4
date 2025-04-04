using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private float scanCooldown = 1.5f;
    private float timer = 0;
    bool idle = false;
    float smallestDistance = float.MaxValue;
    Vector3 targetPoint;
    Vector3 checkPosition = new Vector3(0, 0, 0);

    Vector3 lastPosition;
    Quaternion lastRotation;

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

        foreach (var m in StationController.instance.stations)
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

        if(enemy.playerToAttack != null)
            MakeAttackMove();
        
      

    }

    private void MakeAttackMove()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.playerToAttack.transform.position) >= enemy.range / 2)
        {
            // Debug.Log(Vector3.Distance(enemy.transform.position, enemy.playerToAttack.transform.position) + "  " + enemy.range / 2);
            SendEnemyToPlayer();
        }
        // Debug.Log(Vector3.Distance(enemy.transform.position, enemy.playerToAttack.transform.position) + "  " + enemy.range / 3);

        if (Vector3.Distance(enemy.transform.position, enemy.playerToAttack.transform.position) <= enemy.range / 4)
        {
            //Debug.Log("volam se");
            SendEnemyFromPlayer();
        }


        if (Vector3.Distance(targetPoint, checkPosition) > 1 || IsNotMoving())
        {
            //enemy.transform.LookAt(enemy.playerToAttack.transform);
            Vector3 direction = enemy.playerToAttack.transform.position - enemy.transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);

        }
    }

    private bool IsNotMoving()
    {
        return Quaternion.Angle(lastRotation, agent.transform.rotation) < 1
            && Vector3.Distance(lastPosition, agent.transform.position) < 0.0000001;
    }

    private void SendEnemyFromPlayer()
    {

        checkPosition.x = agent.transform.position.x;
        checkPosition.z = agent.transform.position.z;


        Vector3 retreatDirection = (enemy.transform.position - enemy.playerToAttack.transform.position).normalized;

        
        float retreatDistance = enemy.range / 2f; 
        Vector3 retreatPosition = enemy.transform.position + retreatDirection * retreatDistance;
        //Debug.Log($"enemy position {enemy.transform.position}, retreat position {retreatPosition}");
        
        if (NavMesh.SamplePosition(retreatPosition, out NavMeshHit hit, 100, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            targetPoint = agent.destination;
            
        }


        targetPoint = agent.destination;
        lastRotation = agent.transform.rotation;
        lastPosition = agent.transform.position;

    }

    private void SendEnemyToPlayer()
    {

        checkPosition.x = agent.transform.position.x;
        checkPosition.z = agent.transform.position.z;


        Vector3 difference = (enemy.playerToAttack.transform.position - enemy.transform.position) / 1.5f;

        agent.SetDestination(enemy.playerToAttack.transform.position - difference);

        targetPoint = agent.destination;
        lastRotation = agent.transform.rotation;
        lastPosition = agent.transform.position;
        
    }

   
   

    public void ScanForUnits()
    {
        ChangeTarget();
        idle = true;
        
        foreach (var m in MovementController.instance.units)
        {
            if (Vector3.Distance(m.transform.position, enemy.transform.position) < 35)
            {
                idle = false; 
                break;
            }
        }
        foreach (var m in StationController.instance.stations)
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
