using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SocialPlatforms.Impl;

public class kamPohyb3d : MonoBehaviour
{
    [SerializeField]
    public int speed;
    [SerializeField]
    public int movementSpeed;
   

    [SerializeField]
    float scrol;
    [SerializeField]
    Camera cam;
   

   
    public float movex = 0;
    public float movez = 0;


    //Transform bod;
    Vector3 smer;
    Vector3 normVector;
    
    Quaternion otoc;
    

    void Start()
    {
       

    }
   
    // Update is called once per frame
    void Update()
    {

        HniSe();

       

        
    }



    private void HniSe()
    {


        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float je = Input.GetAxis("Mouse ScrollWheel");
            if (je > 0)
            {
                smer = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);

                cam.transform.position -= smer * speed * Time.deltaTime * 5;
            }
            if (je < 0)
            {
                smer = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                if (Vector3.Distance(transform.position, cam.transform.position) < 50)
                    cam.transform.position += smer * speed * Time.deltaTime * 5;
            }

        }


        Vector3 camDirection = new Vector3(cam.transform.position.x, 0, cam.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 rightVector = Vector3.Cross(camDirection, Vector3.up).normalized;
        Vector3 forwardVector = -camDirection.normalized;

        
        Vector3 movement = Vector3.zero;

      
        if (Input.GetKey(KeyCode.A))
        {
            movement -= rightVector;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += rightVector;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement += forwardVector;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= forwardVector;
        }

        
        transform.position += movement.normalized * movementSpeed * Time.deltaTime;



        if (Input.GetKey(KeyCode.Q))
        {
            otoc = Quaternion.Euler(0f, -speed * Time.deltaTime * 45, 0f);
            transform.rotation *= otoc;


        }
        if (Input.GetKey(KeyCode.E))
        {
            otoc = Quaternion.Euler(0f, speed * Time.deltaTime * 45, 0f);
            transform.rotation *= otoc;


        }
    }







}


