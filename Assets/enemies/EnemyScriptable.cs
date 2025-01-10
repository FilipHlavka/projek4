
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "enemies", menuName = "enemyData", order = 0)]
public class EnemyScriptable : ScriptableObject
{
    public List<EnemyDataHolder> prefs;
}
