using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    Vector3 targetPoint;
    Vector3 checkPosition = new Vector3(0, 0, 0);

    Vector3 lastPosition;
    Quaternion lastRotation;

    float generateCooldown = 0;

    public override void InitState(NavMeshAgent agent)
    {
        base.InitState(agent);
        SendEnemyToNextPoint();
    }

    private Vector3 GenerateRandomPoint()
    {
        // dát rozmìry terénu
        
        return new Vector3(Random.Range(0, MovementController.instance.tr.terrainData.size.x), 0, Random.Range(0, MovementController.instance.tr.terrainData.size.z));
    }

    public override void DoStep()
    {
        checkPosition.x = agent.transform.position.x;
        checkPosition.z = agent.transform.position.z;

        if (Vector3.Distance(targetPoint, checkPosition) < 1 || IsNotMoving())
        {
            SendEnemyToNextPoint();
        }

        lastRotation = agent.transform.rotation;
        lastPosition = agent.transform.position;
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
       
        return null;
    }
}
