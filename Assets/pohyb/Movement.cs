using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class Movement: MonoBehaviour
{
   
    public NavMeshAgent agent;
    bool stuj = false;
    //public static bool nehejbat = false;
    [SerializeField]
    Slider ring;
    public playerAttack atck;
    public Enemy enemyToAttack;
    
  
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
      
    }
    public virtual void Start()
    {
        agent.stoppingDistance = 2;
        MovementController.instance.units.Add(this);
        atck = gameObject.GetComponent<playerAttack>();

    }

    public void isSelected(bool tr)
    {
        if(tr)
        ring.value = 1;
        else
        ring.value = 0;
    }
 
    public void Zastav()
    {
        stuj = !stuj;
        agent.isStopped = false;
    }
    public virtual void MoveIt(Vector3 destination)
    {
       
         agent.SetDestination(destination);



    }
}
