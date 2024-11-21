using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private float scanCooldown = 3f;
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

        if (Vector3.Distance(enemy.transform.position, enemy.playerToAttack.transform.position) <= enemy.range / 4)
        {

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

        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatPosition, out hit, 1.0f, NavMesh.AllAreas))
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
