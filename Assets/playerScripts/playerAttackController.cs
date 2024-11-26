using System.Collections.Generic;
using UnityEngine;

public class playerAttackController : MonoBehaviour
{
   
    [SerializeField]
    public Camera cam;
    public static playerAttackController instance;
    public List<Movement> selectedUnits = new List<Movement>();
    public StationMovement station;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedUnits = MovementController.instance.selectedUnits;
       
    }

    // Update is called once per frame
    void Update()
    {
        UnitAttack();
        stationAttack();
    }

    private void UnitAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Pauza.pauza.canChange)
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = ~LayerMask.GetMask("pointer");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.gameObject.tag == "enemy" && selectedUnits.Count != 0)
                {
                    //Debug.Log(hit.transform.gameObject.tag);
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.ShowPointer(true);

                    // pointController.ptController.Move(hit.point);

                    foreach (var obj in selectedUnits)
                    {
                        enemy.attackingPlayers.Add(obj);
                        obj.atck.AttackTarget(enemy, obj);

                    }
                    pointController.ptController.MoveUp();

                }

            }



        }
    }

    private void stationAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Pauza.pauza.canChange)
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = ~LayerMask.GetMask("pointer");

           
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                //Debug.Log(hit.transform.gameObject.tag);

                if (hit.transform.gameObject.tag == "enemy" && station != null)
                {
                    Debug.Log(hit.transform.gameObject.tag);
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.ShowPointer(true);

                    // pointController.ptController.Move(hit.point);

                
                        enemy.attackingStation = station;
                        station.atck.AttackTarget(enemy, station);
                    
                       

                    
                    pointController.ptController.MoveUp();

                }

            }



        }
    }
}
