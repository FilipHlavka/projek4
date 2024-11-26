using UnityEngine;

public class StationMovement : Movement
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
       
        StationController.instance.stations.Add(this);
        atck = gameObject.GetComponent<StationAttack>();
    }

}
