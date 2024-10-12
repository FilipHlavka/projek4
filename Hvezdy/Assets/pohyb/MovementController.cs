using NUnit.Framework;
using System.Collections.Generic;
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
            Debug.Log("pøidáno");
            selectedUnits.Add(mv);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = ~LayerMask.GetMask("player");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {

                foreach (var obj in selectedUnits)
                {
                    obj.HejbniSe(hit.point);
                }
            }



        }
    }
}
