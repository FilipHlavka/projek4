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
   
/*
    [SerializeField]
    public fireMode fireMode;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
            if (Vector3.Angle(transform.forward, plAtck.enemy.transform.position - transform.position) <= viewAngle)
            {
                Debug.Log("zde" + transform.name);
                LayerMask layerMask = ~LayerMask.GetMask("pointer");
                if (Physics.Raycast(transform.position, plAtck.enemy.transform.position - transform.position, out RaycastHit hit,1000,layerMask))
                {
                    //Debug.Log("zde" + hit.transform.name);

                    if (hit.transform == plAtck.enemy.transform)
                    {
                        Debug.DrawRay(transform.position, plAtck.enemy.transform.position - transform.position, Color.red);
                        readyForFire = true;
                        //Debug.Log("haaaaaaaaaaaloooooo");
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



        }
    }

    protected Vector3 GenerateFireDestination()
    {
        int x = UnityEngine.Random.Range(-accuracyOffset, accuracyOffset);
        int z = UnityEngine.Random.Range(-accuracyOffset, accuracyOffset);
        Vector3 destination = plAtck.enemy.transform.position + new Vector3(x,0,z);

        return destination;
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
