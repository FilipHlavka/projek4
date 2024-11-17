using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class Pozice
{
    public List<Vector3> newPositions = new List<Vector3>();

    public virtual List<Vector3> makeMath(Vector3 startPosition, List<Movement> unitsList) { return new List<Vector3>(); }

}
