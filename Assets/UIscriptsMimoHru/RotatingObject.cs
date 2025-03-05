using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class RotatingObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    List<GameObject> list = new List<GameObject>();
    public float speed = 3;
    Quaternion rotation;
    void Start()
    {
        rotation = transform.rotation;
        Instantiate(list[Random.Range(0,list.Count)],transform);
       
    }

    // Update is called once per frame
    void Update()
    {
        rotation *= Quaternion.Euler(0f, speed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
    }
}
