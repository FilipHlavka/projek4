using System.Collections.Generic;
using UnityEngine;


public class boxPosition : Pozice
{

    public override List<Vector3> makeMath(Vector3 startPosition, List<Movement> unitsList) {

        double x = Mathf.Round(Mathf.Sqrt(unitsList.Count));
       
        int k = 0;
        for (int i = 0; i <= x; i++)
        {           
            for (int j = 0; j < x && k < unitsList.Count; j++)
            {              
                newPositions.Add(new Vector3(startPosition.x + i * 8, 0, startPosition.z + j * 8));
                k++;               
            }
        }
        return newPositions; 
     }
}
