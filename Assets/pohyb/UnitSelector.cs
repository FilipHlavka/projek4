using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public static UnitSelector Instance;
    Camera cam;
    public List<Movement> stations = new List<Movement>();
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = MovementController.instance.cam;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Pauza.pauza.canChange)
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            LayerMask unitMask = LayerMask.GetMask("unit");
           
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, unitMask ) && !MovementController.instance.unselect)
            {
                MovementController.instance.AddToList(hit.transform);
                
            }
            else
            {
                MovementController.instance.ClearList();
                MovementController.instance.unselect = false;
            }

            LayerMask stationMask = LayerMask.GetMask("station");

            if (Physics.Raycast(ray, out RaycastHit hit2, Mathf.Infinity, stationMask))
            {
              
            }
            else
            {
               
            }

        }
    }
}
