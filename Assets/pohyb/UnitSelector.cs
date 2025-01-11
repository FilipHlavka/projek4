using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
                if(!isClickingOnShop())
                MovementController.instance.AddToList(hit.transform);
                
            }
            else if (isClickingOnShop())
            {


            }
            else
            {
                MovementController.instance.ClearList();
                MovementController.instance.unselect = false;
            }

            LayerMask stationMask = LayerMask.GetMask("station");
            

            if (Physics.Raycast(ray, out RaycastHit hit2, Mathf.Infinity, stationMask))
            {
                Debug.Log("funguj");
                StationController.instance.AddStation(hit2.transform);
            }
            else if(isClickingOnShop())
            {
               
                
            }
            else
            {
                StationController.instance.RemoveStation();
            }

            LayerMask supportStationMask = LayerMask.GetMask("Water");


            if (Physics.Raycast(ray, out RaycastHit hit3, Mathf.Infinity, supportStationMask))
            {
                Debug.Log("funguj");
                StationController.instance.AddSupportStation(hit3.transform);
            }
            else if (isClickingOnShop())
            {


            }
            else
            {
                
                StationController.instance.RemoveSupportStation();
            }

        }
    }

    private bool isClickingOnShop()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("shop")) 
            {
                return true;
            }
        }
        return false;
    }

}
