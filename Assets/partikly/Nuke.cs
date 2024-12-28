using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Nuke : Explosion
{
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    float delay;

    private void Start()
    {
        // nutno znièit všechny instance 

        Invoke("Prehraj", delay);
        Destroy(gameObject,9);
      
    }
    

    public void Prehraj()
    {
       
      soundManager.instance.PlayClip(clip);
    }
}
