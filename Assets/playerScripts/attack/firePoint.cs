using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;

public class firePoint : MonoBehaviour
{
    [SerializeField]
    protected float shootCooldown;
    [SerializeField]
    protected playerAttack plAtck;
    [SerializeField]
    protected int accuracyOffset;

    [SerializeField]
    protected int viewAngle;
    public bool readyForFire = false;
    protected float timer;
    [SerializeField]
    protected projectileType projectileType;

    [SerializeField]
    public AudioClip shot;
    [SerializeField]
    public AudioSource source;
   
/*
    [SerializeField]
    public fireMode fireMode;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (plAtck.enemy != null)
            MakeAttackMove();
    }

    private void MakeAttackMove()
    {
        if (plAtck.shouldFight)
        {
            //Debug.Log("ano");
            if (Vector3.Angle(transform.forward, plAtck.enemy.transform.position - transform.position) <= viewAngle)
            {
               // Debug.Log("zde" + transform.name);
                LayerMask layerMask = ~LayerMask.GetMask("pointer");
                
               // Debug.DrawRay(transform.position, plAtck.enemy.transform.position - transform.position, Color.red);

                if (Physics.Raycast(transform.position, plAtck.enemy.transform.position - transform.position, out RaycastHit hit,1000,layerMask))
                {
                    //Debug.Log("zde" + hit.transform.name);

                    if (hit.transform == plAtck.enemy.transform)
                    {
                        Debug.DrawRay(transform.position, plAtck.enemy.transform.position - transform.position, Color.red);
                        readyForFire = true;
                        
                    }
                    else
                    {
                        readyForFire = false;
                        //plAtck.CheckForFollow();
                    }
                }
                else
                {
                    readyForFire = false;
                    //plAtck.CheckForFollow();

                }

                if (readyForFire)
                {

                    inicializeFire();
                }

            }
            else
            {
                readyForFire = false;
            }



        }
    }

    protected Vector3 GenerateFireDestination()
    {
        int x = UnityEngine.Random.Range(-accuracyOffset, accuracyOffset);
        int z = UnityEngine.Random.Range(-accuracyOffset, accuracyOffset);
        Vector3 destination = plAtck.enemy.transform.position + new Vector3(x,0,z);
        source.clip = shot;
        source.Play();

        return destination;
    }

    private void OnDrawGizmosSelected()
    {
        if (viewAngle > 0)
        {
            Gizmos.color = Color.yellow;
            Vector3 forward = transform.forward;

            
            Quaternion leftRayRotation = Quaternion.Euler(0, -viewAngle, 0);
            Quaternion rightRayRotation = Quaternion.Euler(0, viewAngle, 0);

            Vector3 leftRayDirection = leftRayRotation * forward;
            Vector3 rightRayDirection = rightRayRotation * forward;

            
            Gizmos.DrawRay(transform.position, forward * 5);

           
            Gizmos.DrawRay(transform.position, leftRayDirection * 5);
            Gizmos.DrawRay(transform.position, rightRayDirection * 5);

            
            DrawArc(forward, viewAngle, 5, 20);
        }
    }

    private void DrawArc(Vector3 forward, float angle, float radius, int segments)
    {
        Vector3 startPoint = Quaternion.Euler(0, -angle, 0) * forward * radius + transform.position;
        Vector3 previousPoint = startPoint;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -angle + (i * (angle * 2 / segments));
            Vector3 nextPoint = Quaternion.Euler(0, currentAngle, 0) * forward * radius + transform.position;

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }

        // Close the arc back to the center
        Gizmos.DrawLine(previousPoint, startPoint);
    }

    public virtual void inicializeFire(){ }
}/*
public enum fireMode{
    single,
    doubleBurst,
    triBurst,
    quadBurst
}*/

public enum projectileType
{
    laser,
    redLaser,
    rocket
}
