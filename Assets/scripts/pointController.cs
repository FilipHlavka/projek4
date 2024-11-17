using UnityEngine;
using System.Collections;

public class pointController : MonoBehaviour
{
   
    public GameObject point;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static pointController ptController;
    Vector3 zaklPosition = new Vector3(0, 800, 0);

    private void Awake()
    {
        ptController = this;
    }
    void Start()
    {
        point = this.gameObject;
        point.transform.position = zaklPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "unit")
        {
            StartCoroutine(Delay());
        }
    }

    public void Move(Vector3 position)
    {
        point.transform.position = position;
    }
    public void MoveUp()
    {
        point.transform.position = zaklPosition;
    }
    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(1f);
        MoveUp();
    }
}

