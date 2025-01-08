using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using System.Drawing;
using System.Collections;

public class Cross : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager.instance.PlayClip(clip);
        faceToCamera();
        Invoke("Hide", 0.5f);
    }

    public void faceToCamera()
    {
        Vector3 direction = new Vector3(Camera.main.transform.position.x,0, Camera.main.transform.position.z) - new Vector3(gameObject.transform.position.x, 0 , gameObject.transform.position.z);
        transform.rotation = Quaternion.LookRotation(direction);
        transform.rotation *= Quaternion.Euler(0,45,0);
    }

    public void Hide()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
