using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    public static MovementController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    public List<Movement> selectedUnits = new List<Movement>();
    public List<Movement> units = new List<Movement>();
    [SerializeField]
    public Camera cam;
    public bool unselect = false;
    [SerializeField]
    public Terrain tr;
    public List<Vector3> newPositions = new List<Vector3>();
    [SerializeField]
    GameObject Cross;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public void AddToList(Transform tf)
    {
        
        if(tf.gameObject.TryGetComponent<Movement>(out Movement mv))
        {
            
            selectedUnits.Add(mv);
            mv.isSelected(true);
        }
    }
    public void ClearList()
    {
        foreach(var n in selectedUnits)
            n.isSelected(false);

        selectedUnits.Clear();
        

    }
    public void AddToListBox(Movement mv)
    {
           
            Debug.Log("pøidáno");
            selectedUnits.Add(mv);
            mv.isSelected(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Pauza.pauza.canChange)
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
           LayerMask layerMask = ~LayerMask.GetMask("pointer");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,layerMask))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit hit2, 1, NavMesh.AllAreas))
                {
                    if (hit.transform.gameObject.tag != "unit" && hit.transform.gameObject.tag != "enemy" && selectedUnits.Count != 0)
                    {
                        //Debug.Log(hit.transform.gameObject.tag);
                        int i = 0;

                        pointController.ptController.Move(hit.point);
                        newPositions = PoziceManager.Instance.aktPosition.makeMath(hit.point, selectedUnits);
                        foreach (var obj in selectedUnits)
                        {

                            obj.MoveIt(newPositions[i]);
                            obj.atck.stopAttacking();
                            i++;

                        }
                        newPositions.Clear();
                    }

                }
                else if(hit.transform.gameObject.tag != "unit" && hit.transform.gameObject.tag != "enemy" && selectedUnits.Count != 0)
                {
                    Instantiate(Cross, hit.point, Quaternion.Euler(0, 0, 0));
                    return;
                }
            }
                



        }
    }
}
