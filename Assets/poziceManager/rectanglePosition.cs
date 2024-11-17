using System.Collections.Generic;
using UnityEngine;

public class rectanglePosition :Pozice
{
    public int rows = 2;
    public int columns = 4;
    public override List<Vector3> makeMath(Vector3 startPosition, List<Movement> unitsList)
    {


        
        int k = 0;
        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j < columns && k < unitsList.Count; j++)
            {

                newPositions.Add(new Vector3(startPosition.x + i * 5, 0, startPosition.z + j * 5));
                k++;

            }
        }

        return newPositions;


    }
}
