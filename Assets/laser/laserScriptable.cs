using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "laser", menuName = "laserData", order = 0)]
public class laserScriptable : ScriptableObject
{
   public List<laserDataHolder> prefs;
}
