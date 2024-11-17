using System;
using System.Collections.Generic;
using UnityEngine;

public class trianglePosition : Pozice
{
    public override List<Vector3> makeMath(Vector3 startPosition, List<Movement> unitsList)
    {
        

        double x = unitsList.Count;
        int k = 0;
      
        for (int i = 0; i <= x && k < unitsList.Count; i++)
        {
            for (int d = 0; d < i && k < unitsList.Count; d++)
            {
                newPositions.Add(new Vector3(startPosition.x + i * 5, 0, startPosition.z  + d * 5));
                k++;

            }
            
        }

        return newPositions;
    }
}
