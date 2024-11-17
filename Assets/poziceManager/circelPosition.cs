using System.Collections.Generic;
using UnityEngine;

public class circlePosition : Pozice
{
    public override List<Vector3> makeMath(Vector3 startPosition, List<Movement> unitsList)
    {


        float radius  = Mathf.Floor(Mathf.Sqrt(unitsList.Count))*2;

        int i = 0;
        foreach (var unit in unitsList)
        {
            float angle = i * (2 * Mathf.PI / unitsList.Count);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            newPositions.Add(new Vector3(startPosition.x + x, 0, startPosition.z + z));
            i++;

        }
        return newPositions;
    }
}
