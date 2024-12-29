using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Nuke : Explosion
{
    [SerializeField]
    public AudioClip clip;
    [SerializeField]
    float delay;
    [SerializeField]
    float TTL = 9;

    private void Start()
    {
        // nutno zni�it v�echny instance 

        Invoke("Prehraj", delay);
        Destroy(gameObject,TTL);
      
    }
    

    public virtual void Prehraj()
    {
       
      soundManager.instance.PlayClip(clip);
    }
}
