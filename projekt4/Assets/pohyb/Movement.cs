using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
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

  
    
  
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
      


    }
    void Start()
    {
        agent.stoppingDistance = 2;
        MovementController.instance.units.Add(this);
       

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
    public void HejbniSe(Vector3 destination)
    {
       
         agent.SetDestination(destination);



    }
}
