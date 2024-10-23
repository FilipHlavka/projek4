using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
           // LayerMask layerMask = ~LayerMask.GetMask("unit");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag != "unit")
                {
                    Debug.Log(hit.transform.gameObject.tag);
                    foreach (var obj in selectedUnits)
                    {
                        obj.HejbniSe(hit.point);

                    }
                }
                
            }



        }
    }
}
